using Microsoft.AspNetCore.Mvc;
using TimeTableApp.Models;

namespace TimeTableApp.Controllers
{
    public class TimeTableController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new TimeTableModel());
        }

        [HttpPost]
        public IActionResult GenerateSubjectHours(TimeTableModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            TempData["TotalHoursForWeek"] = model.TotalHoursForWeek;
            TempData["TotalSubjects"] = model.TotalSubjects;

            return RedirectToAction("SubjectHours");
        }

        [HttpGet]
        public IActionResult SubjectHours()
        {
            int totalSubjects = TempData["TotalSubjects"] != null ? (int)TempData["TotalSubjects"] : 0;
            int totalHoursForWeek = TempData["TotalHoursForWeek"] != null ? (int)TempData["TotalHoursForWeek"] : 0;

            if (totalSubjects == 0 || totalHoursForWeek == 0)
                return RedirectToAction("Index");

            ViewBag.TotalHoursForWeek = totalHoursForWeek;

            return View(new SubjectHoursInputModel { Subjects = new List<SubjectHours>(totalSubjects) });
        }

        [HttpPost]
        public IActionResult GenerateTimeTable(SubjectHoursInputModel model)
        {
            int totalHoursForWeek = model.Subjects.Sum(s => s.Hours);

            if (totalHoursForWeek != model.TotalHoursForWeek)
            {
                ModelState.AddModelError("", $"Total hours must be exactly {model.TotalHoursForWeek}.");
                return View("SubjectHours", model);
            }

            TempData["TimeTableData"] = model.Subjects;
            return RedirectToAction("TimeTable");
        }

        [HttpGet]
        public IActionResult TimeTable()
        {
            var subjects = TempData["TimeTableData"] as List<SubjectHours>;
            if (subjects == null) return RedirectToAction("Index");

            return View(subjects);
        }
    }
}
