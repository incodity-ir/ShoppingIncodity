using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShappingCartApi.Models;

namespace ShappingCartApi.Persistence.MapConfig
{
    public class CartDetailConfig:IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
