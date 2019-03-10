using Dev.Data.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Data.Interface
{
    public interface IDataProvider
    {
        DataProvider Provider { get; }
        IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString);
        DataContext CreateDbContext(string connectionString);
    }
}
