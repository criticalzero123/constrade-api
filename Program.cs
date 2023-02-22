using ConstradeApi.Entity;
using ConstradeApi.Middleware;
using ConstradeApi.Model.MUserAuthorize.Repository;
using ConstradeApi.Model.MOtp.Repository;
using ConstradeApi.Model.MProduct.Repository;
using ConstradeApi.Model.MSubcription.Repository;
using ConstradeApi.Model.MTransaction.Repository;
using ConstradeApi.Model.MUser.Repository;
using ConstradeApi.Model.MWallet.Repository;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json", optional:true);
            builder.Services.AddControllers();


            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(option => option.UseNpgsql(builder.Configuration["ConnectionString:PostgressDB"]));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ITransactionRepository, TrsanctionRepository>();
            builder.Services.AddScoped<IWalletRepository, WalletRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IUserAuthorizeRepository, UserAuthorizeRepository>();

            builder.Services.AddTransient<IOtpRepository, OtpRepository>();

            builder.Services.AddControllers();
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Configure the HTTP request pipeline.
            
           
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseSession();

            app.UseWhen(httpContext => !httpContext.Request.Path.StartsWithSegments("/api/users"), 
                        subApp => subApp.UseMiddleware<UserAuthorizeMiddleware>());

            app.MapControllers();

            app.Run();
        }
    }
}