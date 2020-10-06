using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class CategoryAllMassageService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryAllMassageService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<CategoryMassageModel>> GetAllListAsync()
        {
            var result = await _applicationDbContext.CategoryMassageModels.Include(c => c.MassageAllModelCategory).ToListAsync();
            return result;
        }
      
        public Task<CategoryMassageModel> GetById(int categoryId)
        {
            var result = _applicationDbContext.CategoryMassageModels.Where(u => u.Id == categoryId).SingleOrDefault();
            return Task.FromResult(result);
        }
        public async Task DeleteAsync(int categoryId)
        {
            var result = await _applicationDbContext.CategoryMassageModels.SingleOrDefaultAsync(e => e.Id == categoryId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.CategoryMassageModels.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Save(CategoryMassageModel categoryMassageModel)
        {
            if (categoryMassageModel.Id == 0)
            {
                var result = _applicationDbContext.CategoryMassageModels.Add(categoryMassageModel);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.CategoryMassageModels.AsNoTracking().FirstOrDefault(z => z.Id == categoryMassageModel.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.CategoryMassageModels.Update(categoryMassageModel);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
