using System;
using System.Data;
using System.Net;
using System.Reflection;
using Autofac;
using CommunityCalendar.Models;
using CommunityCalendar.Utilities;
namespace CommunityCalendar.Tests
{
    public class ClassServiceTests: AutofacTestBase
    {
        ISampleDataProvider _sampleDataProvider;
        IClassServices _classServices;

        public ClassServiceTests()
        {
            _sampleDataProvider = _container.Resolve<ISampleDataProvider>();
            _classServices = _container.Resolve<IClassServices>();
        }

        //Naming convention: Object Type + _Method Name_ + 'Test'
        [Fact]
        public void User_PropsAsDict_Test()
        {
            //Arrange
            User sampleUser = _sampleDataProvider.sampleUser;

            Dictionary<string, object> expected = new();
            PropertyInfo[] props = sampleUser.GetType().GetProperties();
            foreach (var prop in props)
            {
                expected.Add(prop.Name, prop.GetValue(sampleUser));
            }

            //Act

            Dictionary<string, object> actual = _classServices.PropsAsDict(sampleUser);

            //assert
            bool tf = true;
            foreach (var prop in props)
            {
                string? actualValue = actual[prop.Name]?.ToString();
                string? expectedValue = expected[prop.Name]?.ToString();
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

