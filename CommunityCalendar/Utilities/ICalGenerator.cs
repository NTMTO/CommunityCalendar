namespace CommunityCalendar.Utilities
{
    public interface ICalGenerator
    {
        List<List<DateTime>> Calculate(DateTime ChosenMonth);
        List<DateTime> ExpandToSunSatDisplay(List<DateTime> monthDays);
        List<List<DateTime>> MonthInWeeks(List<DateTime> monthDays);
    }
}