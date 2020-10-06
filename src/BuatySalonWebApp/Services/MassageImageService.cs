using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class MassageImageService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MassageImageService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<MassageImage>> GetAllListAsync()
        {
            var result = await _applicationDbContext.MassageImages.ToListAsync();
            return result;
        }
        public void Save(MassageImage massageImage)
        {
            if (massageImage.Id == 0)
            {
                var result = _applicationDbContext.MassageImages.Add(massageImage);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.MassageImages.FirstOrDefault(z => z.Id == massageImage.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.MassageImages.Update(massageImage);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        public async Task DeleteAsync(int massageId)
        {
            var result = await _applicationDbContext.MassageImages.FirstOrDefaultAsync(e => e.Id == massageId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.MassageImages.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
