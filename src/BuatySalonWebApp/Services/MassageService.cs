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
    public class MassageService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MassageService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<MassageAllModel>> GetAllListAsync()
        {
            var result = await _applicationDbContext.MassageAllModels.Include(m=>m.MassageImages).OrderBy(m=>m.SortNumber).ToListAsync();
            return result;
        }
        public Task<MassageAllModel> GetById(Guid massageId)
        {
            var result = _applicationDbContext.MassageAllModels.Include(z=>z.MassageImages).FirstOrDefault(z => z.Id == massageId);
            return Task.FromResult(result);
        }
        public async Task DeleteAsync(Guid massageId)
        {
            var result = await _applicationDbContext.MassageAllModels.Include(e=>e.MassageImages).FirstOrDefaultAsync(e => e.Id == massageId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.MassageAllModels.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public Task<MassageAllModel> GetByUrl(string url)
        {
            var result = _applicationDbContext.MassageAllModels.Where(u => u.Url == url).Include(z => z.MassageImages).FirstOrDefault();
            return Task.FromResult(result);
        }
        public void Save(MassageAllModel massageAllModel, List<int> categoriesMassage)
        {
            if (Guid.Empty.Equals(massageAllModel.Id))
            {
                var maxElement = _applicationDbContext.MassageAllModels.Select(p => p.SortNumber).FirstOrDefault();
                if (maxElement == 0)
                {
                    massageAllModel.SortNumber = maxElement + 1;
                }
                else
                {
                    maxElement = _applicationDbContext.NailsAllModels.Max(p => p.SortNumber);
                    massageAllModel.SortNumber = maxElement + 1;
                }
                massageAllModel.Url = Regex.Replace(massageAllModel.Title, @"(\s+|@|&|'|\(|\)|<|>|#)","-").ToLower();
                var result = _applicationDbContext.MassageAllModels.Add(massageAllModel);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                    result.Entity.MassageAllModelCategories = new List<MassageAllModelCategoryAllMassageModel>();
                    foreach (var item in categoriesMassage)
                    {
                        result.Entity.MassageAllModelCategories.Add(new MassageAllModelCategoryAllMassageModel { MassageAllModelId = result.Entity.Id, CategoryMassageModelId= item });
                    }
                    _applicationDbContext.MassageAllModels.Update(result.Entity);
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.MassageAllModels.AsNoTracking().FirstOrDefault(z => z.Id == massageAllModel.Id);
                if (dbEntity != null)
                {
                    var servicesCategory = _applicationDbContext.MassageAllModels.Where(sc => sc.Id == massageAllModel.Id).ToList();
                    foreach (var item in servicesCategory)
                    {
                        _applicationDbContext.MassageAllModels.Remove(item);
                    }
                    massageAllModel.MassageAllModelCategories = new List<MassageAllModelCategoryAllMassageModel>();
                    foreach (var item in categoriesMassage)
                    {
                        massageAllModel.MassageAllModelCategories.Add(new MassageAllModelCategoryAllMassageModel { MassageAllModelId = massageAllModel.Id, CategoryMassageModelId = item });
                    }
                    _applicationDbContext.MassageAllModels.Update(massageAllModel);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        //save moved  element up & down
        public void SaveMassageAdmin(MassageAllModel massageAllModel)
        {
            if (Guid.Empty.Equals(massageAllModel.Id))
            {
                var maxElement = _applicationDbContext.MassageAllModels.Select(p => p.SortNumber).FirstOrDefault();
                if (maxElement == 0)
                {
                    massageAllModel.SortNumber = maxElement + 1;
                }
                else
                {
                    maxElement = _applicationDbContext.NailsAllModels.Max(p => p.SortNumber);
                    massageAllModel.SortNumber = maxElement + 1;
                }
                var result = _applicationDbContext.MassageAllModels.Add(massageAllModel);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.MassageAllModels.AsNoTracking().FirstOrDefault(z => z.Id == massageAllModel.Id);
                if (dbEntity != null)
                {
                    _applicationDbContext.MassageAllModels.Update(massageAllModel); 
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        public async Task<List<MassageAllModel>> GetCategoryIdAsync(int messageId)
        {
            var result = await _applicationDbContext.MassageAllModelCategoryAllMassageModels.Where(n => n.CategoryMassageModelId == messageId).Select(p => p.MassageAllModel).ToListAsync();
            return result;
        }
    }
}
