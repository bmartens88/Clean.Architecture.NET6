using Clean.Architecture.Infrastructure.Persistence.Data;
using Clean.Architecture.Shared.Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.Infrastructure.Persistence
{
    /// <summary>
    /// Class used for registering services for dependency injection
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the services from the persistence Assembly to the DI container
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> used for registration of service(s)</param>
        /// <param name="configuration"><see cref="IConfiguration"/> instance for further configuration</param>
        /// <returns><see cref="IServiceCollection"/> with services registered</returns>
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddDbContext<AppDbContext>(opts =>
                    opts.UseInMemoryDatabase(":inmemory"))
            .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    }
}
