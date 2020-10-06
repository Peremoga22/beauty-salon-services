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
    public class NailsAllService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NailsAllService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<NailsAllModel>> GetAllListAsync()
        {
            var result = await _applicationDbContext.NailsAllModels.Include(n=>n.NailsImages).OrderBy(n=>n.SortNumber).ToListAsync();
            return result;
        }
        public async Task<NailsAllModel> GetByIdAsync(Guid nailsId)
        {
            var result =  _applicationDbContext.NailsAllModels.Include(u=>u.NailsImages).FirstOrDefault(u => u.Id == nailsId);
            return await Task.FromResult(result);
        }
        public Task<NailsAllModel> GetByUrl(string url)
        {
            var result = _applicationDbContext.NailsAllModels.Where(u => u.Url == url).Include(u=>u.NailsImages).FirstOrDefault();
            return Task.FromResult(result);
        }
        public void Save(NailsAllModel nailsAllModel, List<int> categories)
        {
            if (Guid.Empty.Equals(nailsAllModel.Id))
            {
                var maxElement = _applicationDbContext.NailsAllModels.Select(p => p.SortNumber).FirstOrDefault();
                if (maxElement == 0)
                {
                    nailsAllModel.SortNumber = maxElement + 1;
                }
                else
                {
                    maxElement = _applicationDbContext.NailsAllModels.Max(p => p.SortNumber);
                    nailsAllModel.SortNumber = maxElement + 1;
                }
                nailsAllModel.Url = Regex.Replace(nailsAllModel.Title, @"(\s+|@|&|'|\(|\)|<|>|#)", "-").ToLower();

                var result = _applicationDbContext.NailsAllModels.Add(nailsAllModel);
                if (result.State == EntityState.Added)
                {
                   _applicationDbContext.SaveChanges();
                    result.Entity.NailsAllModelCategories = new List<NailsAllModelCategoryAllNailsModel>();
                    foreach (var item in categories)
                    {
                        result.Entity.NailsAllModelCategories.Add(new NailsAllModelCategoryAllNailsModel { NailsAllModelId = result.Entity.Id, CategoryAllNailsModelId = item });
                    }
                    _applicationDbContext.NailsAllModels.Update(result.Entity);
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntry = _applicationDbContext.NailsAllModelCategories.AsNoTracking().FirstOrDefault(z => z.NailsAllModelId == nailsAllModel.Id);
                if (dbEntry != null)
                {
                    var nailsDb = _applicationDbContext.NailsAllModelCategories.Where(pc => pc.NailsAllModelId == nailsAllModel.Id).ToList();
                    foreach (var item in nailsDb)
                    {
                        _applicationDbContext.NailsAllModelCategories.Remove(item);
                    }
                    nailsAllModel.NailsAllModelCategories = new List<NailsAllModelCategoryAllNailsModel>();
                    foreach (var item in categories)
                    {
                        nailsAllModel.NailsAllModelCategories.Add(new NailsAllModelCategoryAllNailsModel { NailsAllModelId = nailsAllModel.Id, CategoryAllNailsModelId = item });
                    }
                    _applicationDbContext.NailsAllModels.Update(nailsAllModel);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
        //save moved  element up & down
        public void SaveNailAdmin(NailsAllModel nailsAllModel)
        {
            if (Guid.Empty.Equals(nailsAllModel.Id))
            {
                var maxElement = _applicationDbContext.NailsAllModels.Select(p => p.SortNumber).FirstOrDefault();
                if (maxElement == 0)
                {
                    nailsAllModel.SortNumber = maxElement + 1;
                }
                else
                {
                    maxElement = _applicationDbContext.NailsAllModels.Max(p => p.SortNumber);
                    nailsAllModel.SortNumber = maxElement + 1;
                }
                var result = _applicationDbContext.NailsAllModels.Add(nailsAllModel);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                var dbEntity = _applicationDbContext.NailsAllModels.AsNoTracking().FirstOrDefault(z => z.Id == nailsAllModel.Id);
                if (dbEntity != null)
                {
                    _applicationDbContext.NailsAllModels.Update(nailsAllModel);
                    _applicationDbContext.SaveChanges();
                }
            }
        }

        public async Task DeleteAsync(Guid nailsId)
        {
            var result = await _applicationDbContext.NailsAllModels.Include(e=>e.NailsImages).SingleOrDefaultAsync(e => e.Id == nailsId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.NailsAllModels.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<List<NailsAllModel>> GetCategoryIdAsync(int categoryId)
        {
            var result = await _applicationDbContext.NailsAllModelCategories.Where(n => n.CategoryAllNailsModelId == categoryId).Select(p => p.NailsAllModel).ToListAsync();
            return result;
        }
    }
}
