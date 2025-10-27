using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAdapter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Infrastructure
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Primary key
            builder.HasKey(p => p.Id);

            // Simple property
            builder.Property(p => p.ManufacturerName).HasMaxLength(200);

            builder.Property(p => p.SerialNumber);
            builder.Property(p => p.ImageUrl)
                   .HasMaxLength(500)
                   .IsRequired(false);

            // --- Owned type: ProductInfo ---
            builder.OwnsOne(p => p.ProductInfo, pi =>
            {
                pi.Property(x => x.ProductDesignation).HasColumnName("ProductInfo_ProductDesignation");
                pi.Property(x => x.ProductRoot).HasColumnName("ProductInfo_ProductRoot");
                pi.Property(x => x.ProductFamily).HasColumnName("ProductInfo_ProductFamily");
                pi.Property(x => x.ProductType).HasColumnName("ProductInfo_ProductType");
                pi.Property(x => x.OrderCode).HasColumnName("ProductInfo_OrderCode");
                pi.Property(x => x.ArticleNumber).HasColumnName("ProductInfo_ArticleNumber");
            });

            
         

            // --- Owned type: AddressInfo ---
            builder.OwnsOne(p => p.AddressInfo, ai =>
            {
                ai.Property(x => x.Street).HasColumnName("Address_Street");
                ai.Property(x => x.ZipCode).HasColumnName("Address_ZipCode");
                ai.Property(x => x.City).HasColumnName("Address_City");
                ai.Property(x => x.Country).HasColumnName("Address_Country");
            });

            

         

            // Optional: configure CompanyLogo as a blob
            builder.Property(p => p.CompanyLogo)
                   .HasColumnType("varbinary(max)")
                   .IsRequired(false);

            builder.HasMany(p => p.Documents)
               .WithOne(d => d.Product)
               .HasForeignKey(d => d.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
