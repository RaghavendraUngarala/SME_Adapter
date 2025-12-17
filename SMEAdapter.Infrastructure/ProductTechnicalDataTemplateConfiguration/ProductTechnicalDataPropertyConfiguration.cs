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
    public class ProductTechnicalDataPropertyConfiguration : IEntityTypeConfiguration<ProductTechnicalDataProperty>
    {
        public void Configure(EntityTypeBuilder<ProductTechnicalDataProperty> builder)
        {
            builder.ToTable("ProductTechnicalDataProperties");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(p => p.CustomPropertyName)
                .HasMaxLength(200);

            builder.Property(p => p.CustomSemanticId)
                .HasMaxLength(500);

            builder.HasOne(p => p.TemplateProperty)
                .WithMany()
                .HasForeignKey(p => p.TemplatePropertyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
