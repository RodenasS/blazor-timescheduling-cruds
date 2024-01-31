using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeScheduling.Data;
using TimeScheduling.Data.Models;

namespace TimeScheduling.Services
{
    public class EmployeeJobsService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeJobsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeJob>> GetEmployeeJobsAsync()
        {
            return await _context.EmployeeJobs.ToListAsync();
        }

        public async Task CreateEmployeeJobAsync(EmployeeJob employeeJob)
        {
            _context.EmployeeJobs.Add(employeeJob);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeJobAsync(EmployeeJob employeeJob)
        {
            _context.EmployeeJobs.Update(employeeJob);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeJob?> GetEmployeeJobAsync(int employeeId, int jobId)
        {
            return await _context.EmployeeJobs
                .FirstOrDefaultAsync(ej => ej.EmployeeId == employeeId && ej.JobId == jobId);
        }

        public async Task<List<Job>> GetJobsForEmployeeAsync(int employeeId)
        {
            return await _context.EmployeeJobs
                .Where(ej => ej.EmployeeId == employeeId)
                .Select(ej => ej.Job)
                .ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesForJobAsync(int jobId)
        {
            return await _context.EmployeeJobs
                .Where(ej => ej.JobId == jobId)
                .Select(ej => ej.Employee)
                .ToListAsync();
        }

        public async Task RemoveEmployeeJobAsync(int employeeId, int jobId)
        {
            var employeeJob = await GetEmployeeJobAsync(employeeId, jobId);

            if (employeeJob != null)
            {
                _context.EmployeeJobs.Remove(employeeJob);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AssignEmployeesToJobAsync(int jobId, List<int> employeeIds)
        {
            var existingAssociations = await _context.EmployeeJobs
                .Where(ej => ej.JobId == jobId)
                .ToListAsync();
            
            foreach (var employeeId in employeeIds)
            {
                var newAssociation = new EmployeeJob
                {
                    EmployeeId = employeeId,
                    JobId = jobId
                };

                _context.EmployeeJobs.Add(newAssociation);
            }

            await _context.SaveChangesAsync();
        }
        
    }
}