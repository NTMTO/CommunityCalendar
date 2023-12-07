using System;
using System.Data;
using System.Net;
using System.Reflection;
using CommunityCalendar.Models;
using CommunityCalendar.Utilities;
namespace CommunityCalendar.Tests
{
    public class ClassServiceTests
    {

        //Naming convention: Object Type + _Method Name_ + 'Test'
        [Fact]
        public void User_PropsAsDict_Test()
        {
            //Arrange
            Dictionary<string, object> sample = SampleData.sampleUserRow;
            User sampleUser = new(
                UserId: (Guid)sample["userId"],
                FirstName: (string)sample["firstName"],
                LastName: (string)sample["lastName"],
                DateOfBirth: (DateTime)sample["dateOfBirth"],
                Phone: (string)sample["phone"],
                Email: (string)sample["email"],
                Address: (string)sample["address"],
                Role: (string)sample["role"],
                NotificationPreferences: (string)sample["notificationPreferences"]
                );

            //Act
            Dictionary<string, object> expected = new();
            PropertyInfo[] props = sampleUser.GetType().GetProperties();
            foreach (var prop in props)
            {
                expected.Add(prop.Name, prop.GetValue(sampleUser));
            }

            Dictionary<string, object> actual = ClassServices.//nned to use DI

            //assert
            bool tf = true;
            foreach (var prop in props)
            {
                string expectedKey = char.ToLower(prop.Name[0]) + prop.Name[1..];
                string? actualValue = actual[prop.Name]?.ToString();
                string? expectedValue = expected[expectedKey]?.ToString();
                if (actualValue != expectedValue)
                    { tf = false; }
            }
            Assert.True(tf);
        }
        //[Fact]
        //public void CalEvent_PropsAsDict_Test()
        //{
        //    //Arrange
        //    Dictionary<string, object> expected = SampleData.sampleCalEventRow;
        //    CalEvent sampleUser = SampleData.sampleCalEvent;

        //    //Act
        //    Dictionary<string, object> actual = new();
        //    PropertyInfo[] props = sampleCalEvent.GetType().GetProperties();
        //    foreach (var prop in props)
        //    {
        //        actual.Add(prop.Name, prop.GetValue(sampleCalEvent));
        //    }

        //    //assert
        //    foreach (var prop in props)
        //    {
        //        Assert.True(actual[prop.Name] == expected[prop.Name]);
        //    }
        //}
        //TODO uncoment above once first calendar event created



    }

}

