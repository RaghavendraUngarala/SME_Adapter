using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Infrastructure.ProductTechnicalDataTemplateConfiguration
{
    public class ProductTechnicalDataConfiguration : IEntityTypeConfiguration<ProductTechnicalData>
    {
        public void Configure(EntityTypeBuilder<ProductTechnicalData> builder)
        {
            builder.ToTable("ProductTechnicalData");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Version)
                .HasMaxLength(50);

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Template)
                .WithMany()
                .HasForeignKey(p => p.TemplateId)
                .OnDelete(DeleteBehavior.Restrict); // Don't delete if template is used

            builder.HasMany(p => p.Properties)
                .WithOne(prop => prop.ProductTechnicalData)
                .HasForeignKey(prop => prop.ProductTechnicalDataId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
