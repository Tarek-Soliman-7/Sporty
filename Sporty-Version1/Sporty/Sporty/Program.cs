using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sporty.Data;
using Sporty.Models;
using Sporty.Services;
using Sporty.Repositories.Interfaces;
using Sporty.Repositories;
using System.Threading.Tasks;
using Sporty.Services.Interfaces;
using System.Net.Http.Headers;
using Sporty.Hubs;
using Sporty.ViewModels;


namespace Sporty
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IClubRepository, ClubRepository>();
            builder.Services.AddScoped<IEventRepo, EventRepo>();
            builder.Services.AddScoped<IBookingRepo, BookingRepo>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IMembershipTypesRepository, MembershipTypesRepository>();
            builder.Services.AddScoped<MembershipRequestRepository>();
            builder.Services.AddScoped<MembershipTypesRepository>();
            builder.Services.AddScoped<BranchService>();
            builder.Services.AddScoped<MembershipRequestService>();
            builder.Services.AddScoped<MembershipTypesService>();
            builder.Services.AddScoped<DocumentRepository>();
            builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddScoped<IClubService, ClubService>();

            builder.Services.AddSignalR();

            builder.Services.AddHttpClient<IPaymentGatewayService, PaymobPaymentService>((serviceProvider, client) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var baseUrl = config["Paymob:BaseUrl"];

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });


            builder.Services.AddIdentity<User,IdentityRole>(options =>
           {
               options.Password.RequireDigit = true;
               options.Password.RequireLowercase = true;
               options.Password.RequireUppercase = true;
               options.Password.RequiredLength = 8;
               options.Password.RequireNonAlphanumeric = false;
               options.User.RequireUniqueEmail = true;
               options.SignIn.RequireConfirmedAccount = false;
               options.SignIn.RequireConfirmedEmail = false;
               options.SignIn.RequireConfirmedPhoneNumber = false;
           }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            var app = builder.Build();
            await SeedService.SeedDatabase(app.Services);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<NotificationHub>("/NotificationHub");
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
