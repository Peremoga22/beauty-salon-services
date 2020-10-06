using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class NailsImageService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NailsImageService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<NailsImage>> GetAllListAsync()
        {
            var result = await _applicationDbContext.NailsImages.ToListAsync();
            return result;
        }
        public void Save(NailsImage nailsImage)
        {
            if (nailsImage.Id == 0)
            {
                var result = _applicationDbContext.NailsImages.Add(nailsImage);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.NailsImages.FirstOrDefault(z => z.Id == nailsImage.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.NailsImages.Update(nailsImage);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        public async Task DeleteAsync(int imageId)
        {
            var result = await _applicationDbContext.NailsImages.FirstOrDefaultAsync(e => e.Id == imageId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.NailsImages.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
