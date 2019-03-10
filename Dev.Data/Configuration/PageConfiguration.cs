using Dev.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Data.Configuration
{
    public class PageConfiguration : BaseConfiguration<Dev.Domain.Page>, IEntityConfiguration
    {
        public PageConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {

        }
        public override void Configure()
        {
            base.Configure();

            _builder.Property(e => e.PageName)
                .IsRequired()
                .HasMaxLength(250);

            _builder.ToTable("comPage");
        }
    }
}
