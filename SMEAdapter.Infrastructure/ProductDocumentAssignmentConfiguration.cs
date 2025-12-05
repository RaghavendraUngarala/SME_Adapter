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
    public class ProductDocumentAssignmentConfiguration : IEntityTypeConfiguration<ProductDocumentAssignment>
    {
        public void Configure(EntityTypeBuilder<ProductDocumentAssignment> builder)
        {
            builder.ToTable("ProductDocumentAssignments");

           
            builder.HasKey(x => new { x.ProductId, x.ProductDocumentId });

            builder.HasOne(x => x.Product)
           .WithMany(p => p.DocumentAssignments)
           .HasForeignKey(x => x.ProductId)
           .OnDelete(DeleteBehavior.Restrict); // FIX

            builder.HasOne(x => x.ProductDocument)
                .WithMany(d => d.DocumentAssignments)
                .HasForeignKey(x => x.ProductDocumentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
