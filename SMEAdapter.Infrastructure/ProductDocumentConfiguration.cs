
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace SMEAdapter.Infrastructure
{
    public class ProductDocumentConfiguration : IEntityTypeConfiguration<ProductDocument>
    {
        public void Configure(EntityTypeBuilder<ProductDocument> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(d => d.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Data)
            .HasColumnType("varbinary(max)");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Documents)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.OwnsOne(d => d.Version, v =>
            {
                v.Property(x => x.Language).HasMaxLength(50);
                v.Property(x => x.Version).HasMaxLength(50);
                v.Property(x => x.Title).HasMaxLength(255);
                v.Property(x => x.Summary);
                v.Property(x => x.Keywords);
                v.Property(x => x.State).HasMaxLength(50);
                v.Property(x => x.StateDate);
                v.Property(x => x.OrganisationName).HasMaxLength(200);
                v.Property(x => x.OrganisationOfficialName).HasMaxLength(200);
            });

            builder.OwnsOne(d => d.Identifier, i =>
            {
                i.Property(x => x.ValueId).HasMaxLength(100);
                i.Property(x => x.DomainId).HasMaxLength(100);
            });

            builder.OwnsOne(d => d.Classification, c =>
            {
                c.Property(x => x.ClassificationSystem).HasMaxLength(100);
                ConfigureLangStringSet(c.Property(x => x.ClassName))
                    .HasColumnName("Classification_ClassName")
                    .HasColumnType("nvarchar(max)");
                c.Property(x => x.ClassLang).HasMaxLength(50);
                c.Property(x => x.ClassDescription).HasMaxLength(255);
                c.Property(x => x.ClassId).HasMaxLength(100);
            });
        }

             private static PropertyBuilder<LangStringSet> ConfigureLangStringSet(PropertyBuilder<LangStringSet> property)
             {

                    var converter = new ValueConverter<LangStringSet, string>(
                        v => JsonSerializer.Serialize(v == null ? Array.Empty<LangString>() : v.Items, (JsonSerializerOptions?)null),
                        v => new LangStringSet(
                                 JsonSerializer.Deserialize<List<LangString>>(v, (JsonSerializerOptions?)null)
                                 ?? new List<LangString>())
                    );


                    var comparer = new ValueComparer<LangStringSet>(
                        (a, b) =>
                            JsonSerializer.Serialize(a == null ? Array.Empty<LangString>() : a.Items, (JsonSerializerOptions?)null)
                            ==
                            JsonSerializer.Serialize(b == null ? Array.Empty<LangString>() : b.Items, (JsonSerializerOptions?)null),

                        v =>
                            JsonSerializer.Serialize(v == null ? Array.Empty<LangString>() : v.Items, (JsonSerializerOptions?)null)
                            .GetHashCode(),

                        v =>
                            new LangStringSet(
                                JsonSerializer.Deserialize<List<LangString>>(
                                    JsonSerializer.Serialize(v == null ? Array.Empty<LangString>() : v.Items,
                                                             (JsonSerializerOptions?)null),
                                    (JsonSerializerOptions?)null
                                )
                                ?? new List<LangString>())
                    );

                    property.HasConversion(converter);
                    property.Metadata.SetValueComparer(comparer);

                    return property;
             }
             
    }
}
