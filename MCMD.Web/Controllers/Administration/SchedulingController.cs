using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.EntityModel.Administration;


namespace MCMD.Web.Controllers.Administration
{
    public class SchedulingController : Controller
    {
        // GET: Scheduling
        
        public ActionResult Scheduling()
        {
            @TempData["Name"] = Session["Name"];
            return View();
        }

     
        public void UpdateEvent(int id, string NewEventStart, string NewEventEnd)
        {
            DiaryEvent.UpdateDiaryEvent(id, NewEventStart, NewEventEnd);
        }

        public bool DeleteEvent(string NewDeleteEventId)
        {
            return DiaryEvent.DeleteDiaryEvent(NewDeleteEventId);
        }


        public bool SaveEvent(string NewEventDate,string NeweventStartTime, string NeweventEndTime, string NewCurrentDate)
        {
            return DiaryEvent.CreateNewEvent(NewEventDate, NeweventStartTime, NeweventEndTime, NewCurrentDate);
        }

        public JsonResult GetDiarySummary(double start, double end)
        {
            var ApptListForDate = DiaryEvent.LoadAppointmentSummaryInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                              //  someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiaryEvents(double start, double end)
        {
            var ApptListForDate = DiaryEvent.LoadAllAppointmentsInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                               // end = e.EndDateString,
                                color = e.StatusColor,
                                className = e.ClassName,
                               // someKey = e.SomeImportantKeyID,
                                statue=e.StatusString,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }


    }
}