using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class ContactUsIconService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ContactUsIconService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<ContactUsIcon>> GetAllListAsync()
        {
           var result = await _applicationDbContext.ContactUsIcons.ToListAsync();
            return result;
        }
        public Task<ContactUsIcon> GetById(int iconsId)
        {
            var result = _applicationDbContext.ContactUsIcons.Where(i => i.Id == iconsId).FirstOrDefault();
            return Task.FromResult(result);
        }
        public async Task DeleteAsync(int iconsId)
        {
            var result = await _applicationDbContext.ContactUsIcons.Where(i => i.Id == iconsId).FirstOrDefaultAsync();
            if(result == null)
            {
                return;
            }
            _applicationDbContext.ContactUsIcons.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Save(ContactUsIcon contactUsIcon)
        {
            if(contactUsIcon.Id == 0)
            {
                var result = _applicationDbContext.ContactUsIcons.Add(contactUsIcon);
                if(result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.ContactUsIcons.AsNoTracking().FirstOrDefault(z => z.Id == contactUsIcon.Id);
                if(dbEntity != null)
                {
                    _applicationDbContext.ContactUsIcons.Update(contactUsIcon);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
