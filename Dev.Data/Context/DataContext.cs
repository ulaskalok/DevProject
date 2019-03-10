using Dev.Data.Interface;
using Dev.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dev.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            GetType().Assembly.GetTypes()
                .Where(t => !t.GetTypeInfo().IsAbstract && t.GetInterfaces().Contains(typeof(IEntityConfiguration)))
                .ToList()
                .ForEach(t => ((IEntityConfiguration)Activator.CreateInstance(t, new[] { modelBuilder })).Configure());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
