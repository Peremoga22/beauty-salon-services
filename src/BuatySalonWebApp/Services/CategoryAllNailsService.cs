using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class CategoryAllNailsService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryAllNailsService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<CategoryAllNailsModel>> GetAllListAsync()
        {
            var result = await _applicationDbContext.CategoryAllNails.Include(i => i.NailsAllModelCategories).ToListAsync();
            return result;
        }
        public Task<CategoryAllNailsModel> GetById(int categoryId)
        {
            var result = _applicationDbContext.CategoryAllNails.Where(u => u.Id == categoryId).SingleOrDefault();
            return Task.FromResult(result);
        }
        public async Task DeleteAsync(int categoryId)
        {
            var result = await _applicationDbContext.CategoryAllNails.SingleOrDefaultAsync(e => e.Id == categoryId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.CategoryAllNails.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Save(CategoryAllNailsModel  categoryAllNailsModel)
        {
            if (categoryAllNailsModel.Id == 0)
            {
                var result = _applicationDbContext.CategoryAllNails.Add(categoryAllNailsModel);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.CategoryAllNails.AsNoTracking().FirstOrDefault(z => z.Id == categoryAllNailsModel.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.CategoryAllNails.Update(categoryAllNailsModel);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
