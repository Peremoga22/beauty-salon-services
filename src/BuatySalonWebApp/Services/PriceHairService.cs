using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class PriceHairService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PriceHairService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<PriceHair>> GetAllListAsync()
        {
            var result = await _applicationDbContext.PriceHairs.ToListAsync();
            return result;
        }
        public Task<PriceHair> GetById(int priceHairId)
        {
            var result = _applicationDbContext.PriceHairs.Where(z => z.Id == priceHairId).FirstOrDefault();
            return Task.FromResult(result);
        }
        public async Task DeleteAsync(int priceHairId)
        {
            var result = await _applicationDbContext.PriceHairs.FirstOrDefaultAsync(e => e.Id == priceHairId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.PriceHairs.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Save(PriceHair priceHair)
        {
            if (priceHair.Id == 0)
            {
                var result = _applicationDbContext.PriceHairs.Add(priceHair);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.PriceHairs.AsNoTracking().FirstOrDefault(z => z.Id == priceHair.Id);
                if (dbEntity != null)
                {
                    _applicationDbContext.PriceHairs.Update(priceHair);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
