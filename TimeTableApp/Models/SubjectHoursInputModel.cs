using System.ComponentModel.DataAnnotations;

namespace TimeTableApp.Models
{
   
    public class SubjectHours
    {
        [Required]
        public string SubjectName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Hours must be a positive number.")]
        public int Hours { get; set; }
    }

    public class SubjectHoursInputModel
    {
        public List<SubjectHours> Subjects { get; set; }
        public int TotalHoursForWeek { get; set; }
    }
}
