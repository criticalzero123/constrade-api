using ConstradeApi.Entity;
using ConstradeApi.Middleware;
using ConstradeApi.Model.MUserApiKey.Repository;
using ConstradeApi.Model.MOtp.Repository;
using ConstradeApi.Model.MProduct.Repository;
using ConstradeApi.Model.MSubcription.Repository;
using ConstradeApi.Model.MTransaction.Repository;
using ConstradeApi.Model.MUser.Repository;
using ConstradeApi.Model.MWallet.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>();

            builder.Services.AddTransient<IOtpRepository, OtpRepository>();

            builder.Services.AddControllers();

            //for the Jwt Auth
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata= false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //When in production specify the website in the appsettings.json
                    //ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    //ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ValidateIssuerSigningKey = true,
                };
            });

            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Configure the HTTP request pipeline.
            
           
            app.UseHttpsRedirection(); 
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWhen(httpContext => !httpContext.Request.Path.StartsWithSegments("/api/auth"), 
                        subApp => subApp.UseMiddleware<CheckApiKeyMiddleware>());

            app.MapControllers();

            app.Run();
        }
    }
}