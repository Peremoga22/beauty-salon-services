using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class ContactUsService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContactUsService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<ContactUs>> GetAllListAsync()
        {
            var result = await _applicationDbContext.ContactUs.Include(f => f.ContactUsInfo).ToListAsync();
            return result;
        }
        public Task<ContactUs> GetById(Guid contactUsId)
        {
            var result = _applicationDbContext.ContactUs.Include(f => f.ContactUsInfo).Where(u => u.Id == contactUsId).SingleOrDefault();
            return Task.FromResult(result);
        }
        public async Task DeleteAsync(Guid contactUsId)
        {
            var result = await _applicationDbContext.ContactUs.SingleOrDefaultAsync(e => e.Id == contactUsId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.ContactUs.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<ContactUs> Edit(Guid contactUsId)
        {
            var result = _applicationDbContext.ContactUs.FirstOrDefault(e => e.Id == contactUsId);
            return await Task.FromResult(result);
        }
        public void Save(ContactUs contactUs)
        {
            if (Guid.Empty.Equals(contactUs.Id))
            {
                var result = _applicationDbContext.ContactUs.Add(contactUs);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                ContactUs dbEntry = _applicationDbContext.ContactUs.AsNoTracking().FirstOrDefault(z => z.Id == contactUs.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.ContactUs.Update(contactUs);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
