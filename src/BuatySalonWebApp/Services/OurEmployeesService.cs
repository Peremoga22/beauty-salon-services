using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class OurEmployeesService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public OurEmployeesService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<OurEmployee>> GetAllListAsync()
        {
            var result = await _applicationDbContext.OurEmployees.ToListAsync();
            return result;
        }
        public Task<OurEmployee> GetById(int employeeId)
        {
            var result = _applicationDbContext.OurEmployees.Where(z => z.Id == employeeId).FirstOrDefault();
            return Task.FromResult(result);
        }
       
        public async Task DeleteAsync(int employeeId)
        {
            var result = await _applicationDbContext.OurEmployees.SingleOrDefaultAsync(e => e.Id == employeeId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.OurEmployees.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Save(OurEmployee employee)
        {
            if (employee.Id ==0)
            {
                var result = _applicationDbContext.OurEmployees.Add(employee);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.OurEmployees.AsNoTracking().FirstOrDefault(z => z.Id == employee.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.OurEmployees.Update(employee);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
