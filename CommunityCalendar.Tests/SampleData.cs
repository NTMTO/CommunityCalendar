using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using CommunityCalendar.Data;
using CommunityCalendar.Models;

namespace CommunityCalendar.Tests
{
    public class SampleData
    {

        public static readonly Dictionary<string, object> sampleUserRow = new()
        {
            {"userId", (Guid) Guid.Parse("c1c762ac-6e90-4086-b8b3-3593c1e9188c")},
            {"firstName", "Adam"},
            {"lastName", "Cluchey"},
            {"dateOfBirth", (DateTime) DateTime.Parse("2/27/1986 12:00:00 AM")},
            {"phone", "214-608-0701"},
            {"address", "5671 Worrell Dr Ft Worth TX 76133"},
            {"email", "adam@adamcluchey.com"},
            {"role", "Admin"},
            {"notificationPreferences", "Email"},
            {"eventsParticipatingIn", null },
            {"eventsCreated", null }
        };


        public static readonly Dictionary<string, object> sampleCalEventRow = new()
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

