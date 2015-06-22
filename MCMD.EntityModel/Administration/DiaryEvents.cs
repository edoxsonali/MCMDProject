using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using MCMD.Common.CommonClass;


namespace MCMD.EntityModel.Administration
{
    public class DiaryEvent
    {

        public int ID;
        public string Title;
        public int SomeImportantKeyID;
        public string StartDateString;
        public string EndDateString;
        public string StatusString;
        public string StatusColor;
        public string ClassName;




        public static List<DiaryEvent> LoadAllAppointmentsInDateRange(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            using (ApplicationDbContext ent = new ApplicationDbContext())
            {
                //var rslt = ent.AppointmentDiary.Where(s => s.DateTimeScheduled >= fromDate && System.Data.Objects.EntityFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate);
                var rslt = ent.SchedulingDiarys.Where(s => s.DateTimeScheduled >= fromDate && s.DateTimeScheduled <= toDate);

                List<DiaryEvent> result = new List<DiaryEvent>();
                foreach (var item in rslt)
                {
                    DiaryEvent rec = new DiaryEvent();

                    rec.ID = item.ID;
                    rec.SomeImportantKeyID = item.SomeImportantKey;
                    rec.StartDateString = item.DateTimeScheduled.ToString(); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                    rec.EndDateString = item.DateTimeScheduled.AddMinutes(item.AppointmentLength).ToString("s"); // field AppointmentLength is in minutes
                    // rec.Title = item.Title + " - " + item.AppointmentLength.ToString() + " mins";
                
                    rec.Title = (item.StartTime+" "+item.StartSlot +" To "+item.EndTime+" "+item.EndSlot).ToString();
                    rec.StatusString = Enums.GetName<AppointmentStatus>((AppointmentStatus)item.StatusENUM);
                    rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);
                    string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                    rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                    rec.StatusColor = ColorCode;
                    result.Add(rec);

                }

                return result;
            }

        }


        public static List<DiaryEvent> LoadAppointmentSummaryInDateRange(double start, double end)
        {

            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            using (ApplicationDbContext ent = new ApplicationDbContext())
            {
                //var rslt = ent.AppointmentDiary.Where(s => s.DateTimeScheduled >= fromDate && System.Data.Objects.EntityFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate)
                //                                        .GroupBy(s => System.Data.Objects.EntityFunctions.TruncateTime(s.DateTimeScheduled))
                //                                        .Select(x => new { DateTimeScheduled = x.Key, Count = x.Count() });

                var rslt = ent.AppointmentDiary.Where(s => s.DateTimeScheduled >= fromDate && s.DateTimeScheduled <= toDate);

                List<DiaryEvent> result = new List<DiaryEvent>();
                int i = 0;
                foreach (var item in rslt)
                {
                    DiaryEvent rec = new DiaryEvent();
                    rec.ID = i; //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                    rec.SomeImportantKeyID = -1;
                    string StringDate = string.Format("{0:yyyy-MM-dd}", item.DateTimeScheduled);
                    rec.StartDateString = StringDate + "T00:00:00"; //ISO 8601 format
                    rec.EndDateString = StringDate + "T23:59:59";
                    //  rec.Title = "Booked: " + item.Count.ToString();
                    rec.Title = "Booked: ";
                    result.Add(rec);
                    i++;

                    //rec.ID = item.ID;
                    //rec.SomeImportantKeyID = item.SomeImportantKey;
                    //rec.StartDateString = item.DateTimeScheduled.ToString(); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                    //rec.EndDateString = item.DateTimeScheduled.AddMinutes(item.AppointmentLength).ToString("s"); // field AppointmentLength is in minutes
                    //// rec.Title = item.Title + " - " + item.AppointmentLength.ToString() + " mins";

                    //rec.Title = (item.StartTime + " " + item.StartSlot + " To " + item.EndTime + " " + item.EndSlot).ToString();
                    //rec.StatusString = Enums.GetName<AppointmentStatus>((AppointmentStatus)item.StatusENUM);
                    //rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);
                    //string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                    //rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                    //rec.StatusColor = ColorCode;
                    //result.Add(rec);



                }

                return result;
            }

        }

