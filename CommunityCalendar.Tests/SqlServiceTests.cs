using System;
using System.Reflection;
using Autofac;
using CommunityCalendar;
using CommunityCalendar.Utilities;

namespace CommunityCalendar.Tests
{
    public class SqlServiceTests : AutofacTestBase
    {

        ISqlServices _sqlServices;
        ISampleDataProvider _sampleDataProvider;

        public SqlServiceTests()
        {
            _sqlServices = _container.Resolve<ISqlServices>();
            _sampleDataProvider = _container.Resolve<ISampleDataProvider>();
        }

        [Fact]
        public void Search_ReturnsRow()
        {
            //arrange
            Dictionary<string, string> expected = new();
            Dictionary<string, string> actual = new();
            Dictionary<string, object> sample = _sampleDataProvider.SampleUserRow;

            //act
            Dictionary<string, object> testSearch = new()
            {
                {"firstName", "Adam"},
                {"lastName", "Cluchey"}
            };
            List<Dictionary<string, object>> result = _sqlServices.Search("UserDB", testSearch);

            Dictionary<string, object>? output = result.Count > 0 ? result[0] : null;

            if (output is not null)
            {
                foreach (KeyValuePair<string, object> item in sample)
                {
                    expected.Add(item.Key.ToString(), item.Value?.ToString());
                }
                foreach (var item in output)
                {
                    actual.Add(item.Key.ToString(), item.Value?.ToString());
                }

                //assert
                //Assert.True(actual == expected);

                foreach (KeyValuePair<string, string> item in actual)
                {
                    Assert.True(actual[item.Key] == expected[item.Key]);
                }
            }
            else
            {
                Assert.True(false);
            }

            //}

            //[Theory]
            //[InlineData("UserDB")] //This doesn't seem to be returning anything with given search below
            ////[InlineData("CalEventDB")]
            //public void SearchAllColumns_Works(string table)
            //{
            //    //Arrange
            //    User expected = new User(SampleData.sampleUserRow);

            //    //Act
            //    var result = SqlService.SearchAllColumns(table, "%adam%");
            //    User actual = new User(result.First());

            //    PropertyInfo[] props = typeof(User).GetProperties();

            //    //Assert
            //    foreach (PropertyInfo prop in props)
            //    {
            //        Assert.True(prop.GetValue(expected) == prop.GetValue(actual));
            //    }

            //}
        }
    }
}

//string firstName, string lastName, DateTime dateOfBirth, string phone, string address, string email, string? role, string? notificationPreferences
