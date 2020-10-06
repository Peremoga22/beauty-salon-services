using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class CheackEmailsUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CheackEmailsUserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<CheackEmailUsers>> GetAllListAsync()
        {
            var result = await _applicationDbContext.CheackEmailUsers.ToListAsync();
            return result;
        }
        public Task<CheackEmailUsers> GetById(int emailId)
        {
            var result = _applicationDbContext.CheackEmailUsers.Where(e => e.Id == emailId).FirstOrDefault();
            return Task.FromResult(result);
        }
        public void Save(CheackEmailUsers cheackEmailUsers)
        {
            if(cheackEmailUsers.Id == 0)
            {
                var result = _applicationDbContext.CheackEmailUsers.Add(cheackEmailUsers);
                if(result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.CheackEmailUsers.FirstOrDefault(e => e.Id == cheackEmailUsers.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.CheackEmailUsers.Update(cheackEmailUsers);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        public async Task DeleteAsync(int emailId)
        {
            var result = await _applicationDbContext.CheackEmailUsers.Where(u => u.Id == emailId).FirstOrDefaultAsync();
            if (result == null)
            {
                return;
            }
            _applicationDbContext.CheackEmailUsers.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
