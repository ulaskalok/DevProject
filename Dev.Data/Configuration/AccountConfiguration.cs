using Dev.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Data.Configuration
{
    public class AccountConfiguration : BaseConfiguration<Dev.Domain.Account>, IEntityConfiguration
    {
        public AccountConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {

        }
        public override void Configure()
        {
            base.Configure();

            _builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);

            _builder.Property(e => e.Password)
               .IsRequired()
               .HasMaxLength(50);

            _builder.ToTable("comAccount");
        }
    }
}
