using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string connectionString)
        {
            //builder.Services.AddDbContext<GraphicGeneratorDBContext>(options =>
            //    options.UseSqlServer(connectionString));
            services.AddDbContext<GraphicGeneratorDBContext>(options => options.UseSqlServer(connectionString));
            //services.AddScoped<IPersonRepository, PersonRepository>();
            //options.UseSqlServer(connection, b => b.MigrationsAssembly("SistemaGraficosCITIC"))
            return services;
        }
    }
}
