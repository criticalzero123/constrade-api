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
using ConstradeApi.Model.MUserChat.Repository;
using ConstradeApi.Hubs;
using ConstradeApi.Model.MUserMessage.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ConstradeApi.Model.MProductChat.Repository;
using ConstradeApi.Model.MProductMessage.Repository;
using ConstradeApi.Model.MUserReport.Repositories;
using ConstradeApi.Model.MProductReport.Repository;
using ConstradeApi.Model.MSystemFeedback.Repository;
using ConstradeApi.Model.MUserNotification.Repository;
using ConstradeApi.Model.MCommunity.Repository;

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
            builder.Services.AddSignalR();
      

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(option => option.UseNpgsql(builder.Configuration["ConnectionString:PostgressDB"]));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IWalletRepository, WalletRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IUserChatRepository, UserChatRepository>();
            builder.Services.AddScoped<IUserMessageRepository, UserMessageRepository>();
            builder.Services.AddScoped<IProductChatRepository, ProductChatRepository>();
            builder.Services.AddScoped<IProductMessageRepository, ProductMessageRepository>();
            builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
            builder.Services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();

            builder.Services.AddTransient<IOtpRepository, OtpRepository>();
            builder.Services.AddTransient<IUserReportRepository,UserReportRepository>();
            builder.Services.AddTransient<IProductReportRepository,ProductReportRepository>();
            builder.Services.AddTransient<ISystemFeedbackRepository, SystemFeedbackRepository>();
            builder.Services.AddControllers();

            //for the Jwt Auth
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                //for signal R
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hubs")))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };

                o.RequireHttpsMetadata= false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    
                    NameClaimType = ClaimTypes.Name,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ValidateIssuerSigningKey = true,
                };
            });

            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Configure the HTTP request pipeline.
            
           
            app.UseRouting();
            
            app.UseHttpsRedirection(); 
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWhen(httpContext => !httpContext.Request.Path.StartsWithSegments("/api/auth"),
                        subApp => subApp.UseMiddleware<CheckApiKeyMiddleware>());
            app.UseEndpoints(e =>
            {
                e.MapHub<UserChatHub>("/hubs/userChatHub");
                e.MapHub<ProductChatHub>("/hubs/productChatHub");
            });

            app.MapControllers();


            app.Run();
        }
    }
}