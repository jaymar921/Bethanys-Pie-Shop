using BethanysPieShop.Models;
using BethanysPieShop.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication1
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // add the entity framework
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

            
            // adding MVC support
            services.AddControllersWithViews();

            // add our own services
            services.registerMyServices();
            //services.AddScoped()      <-- middle ground, per request, an instance will be created and that instance remain active throughout the entire request. Works best on data accessing such as databases or repositories
            //services.AddTransient();  <-- always give you a new instance when you asked for one
            //services.AddSingleton()   <-- creates a single instance for the entire application and reuse that single instance


            // adding session - we will use a cookie to store our carts in client side
            services.AddHttpContextAccessor();
            services.AddSession();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // use HTTPS redirection
            app.UseHttpsRedirection();

            app.UseStatusCodePages();
            // use static files, responsible for serving static files such as in wwwroot folder
            app.UseStaticFiles();
            // use session should be before use routing
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapRazorPages();
            });

            
        }
    }
}
