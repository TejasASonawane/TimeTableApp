using System.ComponentModel.DataAnnotations;

namespace TimeTableApp.Models
{
    public class TimeTableModel
    {
        [Required]
        [Range(1, 7, ErrorMessage = "No of Working Days must be between 1 and 7.")]
        public int NoOfWorkingDays { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "No of Subjects per day must be between 1 and 8.")]
        public int NoOfSubjectsPerDay { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total Subjects must be greater than 0.")]
        public int TotalSubjects { get; set; }

        public int TotalHours => NoOfWorkingDays * NoOfSubjectsPerDay; 

        public List<SubjectHours> SubjectHoursList { get; set; } = new();
    }

    public class SubjectHours
    {
        public string SubjectName { get; set; }
        public int Hours { get; set; }
    }
}
