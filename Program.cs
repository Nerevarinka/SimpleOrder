namespace SimpleOrder
{
    using Microsoft.EntityFrameworkCore;
    using SimpleOrder.Models;
    using SimpleOrder.Services;
    using System.Configuration;
    using System.Globalization;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<SimpleOrderContext>(options => options.UseNpgsql(connection));
            builder.Services.AddTransient<OrderService>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            CultureInfo culture = new CultureInfo("en-US");
            culture.NumberFormat.NumberDecimalSeparator = ".";
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/Error/404");

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Orders}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
