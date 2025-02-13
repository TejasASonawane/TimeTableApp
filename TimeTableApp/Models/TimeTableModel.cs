using System.ComponentModel.DataAnnotations;

namespace TimeTableApp.Models
{
    public class TimeTableModel
    {
        [Required]
        [Range(1, 7, ErrorMessage = "Working days must be between 1 and 7.")]
        public int NoOfWorkingDays { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "Subjects per day must be less than 9.")]
        public int NoOfSubjectsPerDay { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total subjects must be a positive number.")]
        public int TotalSubjects { get; set; }

        public int TotalHoursForWeek => NoOfWorkingDays * NoOfSubjectsPerDay;
    }
}
