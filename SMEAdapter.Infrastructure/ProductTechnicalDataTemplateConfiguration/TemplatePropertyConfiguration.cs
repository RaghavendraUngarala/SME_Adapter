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
    public class TemplatePropertyConfiguration : IEntityTypeConfiguration<TemplateProperty>
    {
        public void Configure(EntityTypeBuilder<TemplateProperty> builder)
        {
            builder.ToTable("TemplateProperties");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.SemanticId)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.DataType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Unit)
                .HasMaxLength(50);

            builder.Property(p => p.DefaultValue)
                .HasMaxLength(500);
        }
    }
}
