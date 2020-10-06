using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class FashionService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FashionService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }         
        public async Task<List<Fashion>> GetAllListAync()
        {
            var result = await _applicationDbContext.Fashions.OrderBy(m=>m.SortNumber).ToListAsync();
            return  result;
        }
        public Task<Fashion> GetById(Guid fashionId)
        {
            var result = _applicationDbContext.Fashions.Where(z => z.Id == fashionId).SingleOrDefault();
            return Task.FromResult(result);
        }
        public void Save(Fashion fashion)
        {
            if (Guid.Empty.Equals(fashion.Id))
            {
                var maxElement = _applicationDbContext.Fashions.Select(p => p.SortNumber).FirstOrDefault();
                if (maxElement == 0)
                {
                    fashion.SortNumber = maxElement + 1;
                }
                else
                {
                    maxElement = _applicationDbContext.Fashions.Max(p => p.SortNumber);
                    fashion.SortNumber = maxElement + 1;
                }
                var result = _applicationDbContext.Fashions.Add(fashion);
                if(result.State==EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.Fashions.AsNoTracking().FirstOrDefault(z => z.Id == fashion.Id);
                if (dbEntity != null)
                {
                    _applicationDbContext.Fashions.Update(fashion);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        public async Task DeleteFashionAsync(Guid fashionId)
        {
            var result = await _applicationDbContext.Fashions.Where(z => z.Id == fashionId).SingleOrDefaultAsync();
            if(result==null)
            {
                return;
            }
            _applicationDbContext.Fashions.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
