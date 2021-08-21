using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotne5WebAPITemplate.Models.Entities.Base
{
    public static class BaseConfiguration<T> where T : class, IEntityBase
    {
        public static void SetBaseConfiguration(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.Status).IsRequired();
        }
    }
}
