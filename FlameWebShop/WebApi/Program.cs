using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Context;
using WebApi.Helpers.Jwt;
using WebApi.Helpers.Repositories;
using WebApi.Helpers.Services;
using WebApi.Models.Email;
using WebApi.Models.Interfaces;


namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers().AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region DbContexts
            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DBFlameShopCS")));
            #endregion

            #region EmailConfig
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
            builder.Services.AddScoped<IMailService, MailService>();
            #endregion

            #region Helpers / Services 
            builder.Services.AddScoped<JwtToken>();
            builder.Services.AddScoped<IAccountService, AccountService>();
           // builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            #endregion

            #region Repositories
            //builder.Services.AddScoped<AddressItemRepository>();
            //builder.Services.AddScoped<AddressRepository>();
            builder.Services.AddScoped<CategoryRepository>();
            builder.Services.AddScoped<CreditCardRepository>();
            builder.Services.AddScoped<OrderRepository>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<ReviewRepository>();
           // builder.Services.AddScoped<UserProfileAddressItemRepository>();
            builder.Services.AddScoped<UserProfileCreditCardRepository>();
            builder.Services.AddScoped<UserProfileRepository>();
            #endregion

            #region Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(x =>
            {
                x.Password.RequiredLength = 8;
                x.SignIn.RequireConfirmedAccount = false;
                x.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();


            builder.Services.Configure<DataProtectionTokenProviderOptions>(x =>
            {
                x.TokenLifespan = TimeSpan.FromHours(10);
            });
            #endregion

            #region Authentication
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        if (string.IsNullOrEmpty(context?.Principal?.FindFirst("id")?.Value) || string.IsNullOrEmpty(context?.Principal?.Identity?.Name))
                            context?.Fail("Unauthorized");

                        return Task.CompletedTask;
                    }
                };

                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["TokenValidation:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["TokenValidation:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["TokenValidation:SecretKey"]!))
                };
            });
            #endregion

            #region External Auth

            //builder.Services.AddAuthentication()
            //    .AddGoogle(x =>
            //    {
            //        x.ClientId = builder.Configuration["GoogleClientId"]!;
            //        x.ClientSecret = builder.Configuration["GoogleClientSecret"]!;
            //    });

            #endregion

            var app = builder.Build();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}