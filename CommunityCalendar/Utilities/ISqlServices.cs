namespace CommunityCalendar.Utilities
{
    public interface ISqlServices
    {
        List<Dictionary<string, object>>? BuildSubmitCommand(string command, Dictionary<string, object> args);
        List<Dictionary<string, object>>? Create(string table, object obj);
        List<Dictionary<string, object>>? Delete(string table, object obj);
        Dictionary<string, string> GetColumns(string table);
        List<Dictionary<string, object>>? Search(string table, Dictionary<string, object> args);
        List<Dictionary<string, object>>? SearchAllColumns(string table, string arg);
        List<Dictionary<string, object>>? Update(string table, object obj);
    }
}