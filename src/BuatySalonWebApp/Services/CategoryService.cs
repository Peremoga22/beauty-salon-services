using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<Category>> GetAllListAsync()
        {
            var result = await _applicationDbContext.Categories.Include(c=>c.ServiceCategories).ToListAsync();
            return result;
        }
        public Task<Category> GetById(int categoryId)
        {
            var result =  _applicationDbContext.Categories.Include(c=>c.ServiceCategories).Where(z => z.Id == categoryId).SingleOrDefault();
            return Task.FromResult(result);
        }
        public async Task DeleteAsync(int categoryId)
        {
            var result = await _applicationDbContext.Categories.SingleOrDefaultAsync(e => e.Id == categoryId);
            if(result == null)
            {
                return;
            }
            _applicationDbContext.Categories.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<Category> Edit(int categoryId)
        {
            var result = _applicationDbContext.Categories.FirstOrDefault(e => e.Id == categoryId);
            return await Task.FromResult(result);
        }
        public void Save(Category category)
        {
            if(category.Id==0)
            {
                var result = _applicationDbContext.Categories.Add(category);
                if(result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.Categories.AsNoTracking().FirstOrDefault(z => z.Id == category.Id);
                if(dbEntity!=null)
                {
                    _applicationDbContext.Categories.Update(category);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
