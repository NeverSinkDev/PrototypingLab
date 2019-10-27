using FuncCollection.CommonExtensions;
using FuncCollection.Mathem.NumericSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncCollection.TimeUtils
{
    public static class DateUtils
    {
        public static HashSet<int> ThirtyDays { get; set; } = new HashSet<int>() { 9, 4, 6, 11 };

        public static int GetDaysBetween(DateTime start, DateTime end)
        {
            return NumericSources.YieldSequenceFromTo(start.Year, end.Year).ToList().SelectWithBorderCases(
                x => GetDaysInFullYear(x),
                x => start.DayOfYear,
                x => end.DayOfYear).Sum();
        }



        public static int GetDaysInFullYear(int year)
        {
            if (year % 4 == 0)
            {
                if (year % 100 == 0)
                {
                    if (year % 400 == 0)
                    {
                        return 364;
                    }

                    return 365;

                }

                return 364;
            }

            return 365;
        }

        public static int GetDaysInMonth(int month, int year)
        {
            if (ThirtyDays.Contains(month))
            {
                return 30;
            }

            if (month == 2)
            {
                if (year % 4 == 0)
                {
                    if (year % 100 == 0)
                    {
                        if (year % 400 == 0)
                        {
                            return 28;
                        }

                        return 29;

                    }

                    return 28;
                }

                return 29;
            }

            return 31;
        }
    }
}
