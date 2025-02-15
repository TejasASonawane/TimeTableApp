using Microsoft.AspNetCore.Mvc;
using TimeTableApp.Models;

namespace TimeTableApp.Controllers
{
    public class TimeTableController : Controller
    {
        public IActionResult Index()
        {
            return View(new TimeTableModel());
        }

        [HttpPost]
        public IActionResult SubmitInput(TimeTableModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            TempData["NoOfWorkingDays"] = model.NoOfWorkingDays;
            TempData["NoOfSubjectsPerDay"] = model.NoOfSubjectsPerDay;
            TempData["TotalSubjects"] = model.TotalSubjects;
            TempData["TotalHours"] = model.TotalHours;

            return RedirectToAction("AddSubjectHours");
        }
        public IActionResult AddSubjectHours()
        {
            var model = new TimeTableModel
            {
                TotalSubjects = (int)TempData["TotalSubjects"],
                NoOfWorkingDays = (int)TempData["NoOfWorkingDays"],
                NoOfSubjectsPerDay = (int)TempData["NoOfSubjectsPerDay"],
                SubjectHoursList = new List<SubjectHours>()
            };

            for (int i = 0; i < model.TotalSubjects; i++)
            {
                model.SubjectHoursList.Add(new SubjectHours());
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult GenerateTimeTable(TimeTableModel model)
        {
            int enteredTotalHours = model.SubjectHoursList.Sum(s => s.Hours);
            int requiredTotalHours = model.NoOfWorkingDays * model.NoOfSubjectsPerDay;

            if (enteredTotalHours != requiredTotalHours)
            {
                ModelState.AddModelError("", "Total hours must be equal to Total Hours for Week.");
                return View("AddSubjectHours", model);
            }

            return View("TimeTable", model);
        }
    }
}
