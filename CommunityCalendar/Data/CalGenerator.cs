using System;
namespace CommunityCalendar.Data
{
    public class CalGenerator
    {
        //Props

        //Constructor (Not Needed?)

        //Methods

        public List<DateTime> Calculate(DateTime ChosenMonth)
        {
            DateTime workingDate = DateTime.Now;
            //To ensure the datetime is in the future..
            while (workingDate.Month != ChosenMonth.Month)
            {
                workingDate.AddMonths(1);
            }
            workingDate = new DateTime(workingDate.Year, workingDate.Month, 1);
            DateTime endDate = workingDate.AddMonths(1);
            List<DateTime> workingList = new List<DateTime>();
            while (workingDate < endDate)
            {
                workingList.Add(workingDate);
                workingDate = workingDate.AddDays(1);
            }
            return workingList;
         }

    }
}

