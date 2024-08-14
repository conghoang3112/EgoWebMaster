using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Globalization;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using TLS.DataProvider;
using TLS.DataProvider.Entities;
using TLS.Repository.Base;
using TLS.Service.Account;
using TLS.Service.Catalog;
using TLS.Service.Common.Storage;
using TLS.Service.Mapping;
using TLS.Service.NewsService;
using TLS.Web.Models;
using TLS.Web.Resources;
using Microsoft.AspNetCore.DataProtection;
using TLS.Service.SurveyService;
using TLS.Service.PlanExecution;

namespace TLS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Localize
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<LocalizationService>();
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    //new CultureInfo("en"),
                    new CultureInfo("vi")
                };
                options.DefaultRequestCulture = new RequestCulture("vi");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddControllersWithViews();

            // DbContext
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            // Identity
            services.AddIdentity<AppUser, AppRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddDataProtection()
            .PersistKeysToFileSystem(new System.IO.DirectoryInfo("AppKeys"))
            //.ProtectKeysWithCertificate(new X509Certificate2());
            .SetDefaultKeyLifetime(TimeSpan.FromDays(90));

            // Config cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.SlidingExpiration = true;
                options.LoginPath = $"/Account/Login"; // Chuyển hướng nếu truy cập chức năng cần đăng nhập
                options.LogoutPath = $"/Account/Logout";
                options.AccessDeniedPath = $"/Admin"; // Chuyển hướng khi truy cập chức năng ko cho phép
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // Trên 30 giấy truy cập lại sẽ nạp lại thông tin User (Role)
                // SecurityStamp trong bảng User đổi -> nạp lại thông tinn Security
                options.ValidationInterval = TimeSpan.FromSeconds(30);
            });

            // Session storage config
            services.AddSession(options =>
            {
                options.Cookie.Name = "WebCookie"; // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                options.IdleTimeout = TimeSpan.FromHours(10); // Thời gian tồn tại của Session
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // DI 
            ConfigureDependencyInjection(services);

            // Mapping
            services.AddSingleton(provider => new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            }).CreateMapper());

            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var supportedCultures = new[] { "en-US", "vi-VN" };

            //var localizationOptions = new RequestLocalizationOptions()
            //    .SetDefaultCulture(supportedCultures[0])
            //    .AddSupportedCultures(supportedCultures)
            //    .AddSupportedUICultures(supportedCultures);

            //app.UseRequestLocalization(localizationOptions);
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    if (dbContext.Database.CanConnect())
                    {
                        Console.WriteLine("Database connection is successful!");
                    }
                    else
                    {
                        Console.WriteLine("Database connection failed.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database connection failed: {ex.Message}");
                }
            }

            var requestLocalizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(requestLocalizationOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IRepository<AppUser>, BaseRepository<AppUser>>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IRepository<Contact>, BaseRepository<Contact>>();
            services.AddTransient<IRepository<Survey>, BaseRepository<Survey>>();
            services.AddTransient<IRepository<PlanExecutionInfo>, BaseRepository<PlanExecutionInfo>>();
            services.AddTransient<IRepository<News>, BaseRepository<News>>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ISurveyService, SurveyService>();
            services.AddTransient<IPlanExecutionService, PlanExecutionService>();
            services.AddScoped<IStorageService, StorageService>();
        }
    }
}