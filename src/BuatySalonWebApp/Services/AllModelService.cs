using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class AllModelService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AllModelService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<ModelService>> GetAllListAsync()
        {
            var result = await _applicationDbContext.ModelServices.OrderBy(s=>s.SortNumber).ToListAsync();
            return result;
        }
        public Task<ModelService> GetById(Guid serviceId)
        {
            var result = _applicationDbContext.ModelServices.Where(z => z.Id == serviceId).SingleOrDefault();
            return Task.FromResult(result);
        }
        public Task<ModelService> GetByUrl(string url)
        {
            var result = _applicationDbContext.ModelServices.Where(u => u.Url == url).Include(u=>u.ServiceCategories).Include(u=>u.ServiceImage).FirstOrDefault();
            return Task.FromResult(result);
        }
        public void Save(ModelService modelService,List<int> categories)
        {
            if(Guid.Empty.Equals(modelService.Id))
            {
                var maxElement = _applicationDbContext.ModelServices.Select(p => p.SortNumber).FirstOrDefault();
                if (maxElement == 0)
                {
                    modelService.SortNumber = maxElement + 1;
                }
                else
                {
                    maxElement = _applicationDbContext.ModelServices.Max(p => p.SortNumber);
                    modelService.SortNumber = maxElement + 1;
                }
                modelService.Url = Regex.Replace(modelService.Title, @"(\s+|@|&|'|\(|\)|<|>|#)", "-").ToLower();
                var result = _applicationDbContext.ModelServices.Add(modelService);
                if(result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                    result.Entity.ServiceCategories = new List<ServiceCategory>();
                    foreach(var item  in categories)
                    {
                        result.Entity.ServiceCategories.Add(new ServiceCategory { ServiceId = result.Entity.Id, CategoryId = item });
                    }
                    _applicationDbContext.ModelServices.Update(result.Entity);
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.ModelServices.AsNoTracking().FirstOrDefault(z => z.Id == modelService.Id);
                if(dbEntity!=null)
                {
                    var servicesCategory = _applicationDbContext.ServiceCategories.Where(sc => sc.ServiceId == modelService.Id).ToList();
                    foreach(var item in servicesCategory)
                    {
                        _applicationDbContext.ServiceCategories.Remove(item);
                    }
                    modelService.ServiceCategories = new List<ServiceCategory>();
                    foreach(var item in categories)
                    {
                        modelService.ServiceCategories.Add(new ServiceCategory { ServiceId = modelService.Id, CategoryId = item });
                    }
                    _applicationDbContext.ModelServices.Update(modelService);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        // save moved up & down
        public void SaveModelAdminService(ModelService modelService)
        {
            if (Guid.Empty.Equals(modelService.Id))
            {
                var maxElement = _applicationDbContext.ModelServices.Select(p => p.SortNumber).FirstOrDefault();
                if (maxElement == 0)
                {
                    modelService.SortNumber = maxElement + 1;
                }
                else
                {
                    maxElement = _applicationDbContext.ModelServices.Max(p => p.SortNumber);
                    modelService.SortNumber = maxElement + 1;
                }
                var result = _applicationDbContext.ModelServices.Add(modelService);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.ModelServices.FirstOrDefault(z => z.Id == modelService.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.ModelServices.Update(modelService);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
       
        public async Task<List<ModelService>> GetByCategoryIdAsync(int categoryId)
        {
            var result = await _applicationDbContext.ServiceCategories.Where(z => z.CategoryId == categoryId).Select(z => z.ModelService).ToListAsync();
            return result;
        }
        public async Task DeleteAsync(Guid serviceId)
        {
            var result = await _applicationDbContext.ModelServices.SingleOrDefaultAsync(e => e.Id == serviceId);
            if(result == null)
            {
                return;
            }
            _applicationDbContext.ModelServices.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
