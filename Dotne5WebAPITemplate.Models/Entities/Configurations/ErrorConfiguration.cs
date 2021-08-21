using Dotne5WebAPITemplate.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotne5WebAPITemplate.Models.Entities.Configurations
{
    public class ErrorConfiguration : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {
            BaseConfiguration<Error>.SetBaseConfiguration(builder);
            builder.Property(b => b.DateCreated).IsRequired();
            builder.Property(b => b.Message).HasMaxLength(5000);
            builder.Property(b => b.StackTrace).HasMaxLength(5000);
        }
    }
}
