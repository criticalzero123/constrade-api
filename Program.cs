using ConstradeApi.Entity;
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

            builder.Services.AddControllers();
            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Configure the HTTP request pipeline.

           
            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}