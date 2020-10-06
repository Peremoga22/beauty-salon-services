using BuatySalonWebApp.Data;
using BuatySalonWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Services
{
    public class IdentityService
    {
        private UserManager<AppUser> userManager;
        private readonly ApplicationDbContext _appIdentityDbContext;
        public IdentityService(UserManager<AppUser> usrMgr, ApplicationDbContext applicationDbContext)
        {
            userManager = usrMgr;
            _appIdentityDbContext = applicationDbContext;
        }
        public async Task<List<AppUser>> GetAllUsers()
        {
            var result = _appIdentityDbContext.Users.ToList();
            return await Task.FromResult(result);
        }
        public async Task DeleteFeaturesAsync(string id)
        {
            var result = _appIdentityDbContext.Users.SingleOrDefault(e => e.Id == id);
            if (result == null)
            {
                return;
            }
            _appIdentityDbContext.Users.Remove(result);
            await _appIdentityDbContext.SaveChangesAsync();
        }
        public async Task<AppUser> Edit(string id)
        {
            var result = _appIdentityDbContext.Users.FirstOrDefault(e => e.Id == id);
            return await Task.FromResult(result);
        }
        public void Save(AppUser identityUser)
        {
            if (Guid.Empty.Equals(identityUser.Id))
            {

                var result = _appIdentityDbContext.Users.Add(identityUser);
                if (result.State == EntityState.Added)
                {
                    _appIdentityDbContext.SaveChanges();
                }
                else
                {
                    AppUser dbEntry = _appIdentityDbContext.Users.AsNoTracking().FirstOrDefault(z => z.Id == identityUser.Id);
                    if (dbEntry != null)
                    {
                        _appIdentityDbContext.Users.Update(identityUser);
                        _appIdentityDbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
