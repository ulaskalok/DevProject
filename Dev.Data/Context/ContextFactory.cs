using Dev.Data.Interface;
using Dev.Data.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dev.Data.Context
{
    public class ContextFactory : IContextFactory
    {
        private Settings.Data DataConfiguration { get; }
        private ConnectionStrings ConnectionStrings { get; }

        public ContextFactory(IOptions<Settings.Data> dataOptions,IOptions<ConnectionStrings> connectionStringsOption)
        {
            DataConfiguration = dataOptions.Value;
            ConnectionStrings = connectionStringsOption.Value;
        }

        public DataContext Create()
        {
            var provider = GetType().Assembly.GetTypes()
                .Where(t => !t.GetTypeInfo().IsAbstract && t.GetInterfaces().Contains(typeof(IDataProvider))).SingleOrDefault(t => t.Name.Contains(DataConfiguration.Provider.ToString()));

           return ((IDataProvider)Activator.CreateInstance(provider)).CreateDbContext(ConnectionStrings.DefaultConnection);
        }
    }
}
