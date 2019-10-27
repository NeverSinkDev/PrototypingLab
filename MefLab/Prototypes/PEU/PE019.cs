using CommonModules.Attributes;
using FuncCollection.TimeUtils;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefLab.Prototypes.PEU
{
    public class PE019 : AbstractModule
    {
        //1 Jan 1900 was a Monday.
        //Thirty days has September,
        //April, June and November.
        //All the rest have thirty-one,
        //Saving February alone,
        //Which has twenty-eight, rain or shine.
        //And on leap years, twenty-nine.
        //A leap year occurs on any year evenly divisible by 4, but not on a century unless it is divisible by 400.
        //How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?

        public override string Description => "019) Counting Sundays";

        [InputField]
        public DateTime StartDate { get; set; } = new DateTime(1901, 1, 1);

        [InputField]
        public DateTime EndDate { get; set; } = new DateTime(2000, 12, 31);

        [OutputField]
        public int SundayCount { get; set; } = 0;

        [ActionPerformer]
        public void Calculate()
        {
            int sundays = 0;

            for (int year = StartDate.Year; year <= EndDate.Year; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    if ((new DateTime(year, month, 1)).DayOfWeek == DayOfWeek.Sunday)
                    {
                        sundays++;
                    }
                }
            }

            SundayCount = sundays;
        }
    }
}
