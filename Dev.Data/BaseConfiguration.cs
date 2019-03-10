using Dev.Data.Interface;
using Dev.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Data
{
    public abstract class BaseConfiguration<T> : IEntityConfiguration where T : Entity
    {
        protected readonly ModelBuilder _modelBuilder;
        protected EntityTypeBuilder<T> _builder => _modelBuilder.Entity<T>();
        protected BaseConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
        public virtual void Configure()
        {
            _builder.Property(p => p.Id).ValueGeneratedOnAdd();
            _builder.Property(e => e.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
