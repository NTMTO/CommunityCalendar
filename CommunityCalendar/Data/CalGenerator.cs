using System;
namespace CommunityCalendar.Data
{
    public class CalGenerator
    {

        public List<List<DateTime>> Calculate(DateTime ChosenMonth)
        {
            DateTime workingDate = DateTime.Now;
            //To ensure the datetime is in the future..
            while (workingDate.Month != ChosenMonth.Month)
            {
                workingDate.AddMonths(2);
            }
            workingDate = new DateTime(workingDate.Year, workingDate.Month, 1);
            DateTime endDate = workingDate.AddMonths(1);
            List<DateTime> workingList = new();
            while (workingDate < endDate)
            {
                workingList.Add(workingDate);
                workingDate = workingDate.AddDays(1);
            }
            List<DateTime> expanded = ExpandToSunSatDisplay(workingList);
            List<List<DateTime>> monthInWeeks = MonthInWeeks(expanded);
            return monthInWeeks;
        }

        public List<DateTime> ExpandToSunSatDisplay(List<DateTime> monthDays)
        {
            while (monthDays[0].DayOfWeek != DayOfWeek.Sunday)
            {
                monthDays.Insert(0, monthDays[0] - TimeSpan.FromDays(1));
            }
            while (monthDays[^1].DayOfWeek != DayOfWeek.Saturday)
            {
                monthDays.Add(monthDays[^1] + TimeSpan.FromDays(1));
            }
            return monthDays;
        }

        public List<List<DateTime>> MonthInWeeks(List<DateTime> monthDays)
        {
            List<List<DateTime>> workingList = new();
            List<DateTime> workingWeek = new();

            foreach (DateTime day in monthDays)
            {
                workingWeek.Add(day);
                if (day.DayOfWeek == DayOfWeek.Saturday)
                {
                    workingList.Add(workingWeek);
                    workingWeek = new ();
                }
            }
            return workingList;
        }
    }
}