        public static void UpdateDiaryEvent(int id, string NewEventStart, string NewEventEnd)
        {
            // EventStart comes ISO 8601 format, eg:  "2000-01-10T10:00:00Z" - need to convert to DateTime
            using (ApplicationDbContext ent = new ApplicationDbContext())
            {
                var rec = ent.SchedulingDiarys.FirstOrDefault(s => s.ID == id);
                if (rec != null)
                {
                    DateTime startDate = DateTime.Parse(NewEventStart);
                    DateTime endDate = DateTime.Parse(NewEventEnd);

                    DateTime DateTimeStart = DateTime.Parse(NewEventStart, null, DateTimeStyles.RoundtripKind).ToLocalTime(); // and convert offset to localtime
                    rec.DateTimeScheduled = DateTimeStart;
                    if (!String.IsNullOrEmpty(NewEventEnd))
                    {
                        TimeSpan span = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime() - DateTimeStart;
                       // rec.AppointmentLength = Convert.ToInt32(span.TotalMinutes);

                        rec.AppointmentLength = 20;
                        //sonali start code
                        rec.DateTimeScheduledEnd = DateTime.Parse(NewEventEnd);
                        while (startDate < endDate)
                        {                         
                            startDate = startDate.AddDays(1);
                            rec.Title = rec.Title;
                            rec.DateTimeScheduled = startDate;
                            rec.StartTime = rec.StartTime;
                            rec.StartSlot = rec.StartSlot;
                            rec.EndTime = rec.EndTime;
                            rec.EndSlot = rec.EndSlot;
                            rec.DateTimeScheduledEnd = endDate;
                            ent.SchedulingDiarys.Add(rec);
                            ent.SaveChanges();
                          
                        }
                        //end code
                    }

                   // ent.SaveChanges();
                }
            }

        }

        public static bool DeleteDiaryEvent(string NewDeleteEventId)
        {

            try
            {
                using (ApplicationDbContext ent = new ApplicationDbContext())
                {
                    int id = Convert.ToInt32(NewDeleteEventId);
                    SchedulingDiary rec = ent.SchedulingDiarys.Find(id);

                    if (rec != null)
                    {
                        ent.SchedulingDiarys.Remove(rec);
                        ent.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }



        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {

                 var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        //public static bool CreateNewEvent(string NewEventDate, string NewEventStart, string NewEventEnd)
        //{
        //    try
        //    {
        //        ApplicationDbContext ent = new ApplicationDbContext();
        //        SchedulingDiary rec = new SchedulingDiary();

        //        //strat timing
        //        DateTime timestartSat = DateTime.Parse(NewEventStart);
        //        string newstartSat = timestartSat.ToString("hh:mm");
        //        string newslotstartSat = timestartSat.ToString("tt");

        //        //End timing
        //        DateTime timeendSat = DateTime.Parse(NewEventEnd);
        //        string newsendSat = timeendSat.ToString("hh:mm");
        //        string newsslotendSat = timeendSat.ToString("tt");


        //        rec.LoginId = 4;

        //        rec.DateTimeScheduled = DateTime.ParseExact(NewEventDate, "dd/MM/yyyy", null);
        //        rec.StartTime = TimeSpan.Parse(newstartSat);
        //        rec.EndTime = TimeSpan.Parse(newsendSat);
        //        rec.StartSlot = newslotstartSat;
        //        rec.EndSlot = newsslotendSat;
        //        ent.SchedulingDiarys.Add(rec);
        //        ent.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    return true;
        //}


        public static bool CreateNewEvent(string Title,string NewEventDate, string NewEventTime, string NewEventDuration, string NeweventStartTime, string NeweventEndTime, string NewCurrentDate)
        {
            try
            {
                ApplicationDbContext ent = new ApplicationDbContext();
                SchedulingDiary rec = new SchedulingDiary();

                // strat timing
                DateTime timestartSat = DateTime.Parse(NeweventStartTime);
                string newstartSat = timestartSat.ToString("hh:mm");
                string newslotstartSat = timestartSat.ToString("tt");

                //End timing
                DateTime timeendSat = DateTime.Parse(NeweventEndTime);
                string newsendSat = timeendSat.ToString("hh:mm");
                string newsslotendSat = timeendSat.ToString("tt");


                rec.Title = Title;
                rec.DateTimeScheduled = DateTime.ParseExact(NewEventDate + " " + NewEventTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                rec.AppointmentLength = Int32.Parse(NewEventDuration);
                rec.StartTime = TimeSpan.Parse(newstartSat);
                rec.StartSlot = newslotstartSat;
                rec.EndTime = TimeSpan.Parse(newsendSat);
                rec.EndSlot = newsslotendSat;
                ent.SchedulingDiarys.Add(rec);
                ent.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
