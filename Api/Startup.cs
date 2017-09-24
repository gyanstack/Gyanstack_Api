using Data.DataAccess;
using Data.DataAccess.Interface;
using Data.DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services)
        {

            services.AddAuthentication(
               o => {
                   o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   o.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Account/LogIn";
                    options.LogoutPath = "/Account/LogIn";
                });

            services.AddMvc();
            services.AddDbContextPool<DataContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseCors(builder =>
            builder.WithOrigins("http://localhost:4200", "http://www.gyanstack.com", "http://gyanstack.com")
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Section}/{action=Index}/{id?}");
            });
        }
    }
}
