using Dev.Data.Interface;
using Dev.Data.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Data.Providers
{
    public class PostgreSQLDataProvider : IDataProvider
    {
        public DataProvider Provider { get; } = DataProvider.PostgreSQL;

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }

        public DataContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
