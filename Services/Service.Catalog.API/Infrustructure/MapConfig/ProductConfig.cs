using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client.AppConfig;
using Service.Catalog.API.Models;

namespace Service.Catalog.API.Infrustructure.MapConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            #region  Seed Data
            builder.HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "MacBook",
                    Price = 999.99,
                    Description = "Apple MacBook",
                    CategoryName = "Apple, Digital",
                    ImageURL = ""
                }
            );

            builder.HasData(
                new Product
                {
                    ProductId = 2,
                    Name = "IPhone 13",
                    Price = 1000.99,
                    Description = "Apple Phone",
                    CategoryName = "Apple, Digital",
                    ImageURL = ""
                }
            );
            builder.HasData(
    new Product
    {
        ProductId = 3,
        Name = "Samsung S23",
        Price = 899.99,
        Description = "Samsung Phone",
        CategoryName = "Samsung, Digital",
        ImageURL = ""
    }
);

            #endregion

        }

    }
}