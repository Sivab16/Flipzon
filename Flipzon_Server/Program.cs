using Flipzon_Server.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Flipzon_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Flipzon_Business.Repository.IRepository;
using Flipzon_Business;

namespace Flipzon_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddDbContext<ApplicationDBContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
         
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting(); // assign the endpoint for our application

            app.MapBlazorHub(); 
            // SignalR communicaiton is works here
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}