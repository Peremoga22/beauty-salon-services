using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class PriceMassageService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PriceMassageService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<PriceMassage>> GetAllListAsync()
        {
            var result = await _applicationDbContext.PriceMassages.ToListAsync();
            return result;
        }
        public Task<PriceMassage> GetById(int priceMassageId)
        {
            var result = _applicationDbContext.PriceMassages.Where(z => z.Id == priceMassageId).FirstOrDefault();
            return Task.FromResult(result);
        }
        public void Save(PriceMassage priceMassage)
        {
            if (priceMassage.Id == 0)
            {
                var result = _applicationDbContext.PriceMassages.Add(priceMassage);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.PriceMassages.AsNoTracking().FirstOrDefault(z => z.Id == priceMassage.Id);
                if (dbEntity != null)
                {
                    _applicationDbContext.PriceMassages.Update(priceMassage);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        public async Task DeleteAsync(int priceMassageId)
        {
            var result = await _applicationDbContext.PriceMassages.FirstOrDefaultAsync(e => e.Id == priceMassageId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.PriceMassages.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
