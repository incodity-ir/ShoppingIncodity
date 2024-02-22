using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShappingCartApi.Models;

namespace ShappingCartApi.Persistence.MapConfig
{
    public class CartHeaderConfig:IEntityTypeConfiguration<CartHeader>
    {
        public void Configure(EntityTypeBuilder<CartHeader> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.CartDetails).WithOne(p => p.CartHeader).HasForeignKey(p => p.CartHeaderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
