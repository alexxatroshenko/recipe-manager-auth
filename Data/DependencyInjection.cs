using Data.Db;
using Data.Db.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DependencyInjection
{
    public static void AddDataDependencies(this IServiceCollection services, string? connectionString)
    {
        if (connectionString is null) 
            throw new ArgumentNullException(nameof(connectionString), "Connection string 'IdentityDb' not found");

        services.AddDbContext<ApplicationDbContext>((_, options) =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    }
}