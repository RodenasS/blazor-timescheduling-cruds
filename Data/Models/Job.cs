using System.ComponentModel.DataAnnotations;

namespace TimeScheduling.Data.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Pavadinimas yra privalomas.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Aprašymas yra privalomas.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Pradžios data yra privaloma.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Pabaigos data yra privaloma.")]
        public DateTime EndDate { get; set; }
        public ICollection<EmployeeJob> EmployeeJobs { get; set; } = new List<EmployeeJob>();
    }
}