using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class ModelHairImageService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ModelHairImageService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<ServiceImage>> GetAllListAsync()
        {
            var result = await _applicationDbContext.serviceImages.ToListAsync();
            return result;
        }
        public void Save(ServiceImage serviceImage)
        {
            if (serviceImage.Id == 0)
            {
                var result = _applicationDbContext.serviceImages.Add(serviceImage);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.serviceImages.FirstOrDefault(z => z.Id == serviceImage.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.serviceImages.Update(serviceImage);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        public async Task DeleteAsync(int imageId)
        {
            var result = await _applicationDbContext.serviceImages.FirstOrDefaultAsync(e => e.Id == imageId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.serviceImages.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public Task<ServiceImage> GetById(int id)
        {
            var result = _applicationDbContext.serviceImages.Where(u => u.Id == id).FirstOrDefault();
            return Task.FromResult(result);
        }
    }
}
