using Microsoft.Data.SqlClient;
using CommunityCalendar.Data;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.SqlTypes;
using System.Collections.Generic;

namespace SqlService
{
    class SqlService
    {

        private SqlConnection BuildConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "community-calendar.database.windows.net";
            builder.UserID = Secrets.azureSqlUsername;
            builder.Password = Secrets.azureSqlPassword;
            builder.InitialCatalog = "CommunityCalendarDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            connection.Open();

            return connection;
        }

        public (List<string>, List<string>) BuildParams(Dictionary<string, object> args)
        {
            List<string> columns = new List<string>();
            List<string> parameters = new List<string>();
            foreach (KeyValuePair<string, object> k in args)
            {
                columns.Add(k.Key);
                parameters.Add("@" + k.Key);
            }
            return (columns, parameters);
        }

        public void Add(string table, Dictionary<string, object> args)
        {

            //string query = "INSERT INTO " + table + " (" + string.Join(",", columns) + ") VALUES (" + string.Join(",", parameters) + ")";

            string command = "INSERT INTO {0} ({1}) VALUES ({2})";
            List<string> columns = new List<string>();
            List<string> parameters = new List<string>();
            foreach (KeyValuePair<string, object> k in args)
            {
                columns.Add(k.Key);
                parameters.Add("@" + k.Key);
            }
            command = string.Format(command, string.Join(",", columns), string.Join(",", parameters));
            BuildSubmitCommand(table, command, args);
        }

        public void Delete(string table, Dictionary<string, object> args)
        {            
            string command = "DELETE FROM {0} WHERE {1}={2}";
            List<string> columns = new List<string>();
            List<string> parameters = new List<string>();
            foreach (KeyValuePair<string, object> k in args)
            {
                columns.Add(k.Key);
                parameters.Add("@" + k.Key);
            }
            command = string.Format(command, string.Join(",", columns), string.Join(",", parameters));
            BuildSubmitCommand(table, command, args);
        }

        public void Update(string table, Dictionary<string, object> args)
        {
            List<string> parameters = new List<string>();
            string identifier;
            string command = "UPDATE {0} SET {2} WHERE {3}";
            foreach (KeyValuePair<string, object> k in args)
            {
                if (k.Key.Contains("Id"))
                {
                    identifier = $"{k.Key}=@{k.Key}";
                }
                else
                {
                    parameters.Add($"{k.Key}=@{k.Key})");
                }
            }
            command = string.Format(table, string.Join(",", parameters), identifier); //STOPPED HERE//
            BuildSubmitCommand(table, command, args);
        }

        public void Search(string table, Dictionary<string, object> args)
        {
            string command = "SELECT * FROM {0} WHERE ({2})";
            BuildSubmitCommand(table, command, args);
        }


        //        .Format(table, string.Join(",", columns), string.Join(",", parameters));
        //    SubmitCommand(query, connection)
        //}

        public void BuildSubmitCommand(string table, string command, Dictionary<string, object> args)
            {

            (List<string> columns, List<string> parameters) = BuildParams(args);
            command = string.Format(command, table, string.Join(",", columns), string.Join(",", parameters);

            SqlConnection connection = BuildConnection();
            connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    foreach (KeyValuePair<string, string> k in keyValuePairs)
                        cmd.Parameters.AddWithValue(k.Key, k.Value);

                    cmd.EndExecuteNonQuery();
                }
            }
        }


    }




    private SqlConnection BuildConnection()
    {
        SqlConnection connection = BuildConnection();

    }




    // Search Users
    // Search CalEvents
    public void AddUser(string firstName, string lastName, DateTime dateOfBirth, string phone, string address, string email, string? role, string? notificationPreferences)
    {
        SqlConnection connection = BuildConnection();
        string command = Add("UserDB");
        command += $"({firstName}, {lastName}, {dateOfBirth}, {phone}, {address}, {email}, {role}, {notificationPreferences})";
        SendCommand(command, connection);
    }

    public void AddEvent(string title, string address, DateTime start, DateTime end, string description, User creator)
    {
        string userId = /*Call SElECT here*/ null;

        SqlConnection connection = BuildConnection();
        string command = Add("CalEventDB");
        command += $"({title}, {address}, {start}, {end}, {description}, {address}, {userId})";
        SendCommand(command, connection);

    }

    //string firstName, string lastName, DateTime dateOfBirth, string phone, string address, string email, string? role, string? notificationPreferences
    public void UpdateUser(Dictionary<string, string> args, string userId)
    {

        SqlConnection connection = BuildConnection();
        string command = Add("UserDB");
        foreach (KeyValuePair<string, string> item in args)
        {
            command += $" {item.Key} = {item.Value},";
        }
        command = command.Insert(command.Length - 1, " ");
        command += $"WHERE userId = {userId}";
        SendCommand(command, connection);
    }

    //string title, string address, DateTime start, DateTime end, string description, User creator
    public void UpdateEvent(Dictionary<string, string> args, string eventId)
    {

        SqlConnection connection = BuildConnection();
        string command = Add("EventDB");
        foreach (KeyValuePair<string, string> item in args)
        {
            command += $" {item.Key} = {item.Value},";
        }
        command = command.Insert(command.Length - 1, " ");
        command += $"WHERE userId = {eventId}";
        SendCommand(command, connection);
    }


    public void DeleteUser(string userId)
    {
        SqlConnection connection = BuildConnection();
        string command = Delete("UserDB");
        command += $" WHERE userId = {userId}";
        SendCommand(command, connection);
    }

    public void DeleteEvent(string eventId)
    {
        SqlConnection connection = BuildConnection();
        string command = Delete("EventDB");
        command += $" WHERE eventId = {eventId}";
        SendCommand(command, connection);
    }

    public User SearchUsers(Dictionary<string, string> args)
    {
        SqlConnection connection = BuildConnection();
        string command = Search("UserDB");
        command += BuildParams(args);
        SendCommand(command, connection);
    }


    public void SendCommand(string sqlString, SqlConnection connection)
    {
        using (SqlCommand command = new SqlCommand(sqlString, connection))
        {
            command.Parameters.Add(new SqlParameter(title, value));

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                }
            }
        }
        connection.Close();
    }

    public string BuildParams(Dictionary<string, string> args)
    {
        string command = "";
        foreach (KeyValuePair<string, string> item in args)
        {
            command += $" {item.Key} = {item.Value},";
        }
        command = command.Insert(command.Length - 1, " ");
        return command;
    }

    public string Add(string table)
    {
        return $"INSERT INTO {table} VALUES";
    }

    public string Update(string table)
    {
        return $"UPDATE {table} SET";
    }

    public string Delete(string table)
    {
        return $"DELETE FROM {table} WHERE";
    }

    public string Search(string table)
    {
        return $"SELECT * FROM {table}";
    }

    public void dothething(Dictionary<string, object> keyValuePairs)
    {
        //(Object)DBNull.Value;



    }

}














    }
}



String sql = "SELECT * FROM UserDB";

