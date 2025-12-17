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
    public class TechnicalDataTemplateConfiguration : IEntityTypeConfiguration<TechnicalDataTemplate>
    {
        public void Configure(EntityTypeBuilder<TechnicalDataTemplate> builder)
        {
            builder.ToTable("TechnicalDataTemplates");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.Version)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.IdtaSubmodelId)
                .HasMaxLength(500);

            builder.HasMany(t => t.Sections)
                .WithOne(s => s.Template)
                .HasForeignKey(s => s.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
