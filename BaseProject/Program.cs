using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BaseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            //使用连接字符串
            builder.Services.AddDbContext<DbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("BaseConnections"), MySqlServerVersion.LatestSupportedServerVersion));
            builder.Services.AddIdentity<IdentityUser<long>, IdentityRole<long>>().AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
            //配置认证模式
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "BaseWebApp";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(240);
                options.LoginPath = "/Account/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            //配置identity
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            });
            builder.Services.AddMvc();


            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //IdentityDbContext.Database.EnsureCreated();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //使用静态文件
            app.UseStaticFiles();
            //添加身份认证中间件
            app.UseAuthentication();
            //添加授权中间件
            app.UseAuthorization();
            //添加默认路由
            app.MapRazorPages();
            app.MapDefaultControllerRoute();
            app.Run();
        }
    }
}
