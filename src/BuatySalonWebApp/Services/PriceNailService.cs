using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class PriceNailService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PriceNailService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<PriceNail>> GetAllListAsync()
        {
            var result = await _applicationDbContext.PriceNails.ToListAsync();
            return result;
        }
        public Task<PriceNail> GetById(int priceNailId)
        {
            var result = _applicationDbContext.PriceNails.Where(z => z.Id == priceNailId).FirstOrDefault();
            return Task.FromResult(result);
        }
        public async Task DeleteAsync(int priceNailId)
        {
            var result = await _applicationDbContext.PriceNails.FirstOrDefaultAsync(e => e.Id == priceNailId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.PriceNails.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Save(PriceNail priceNail)
        {
            if (priceNail.Id == 0)
            {
                var result = _applicationDbContext.PriceNails.Add(priceNail);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.PriceNails.AsNoTracking().FirstOrDefault(z => z.Id == priceNail.Id);
                if (dbEntity != null)
                {
                    _applicationDbContext.PriceNails.Update(priceNail);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
