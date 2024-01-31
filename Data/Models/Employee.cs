using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeScheduling.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vardas yra privalomas.")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Neteisingas el. pašto adresas.")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Neteisingas telefono numeris.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Adresas yra privalomas.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Pareigos yra privalomos.")]
        public string JobTitle { get; set; }
        [Range(0, 100000, ErrorMessage = "Atlyginimo norma turi būti didesnė arba lygi 0.")]
        public string SalaryRate { get; set; }
        public ICollection<EmployeeJob> EmployeeJobs { get; set; } = new List<EmployeeJob>();
    }
}