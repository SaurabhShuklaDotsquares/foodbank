using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModalMapper
{
    public class VolunteerAvailabilityListDtoMapper
    {
        public static List<AvailabilityDto> AvaillibilityListgMap(List<VolunteerAvailability> availabilityItems)
        {
            var availabilityList = new List<AvailabilityDto>();
            foreach (var item in availabilityItems)
            {
                availabilityList.Add(new AvailabilityDto
                {
                    AvailabilityId = item.Id,
                    FromDate = item.FormDate.ToFormatCustomString(),
                    ToDate = item.ToDate.ToFormatCustomString(),
                    PatternName = MapPattern(item),
                    TimeForm = item.TimeForm != null ? item.TimeForm.Value.TimeSpanToString() : null,
                    TimeTo = item.TimeTo != null ? item.TimeTo.Value.TimeSpanToString() : null,
                    AllDay = item.AllDay,
                });
            }
            return availabilityList;
        }

        public static string MapPattern(VolunteerAvailability entity)
        {
            AvailabilityDto model = new AvailabilityDto();
            string patternName = string.Empty;
            if (entity.Pattern.IsNotNullAndNotEmpty())
            {

                if (entity.Pattern.StartsWith("D"))
                {
                    if (entity.Pattern.StartsWith("D1"))
                    {
                        Int32 dailydays = 0;
                        if (Int32.TryParse(entity.Pattern.Split("D1")[1], out dailydays) && dailydays > 0)
                        {
                            patternName = string.Format("Every {0}day",
                                (dailydays == 1 ? "" : (dailydays == 2 ? "other " : (dailydays + dailydays.ToOccurrenceSuffix() + " ")))
                                );
                        }
                    }

                    if (entity.Pattern.StartsWith("D2"))
                    {
                        patternName = "Every weekday";
                    }
                }
                else if (entity.Pattern.StartsWith("W"))
                {

                    if (entity.Pattern[1] == 'Y')
                        patternName += ", " + DayOfWeek.Monday.ToString();

                    if (entity.Pattern[2] == 'Y')
                        patternName += ", " + DayOfWeek.Tuesday.ToString();

                    if (entity.Pattern[3] == 'Y')
                        patternName += ", " + DayOfWeek.Wednesday.ToString();

                    if (entity.Pattern[4] == 'Y')
                        patternName += ", " + DayOfWeek.Thursday.ToString();

                    if (entity.Pattern[5] == 'Y')
                        patternName += ", " + DayOfWeek.Friday.ToString();

                    if (entity.Pattern[6] == 'Y')
                        patternName += ", " + DayOfWeek.Saturday.ToString();

                    if (entity.Pattern[7] == 'Y')
                        patternName += ", " + DayOfWeek.Sunday.ToString();

                    patternName = patternName.TrimStart(',').Trim();
                    StringBuilder sb = new StringBuilder(patternName);
                    int lastComma = patternName.LastIndexOf(',');
                    if (lastComma != -1)
                    {
                        sb.Remove(lastComma, 1);
                        sb.Insert(lastComma, " and");
                        patternName = sb.ToString();
                    }
                    //  patternName = "Every " + patternName;

                    Int32 weekdays = 0;
                    if (Int32.TryParse(entity.Pattern.Substring(8), out weekdays) && weekdays > 0)
                    {
                        patternName = string.Format("Every {0}week on {1}",
                                 (weekdays == 1 ? "" : (weekdays == 2 ? "other " : (weekdays + weekdays.ToOccurrenceSuffix() + " "))),
                                 patternName
                                 );
                    }
                }
                else if (entity.Pattern.StartsWith("M"))
                {
                    if (entity.Pattern.StartsWith("M1"))
                    {
                        Int32 monthlydays = 0;
                        Int32 monthlymonths = 0;
                        if ((Int32.TryParse(entity.Pattern.Substring(2, 2), out monthlydays) && monthlydays > 0) &&
                            (Int32.TryParse(entity.Pattern.Substring(4), out monthlymonths) && monthlymonths > 0)
                            )
                        {
                            patternName = string.Format("On the {0} day of every {1}month",
                                           monthlydays + monthlydays.ToOccurrenceSuffix(),
                                           (monthlymonths == 1 ? "" : (monthlymonths == 2) ? "other " : (monthlymonths + monthlymonths.ToOccurrenceSuffix() + " "))
                                           );
                        }

                    }

                    if (entity.Pattern.StartsWith("M2"))
                    {
                        Int32 monthlyweek = 0;
                        Int32 monthlyweekday = 0;
                        Int32 monthlyweekmonth = 0;
                        if ((Int32.TryParse(entity.Pattern.Substring(2, 1), out monthlyweek)) &&
                            (Int32.TryParse(entity.Pattern.Substring(3, 1), out monthlyweekday)) &&
                            (Int32.TryParse(entity.Pattern.Substring(4), out monthlyweekmonth) && monthlyweekmonth > 0))
                        {
                            patternName = string.Format("On the {0} of every {1}month",
                                          ((MonthWeek)monthlyweek).GetDescription().ToLower() + " " +
                                          ((WeekDay)monthlyweekday).GetDescription(),
                                         (monthlyweekmonth == 1 ? "" : (monthlyweekmonth == 2) ? "other " : (monthlyweekmonth + monthlyweekmonth.ToOccurrenceSuffix() + " "))
                                         );
                        }
                    }
                }
                else if (entity.Pattern.StartsWith("A"))
                {
                    Int32 annualtype = 0;
                    if (Int32.TryParse(entity.Pattern.Substring(3, 1), out annualtype))
                    {
                        if (annualtype == 1)
                        {

                            Int32 annualyears = 0;
                            Int32 annualmonth = 0;
                            Int32 annualmonthday = 0;
                            if ((Int32.TryParse(entity.Pattern.Substring(1, 2), out annualyears) && annualyears > 0) &&
                                (Int32.TryParse(entity.Pattern.Substring(4, 2), out annualmonth) && annualmonth > 0) &&
                                (Int32.TryParse(entity.Pattern.Substring(6), out annualmonthday) && annualmonthday > 0))
                            {
                                patternName = string.Format("Every {0}year on {1} {2}",
                                                     (annualyears == 1 ? "" : (annualyears == 2) ? "other " : (annualyears + annualyears.ToOccurrenceSuffix() + " ")),
                                                     ((Month)annualmonth).GetDescription(),
                                                     annualmonthday + annualmonthday.ToOccurrenceSuffix()
                                                     );
                            }
                        }

                        if (annualtype == 2)
                        {
                            Int32 annualyears = 0;
                            Int32 annualweek = 0;
                            Int32 annualweekday = 0;
                            Int32 annualmonthweek = 0;
                            if ((Int32.TryParse(entity.Pattern.Substring(1, 2), out annualyears) && annualyears > 0) &&
                                (Int32.TryParse(entity.Pattern.Substring(4, 1), out annualweek) && annualweek > 0) &&
                                (Int32.TryParse(entity.Pattern.Substring(5, 1), out annualweekday)) &&
                                (Int32.TryParse(entity.Pattern.Substring(6), out annualmonthweek) && annualmonthweek > 0)
                                )
                            {
                                patternName = string.Format("Every {0}year on {1} {2} of {3}",
                                                 (annualyears == 1 ? "" : (annualyears == 2) ? "other " : (annualyears + annualyears.ToOccurrenceSuffix() + " ")),
                                                 ((MonthWeek)annualweek).GetDescription().ToLower(),
                                                 ((WeekDay)annualweekday).GetDescription(),
                                                 ((Month)annualmonthweek).GetDescription()
                                                 );
                            }
                        }
                    }
                }

            }
            return patternName;
        }

    }
}
