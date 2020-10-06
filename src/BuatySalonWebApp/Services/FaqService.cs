using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class FaqService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FaqService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<FrequentlyAskedQuestion>> GetAllListAsync()
        {
            var result = await _applicationDbContext.FrequentlyAskedQuestions.ToListAsync();
            return result;
        }
        public async Task<FrequentlyAskedQuestion> GetById(Guid faqId)
        {
            var result = await _applicationDbContext.FrequentlyAskedQuestions.Where(u => u.Id == faqId).SingleOrDefaultAsync();
            return result;
        }
        public async Task DeleteAsync(Guid faqId)
        {
            var result = await _applicationDbContext.FrequentlyAskedQuestions.SingleOrDefaultAsync(e => e.Id == faqId);
            if (result == null)
            {
                return;
            }
            _applicationDbContext.FrequentlyAskedQuestions.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<FrequentlyAskedQuestion> Edit(Guid faqId)
        {
            var result = _applicationDbContext.FrequentlyAskedQuestions.FirstOrDefault(e => e.Id == faqId);
            return await Task.FromResult(result);
        }
        public void Save(FrequentlyAskedQuestion faqId)
        {
            if (Guid.Empty.Equals(faqId.Id))
            {
                var result = _applicationDbContext.FrequentlyAskedQuestions.Add(faqId);
                if (result.State == EntityState.Added)
                {
                    _applicationDbContext.SaveChanges();
                }
            }
            else
            {
                FrequentlyAskedQuestion dbEntry = _applicationDbContext.FrequentlyAskedQuestions.AsNoTracking().FirstOrDefault(z => z.Id == faqId.Id);
                if (dbEntry != null)
                {
                    _applicationDbContext.FrequentlyAskedQuestions.Update(faqId);
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
