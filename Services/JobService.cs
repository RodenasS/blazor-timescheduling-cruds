using Microsoft.EntityFrameworkCore;
using TimeScheduling.Data;
using TimeScheduling.Data.Models;

namespace TimeScheduling.Services
{
    public class JobService
    {
        private readonly ApplicationDbContext _context;

        public JobService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task CreateJobAsync(Job job)
        {
            Console.WriteLine(job.Title);
            Console.WriteLine(job.Id);
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
        }

        public async Task<Job?> GetJobByIdAsync(int id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(job => job.Id == id);
        }

        public async Task<List<Employee>> GetEmployeesAssignedToJobAsync(int jobId)
        {
            return await _context.EmployeeJobs
                .Where(employeeJob => employeeJob.JobId == jobId)
                .Select(employeeJob => employeeJob.Employee)
                .ToListAsync();
        }

        public async Task AssignEmployeesToJobAsync(int jobId, List<int> employeeIds)
        {
            var job = await GetJobByIdAsync(jobId);

            if (job == null)
            {
                return;
            }

            foreach (var employeeId in employeeIds)
            {
                var employee = await _context.Employees.FindAsync(employeeId);

                if (employee != null)
                {
                    if (!_context.EmployeeJobs.Any(ej => ej.EmployeeId == employeeId && ej.JobId == jobId))
                    {
                        _context.EmployeeJobs.Add(new EmployeeJob
                        {
                            EmployeeId = employee.Id,
                            JobId = jobId
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        public decimal CalculateSalary(Job job)
        {
            TimeSpan timeWorked = job.EndDate - job.StartDate;
            double hoursWorked = timeWorked.TotalHours;

            var assignedEmployees = GetEmployeesAssignedToJobAsync(job.Id).Result;

            decimal totalSalary =
                assignedEmployees.Sum(employee => decimal.Parse(employee.SalaryRate) * (decimal)hoursWorked);

            return totalSalary;
        }
        
        public async Task UpdateJobAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteJobAsync(int jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);

            if (job != null)
            {
                var employeeJobs = _context.EmployeeJobs.Where(ej => ej.JobId == jobId);
                _context.EmployeeJobs.RemoveRange(employeeJobs);

                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }
    }
}