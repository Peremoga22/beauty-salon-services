using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class ContactUsInfoService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContactUsInfoService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<ContactUsInfo>> GetAllListAsync()
        {
            var result = await _applicationDbContext.ContactUsInfos.Include(f => f.ContactUsIcon).ToListAsync();
            return result;
        }
        public Task<ContactUsInfo> GetById(int iconsId)
        {
            var result = _applicationDbContext.ContactUsInfos.Include(u => u.ContactUsIcon).Where(u => u.Id == iconsId).SingleOrDefault();
            return Task.FromResult(result);
        }
        public async Task DeleteAsync(int iconsId)
        {
            var result = await _applicationDbContext.ContactUsInfos.Include(u => u.ContactUsIcon).SingleOrDefaultAsync(e => e.Id == iconsId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.ContactUsInfos.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<ContactUsInfo> Edit(int iconsId)
        {
            var result = _applicationDbContext.ContactUsInfos.FirstOrDefault(e => e.Id == iconsId);
            return await Task.FromResult(result);
        }
        public void Save(ContactUsInfo contactUsInfo)
        {
            if (contactUsInfo.Id == 0)
            {
                var result = _applicationDbContext.ContactUsInfos.Add(contactUsInfo);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.ContactUsInfos.AsNoTracking().FirstOrDefault(z => z.Id == contactUsInfo.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.ContactUsInfos.Update(contactUsInfo);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
