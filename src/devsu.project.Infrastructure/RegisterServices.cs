using devsu.project.Application.Common.Interceptors;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Infrastructure.Persistence;
using devsu.project.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Infrastructure
{
    public static class RegisterServices
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("appConnectionString")));
            #endregion

            #region Services
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddTransient<IDateTime, DateTimeService>();
            #endregion


            return services;
        }
    }
}
