using Microsoft.Data.SqlClient;
using CommunityCalendar.Data;
using CommunityCalendar.Models;

namespace CommunityCalendar.Utilities
{
    public class SqlServices
    {

        private SqlConnection BuildConnection()
        {
            SqlConnectionStringBuilder builder = new();

            builder.DataSource = "community-calendar.database.windows.net";
            builder.UserID = Secrets.azureSqlUsername;
            builder.Password = Secrets.azureSqlPassword;
            builder.InitialCatalog = "CommunityCalendarDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            connection.Open();

            return connection;
        }

        public List<Dictionary<string, object>>? Create(string table, object obj)
        {

            Dictionary<string, object> args = new()
            {
                {"userId", user.UserId},
                {"firstName", user.FirstName},
                {"lastName", user.LastName},
                {"address", user.Address},
                {"role", user.Role},
                {"notificationPreferences", user.NotificationPreferences},
                {"dateOfBirth", user.DateOfBirth},
                {"email", user.Email},
                {"phone", user.Phone}
            };
            List<string> columns = new List<string>();
            List<string> parameters = new List<string>();
            foreach (KeyValuePair<string, object> k in args)
            {
                columns.Add(k.Key);
                parameters.Add("@" + k.Key);
            }
            string command = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, string.Join(", ", columns), string.Join(", ", parameters));
            return BuildSubmitCommand(command, args);
        }

        public List<Dictionary<string, object>>? Delete(string table, Dictionary<string, object> args)
        {
            List<string> columns = new List<string>();
            List<string> parameters = new List<string>();
            foreach (KeyValuePair<string, object> k in args)
            {
                if (k.Key.Contains("Id"))
                {
                    columns.Add(k.Key);
                    parameters.Add("@" + k.Key);
                }
            }
            string command = string.Format("DELETE FROM {0} WHERE {1}={2}", table, string.Join(", ", columns), string.Join(", ", parameters));
            return BuildSubmitCommand(command, args);
        }

        public List<Dictionary<string, object>>? Update(string table, Dictionary<string, object> args)
        {
            List<string> parameters = new List<string>();
            string identifier = "";
            foreach (KeyValuePair<string, object> k in args)
            {
                if (k.Key.Contains("Id"))
                {
                    identifier = $"{k.Key}=@{k.Key}";
                }
                else
                {
                    parameters.Add($"{k.Key}=@{k.Key}");
                }
            }
            string command = string.Format("UPDATE {0} SET {1} WHERE {2}", table, string.Join(", ", parameters), identifier);
            return BuildSubmitCommand(command, args);
        }

        
        public List<Dictionary<string, object>>? Search(string table, Dictionary<string, object> args)
        {
            List<string> parameters = new List<string>();
            foreach (KeyValuePair<string, object> k in args)
            {
                parameters.Add($"{k.Key} LIKE @{k.Key}"); //when I put single quote around the value here, I get no results, but if I take them away I get a syntax error. I also know that the columns and values specified exist in the table. Does space before/after = fix all?
            }
            string command = string.Format("SELECT * FROM {0} WHERE {1}", table, string.Join(" OR ", parameters));
            return BuildSubmitCommand(command, args);
        }

        public List<Dictionary<string, object>>? SearchAllColumns(string table, object arg)
        {
            Dictionary<string, object> args = new();
            Dictionary<string, string> columns = GetColumns(table);
            foreach (KeyValuePair<string, string> column in columns)
            {
                if (column.Value.ToString().Contains("uniqueidentifier"))
                {
                    try { arg = Guid.Parse(arg.ToString()); }
                    catch { continue; }
                }
                if (column.Value.ToString().Contains("datetime"))
                {
                    try { arg = DateTime.Parse(arg.ToString()); }
                    catch { continue; }
                }
                args.Add(column.Key.ToString(), arg);
                arg = (string)arg.ToString();
            }
            return Search(table, args);
        }

        public Dictionary<string, string> GetColumns(string table)
        {
            string command = $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";
            SqlConnection connection = BuildConnection();
            command += ";";
            Dictionary<string, string> data = new();
            //List<string> data = new();

            using (SqlCommand cmd = new SqlCommand(command, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //Collect the results
                    while (reader.Read())
                    {
                        data.Add(reader["COLUMN_NAME"].ToString(), reader["DATA_TYPE"].ToString());
                    }
                }
                return data;
            }
        }


        public List<Dictionary<string, object>>? BuildSubmitCommand(string command, Dictionary<string, object> args)
        {
            SqlConnection connection = BuildConnection();
            List<Dictionary<string, object>> data = new();
            //string dummyCommand = "SELECT * FROM UserDB WHERE firstName LIKE '@firstName%';";
            using (SqlCommand cmd = new SqlCommand(command, connection))
            {
                //cmd.Parameters.AddWithValue("@firstName", "adam");
                foreach (KeyValuePair<string, object> k in args)
                {
                    cmd.Parameters.AddWithValue($"@{k.Key}", k.Value);
                }
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    //Collect the results
                    while (reader.Read())
                    {
                        //New Row
                        Dictionary<string, object> row = new();
                        data.Add(row);
                        for (int i = 0; i < reader.GetColumnSchema().Count; i++)
                        {
                            //New Column
                            row.Add(reader.GetName(i), reader[i]);
                        }
                    }
                }
                return data;
            }
        }

    }

}


//User
//string firstName, string lastName, DateTime dateOfBirth, string phone, string address, string email, string? role, string? notificationPreferences
//Event
//string title, string address, DateTime start, DateTime end, string description, User creator