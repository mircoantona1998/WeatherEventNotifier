using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Userdata.Models;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddIdentity<Service, IdentityRole>()
         .AddEntityFrameworkStores<SlamanagerContext>()
         .AddDefaultTokenProviders();
        services.AddScoped<UserManager<Service>>();
        services.AddScoped<SignInManager<Service>>();
        services.AddDbContext<SlamanagerContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings") ?? Configuration.GetConnectionString("Userdata")));
        services.AddAuthorization();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();  
        app.UseAuthorization();
        app.Use(async (context, next) =>
        {
            await next();
            if (context.Response.StatusCode == 401)
            {
                await context.ChallengeAsync();  
            }
        });
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        

    }

}
