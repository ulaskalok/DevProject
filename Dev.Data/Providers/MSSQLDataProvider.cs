using Dev.Data.Interface;
using Dev.Data.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Data.Providers
{
    public class MSSQLDataProvider : IDataProvider
    {
        public DataProvider Provider { get; } = DataProvider.MSSQL;

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        public DataContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
