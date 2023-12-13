using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Autofac;
using CommunityCalendar.Data;
using CommunityCalendar.Models;
using CommunityCalendar.Utilities;

namespace CommunityCalendar.Tests
{
    public class SampleDataProvider :  AutofacTestBase, ISampleDataProvider
    {

        ISqlServices _sqlServices;

        public User sampleUser { get; set; }

        public SampleDataProvider()
        {
            _sqlServices = _container.Resolve<ISqlServices>();

            sampleUser = new User
            (
                (Guid)SampleUserRow["userId"],
                (string)SampleUserRow["firstName"],
                (string)SampleUserRow["lastName"],
                (DateTime)SampleUserRow["dateOfBirth"],
                (string)SampleUserRow["phone"],
                (string)SampleUserRow["email"],
                (string)SampleUserRow["address"],
                (string)SampleUserRow["role"],
                (string)SampleUserRow["notificationPreferences"],
                _sqlServices 
            );
        }

        public Dictionary<string, object> SampleUserRow { get; set; } = new()
        {
            {"userId", Guid.Parse("c1c762ac-6e90-4086-b8b3-3593c1e9188c")},
            {"firstName", "Adam"},
            {"lastName", "Cluchey"},
            {"dateOfBirth", DateTime.Parse("2/27/1986 12:00:00 AM")},
            {"phone", "214-608-0701"},
            {"address", "5671 Worrell Dr Ft Worth TX 76133"},
            {"email", "adam@adamcluchey.com"},
            {"role", "Admin"},
            {"notificationPreferences", "Email"},
            {"eventsParticipatingIn", null },
            {"eventsCreated", null }
        };


        public Dictionary<string, object> sampleCalEventRow { get; set; } = new()
        {
            {"userId", Guid.Parse("c1c762ac-6e90-4086-b8b3-3593c1e9188c")},
            {"firstName", "Adam"},
            {"lastName", "Cluchey"},
            {"dateOfBirth", DateTime.Parse("2/27/1986 12:00:00 AM")},
            {"phone", "214-608-0701"},
            {"address", "5671 Worrell Dr Ft Worth TX 76133"},
            {"email", "adam@adamcluchey.com"},
            {"role", "Admin"},
            {"notificationPreferences", "Email"}
        };


    }
}

