using WebApp.Helpers;
using WebApp.Services;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region services
            builder.Services.AddScoped<AccountService>();
			builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<CommentsService>();
			builder.Services.AddScoped<ProductService>();
			builder.Services.AddScoped<ApiHelper>();
			#endregion

			var app = builder.Build();

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
