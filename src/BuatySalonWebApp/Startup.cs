using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BuatySalonWebApp.Areas.Identity;
using BuatySalonWebApp.Data;
using BuatySalonWebApp.Services;
using BuatySalonWebApp.Models;

namespace BuatySalonWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:IdentityConnectionStrings:DefaultConnection"]));
            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();
            services.AddTransient<FashionService>();
            services.AddTransient<AllModelService>();
            services.AddTransient<CategoryService>();
            services.AddTransient<ContactUsIconService>();
            services.AddTransient<ContactUsInfoService>();
            services.AddTransient<ContactUsService>();
            services.AddTransient<FaqService>();
            services.AddTransient<CategoryAllNailsService>();
            services.AddTransient<NailsAllService>();
            services.AddTransient<CheackEmailsUserService>();
            services.AddTransient<IdentityService>();
            services.AddTransient<OurEmployeesService>();
            services.AddTransient<MassageService>();
            services.AddTransient<CategoryAllMassageService>();
            services.AddTransient<PriceMassageService>();
            services.AddTransient<PriceHairService>();
            services.AddTransient<PriceNailService>();
            services.AddTransient<MassageImageService>();
            services.AddTransient<NailsImageService>();
            services.AddTransient<ModelHairImageService>();
            services.AddAuthentication()
                .AddFacebook(facebookOptions => {
                    facebookOptions.ClientId = Configuration["Authentication:Facebook:ClientId"];
                    facebookOptions.ClientSecret = Configuration["Authentication:Facebook:ClientSecret"];
                })
                .AddGoogle(googleOptions => {
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
