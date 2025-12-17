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
    public class TemplateSectionConfiguration : IEntityTypeConfiguration<TemplateSection>
    {
        public void Configure(EntityTypeBuilder<TemplateSection> builder)
        {
            builder.ToTable("TemplateSections");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.SemanticId)
                .HasMaxLength(500);

            builder.HasMany(s => s.Properties)
                .WithOne(p => p.Section)
                .HasForeignKey(p => p.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
