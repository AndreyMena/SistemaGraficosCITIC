using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Core;
//using SistemaGraficosCITIC.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string connectionString)
        {
            //builder.Services.AddDbContext<GraphicGeneratorDBContext>(options =>
            //    options.UseSqlServer(connectionString));
            services.AddDbContext<GraphicGeneratorDBContext>(options => options.UseSqlServer(connectionString));
            //services.AddDbContext<SistemaGraficosCITICContext>(options =>
            //    options.UseSqlServer(connectionString));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<SistemaGraficosCITICContext>();
            //services.AddScoped<IPersonRepository, PersonRepository>();
            //options.UseSqlServer(connection, b => b.MigrationsAssembly("SistemaGraficosCITIC"))
            return services;
        }
    }
}
