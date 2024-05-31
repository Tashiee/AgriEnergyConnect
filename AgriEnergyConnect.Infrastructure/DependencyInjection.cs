using AgriEnergyConnect.Application.Abstractions.Clock;
using AgriEnergyConnect.Application.Abstractions.Email;
using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Products;
using AgriEnergyConnect.Domain.Users;
using AgriEnergyConnect.Infrastructure.Authentication;
using AgriEnergyConnect.Infrastructure.Authorization;
using AgriEnergyConnect.Infrastructure.Clock;
using AgriEnergyConnect.Infrastructure.OB;
using AgriEnergyConnect.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using AuthenticationService = AgriEnergyConnect.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = AgriEnergyConnect.Application.Abstractions.Authentication.IAuthenticationService;


namespace AgriEnergyConnect.Infrastructure;

public static class DependencyInjection
{
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddTransient<IEmailService, EmailService.EmailService>();
        
        AddPersistence(services, configuration);
        
        AddAuthentication(services, configuration);
        
        AddAuthorization(services, configuration);
        
        AddBackgroundJobs(services, configuration);
        
        return services;
    }

    private static void AddAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuthorizationService>();

        services.AddTransient<IClaimsTransformation, CustomClaimsTransformer>();
    }
 
    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        var connectionString = configuration.GetConnectionString("AgriConnectDb");
        
        services.AddDbContext<AuthenticationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<ApplicationIdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();
         
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Login";  
            options.AccessDeniedPath = "/Account/AccessDenied";  
            options.SlidingExpiration = true;  
        });
        
    }
 
    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AgriConnectDb");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<SqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString!));
    }
     
    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        // services.Configure<OBOptions>(configuration.GetSection("Outbox"));
        //
        // services.AddQuartz();
        //
        // services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        //
        // services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
    }
    
  

}