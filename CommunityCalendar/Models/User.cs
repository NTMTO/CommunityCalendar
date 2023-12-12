using CommunityCalendar.Utilities;
namespace CommunityCalendar.Models;



public class User
{
    
    public ISqlServices _sqlServices;

    private static string table = "UserDB";
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public List<Guid>? EventsParticipatingIn { get; set; }
    public List<Guid>? EventsCreated { get; set; }
    public string NotificationPreferences { get; set; }

    public User(Guid UserId, string FirstName, string LastName, DateTime DateOfBirth, string Phone, string Email, string Address, string Role, string NotificationPreferences, ISqlServices sqlServices)
    {

        _sqlServices = sqlServices;

        this.UserId = UserId;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.DateOfBirth = DateOfBirth;
        this.Phone = Phone;
        this.Email = Email;
        this.Address = Address;
        this.Role = Role;
        this.NotificationPreferences = NotificationPreferences;
        this.EventsParticipatingIn = GatherEventsParticipatingIn();
        this.EventsCreated = GatherEventsCreated();

    }

    public List<Guid>? GatherEventsParticipatingIn() => SearchEvents("EventParticipantDB", "userId");
    public List<Guid>? GatherEventsCreated() => SearchEvents("CalEventDB", "creator");
    public List<Guid>? SearchEvents(string table, string parameter)
    {
        List<Guid>? resultList = new();
        Dictionary<string, object> args = new() { { parameter, UserId } };
        List<Dictionary<string, object>>? result = _sqlServices.Search(table, args);
        foreach (var item in result)
        {
            resultList.Add((Guid)item["eventId"]);
        }
        return resultList.Count > 0 ? resultList : null;

    }

    //Change all of these to just accept the object instead

    //public void Save() => SqlServices.Update(table, this);
    //public void Update() => Save();
    //public void Delete() => SqlServices.Delete(table, this);
    //public void Create() => SqlServices.Create(table, this);
}
