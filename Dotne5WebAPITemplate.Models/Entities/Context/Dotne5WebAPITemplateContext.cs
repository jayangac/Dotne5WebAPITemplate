using Dotne5WebAPITemplate.Models.Entities.Base;
using Dotne5WebAPITemplate.Models.Entities.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotne5WebAPITemplate.Models.Entities.Context
{
    public class Dotne5WebAPITemplateContext : DbContext
    {
        public Dotne5WebAPITemplateContext(DbContextOptions<Dotne5WebAPITemplateContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ErrorConfiguration().Configure(modelBuilder.Entity<Error>());
        }
    }
}
