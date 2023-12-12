using CommunityCalendar.Models;

namespace CommunityCalendar.Tests
{
    public interface ISampleDataProvider
    {
        Dictionary<string, object> SampleUserRow { get; set; }
        Dictionary<string, object> sampleCalEventRow { get; set; }
        User sampleUser { get; set; }
    }
}