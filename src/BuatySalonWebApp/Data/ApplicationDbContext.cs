using System;
using System.Collections.Generic;
using System.Text;
using BuatySalonWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuatySalonWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceCategory>()
             .HasKey(t => new { t.CategoryId, t.ServiceId });
            modelBuilder.Entity<ServiceCategory>()
               .HasOne(p => p.ModelService)
               .WithMany(p => p.ServiceCategories)
               .HasForeignKey(p => p.ServiceId);
            modelBuilder.Entity<ServiceCategory>()
               .HasOne(c => c.Category)
               .WithMany(c => c.ServiceCategories)
               .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<NailsAllModelCategoryAllNailsModel>()
            .HasKey(t => new { t.CategoryAllNailsModelId, t.NailsAllModelId });
            modelBuilder.Entity<NailsAllModelCategoryAllNailsModel>()
              .HasOne(p => p.NailsAllModel)
              .WithMany(p => p.NailsAllModelCategories)
              .HasForeignKey(p => p.NailsAllModelId);
            modelBuilder.Entity<NailsAllModelCategoryAllNailsModel>()
             .HasOne(c => c.CategoryAllNailsModel)
             .WithMany(c => c.NailsAllModelCategories)
             .HasForeignKey(c => c.CategoryAllNailsModelId);

            modelBuilder.Entity<MassageAllModelCategoryAllMassageModel>()
                .HasKey(m => new { m.CategoryMassageModelId, m.MassageAllModelId });
            modelBuilder.Entity<MassageAllModelCategoryAllMassageModel>()
                .HasOne(m => m.MassageAllModel)
                .WithMany(m => m.MassageAllModelCategories)
                .HasForeignKey(m => m.MassageAllModelId);
            modelBuilder.Entity<MassageAllModelCategoryAllMassageModel>()
                .HasOne(c => c.CategoryMassageModel)
                .WithMany(c => c.MassageAllModelCategory)
                .HasForeignKey(c => c.CategoryMassageModelId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Fashion> Fashions { get; set; }
        public DbSet<ModelService> ModelServices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<ContactUsInfo> ContactUsInfos { get; set; }
        public DbSet<ContactUsIcon> ContactUsIcons { get; set; }
        public DbSet<FrequentlyAskedQuestion> FrequentlyAskedQuestions { get; set; }
        public DbSet<NailsAllModelCategoryAllNailsModel> NailsAllModelCategories { get; set; }
        public DbSet<NailsAllModel> NailsAllModels { get; set; }
        public DbSet<CategoryAllNailsModel> CategoryAllNails { get; set; }
        public DbSet<CheackEmailUsers> CheackEmailUsers { get; set; }
        public DbSet<OurEmployee> OurEmployees { get; set; }
        public DbSet<MassageAllModelCategoryAllMassageModel> MassageAllModelCategoryAllMassageModels { get; set; }
        public DbSet<CategoryMassageModel> CategoryMassageModels { get; set; }
        public DbSet<MassageAllModel> MassageAllModels { get; set; }
        public DbSet<PriceMassage> PriceMassages { get; set; }
        public DbSet<PriceHair> PriceHairs { get; set; }
        public DbSet<PriceNail> PriceNails { get; set; }
        public DbSet<MassageImage> MassageImages { get; set; }
        public DbSet<NailsImage> NailsImages { get; set; } 
        public DbSet<ServiceImage> serviceImages { get; set; }
    }
}
