namespace CommunityCalendar.Models;

using System.Data;
using System.Reflection;

public class CalEvent //ISelfAsDict
{
    private static string table = "CalEventDB";
    //private Dictionary<string, object> selfAsDict = new();

    public Guid EventId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Guid Creator { get; set; }
    public string EventLocation { get; set; }
    public string Description { get; set; }
    public int? ParticipantLimit { get; set; }
    public int CurrentParticipants { get; set; }

    //The SelfAsDict methods might actually be stupid. Maybe not going to do that.

    //public Dictionary<string, object> SelfAsDict
    //{
    //    get
    //    {
    //        return selfAsDict;
    //    }
    //    set
    //    {
    //        PropertyInfo[] properties = this.GetType().GetProperties();
    //        foreach (var p in properties)
    //        {
    //            if (p.Name != "SelfAsDict")
    //            {
    //                selfAsDict.Add(p.Name, p.GetValue(this));
    //            }
    //        }
    //    }
    //}

    public CalEvent(Dictionary<string, object> args)
    {
        EventId = (Guid)args["eventId"];
        Start = (DateTime)args["start"];
        End = (DateTime)args["end"];
        ParticipantLimit = args["participantLimit"] is not null ? (int)args["participantLimit"] : null;
        CurrentParticipants = (int)args["currentParticipants"];
        Creator = (Guid)args["creator"];
        EventLocation = (string)args["eventLocation"];
        Description = (string)args["description"];
        //SelfAsDict = selfAsDict;
    }

    //Change all of these to just accept the object instead
    //public void Save() => SqlService.Update(table, SelfAsDict);
    //public void Update() => Save();
    //public void Delete() => SqlService.Delete(table, SelfAsDict);
    //public void Create() => SqlService.Create(table, SelfAsDict);
}





