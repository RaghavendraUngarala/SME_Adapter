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
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CompanyImageUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            // -----------------------------
            // Main multilingual name
            // -----------------------------
            ConfigureLangStringSet(builder.Property(c => c.CompanyManufacturerName))
                .HasColumnName("ManufacturerName")
                .HasColumnType("nvarchar(max)");

            // -----------------------------
            // Owned: CompanyAddressInfo
            // -----------------------------
            builder.OwnsOne(c => c.CompanyAddressInfo, ai =>
            {
                ConfigureLangStringSet(ai.Property(x => x.ZipCode))
                    .HasColumnName("Address_ZipCode")
                    .HasColumnType("nvarchar(max)");

                ConfigureLangStringSet(ai.Property(x => x.City))
                    .HasColumnName("Address_City")
                    .HasColumnType("nvarchar(max)");

                ConfigureLangStringSet(ai.Property(x => x.Country))
                    .HasColumnName("Address_Country")
                    .HasColumnType("nvarchar(max)");
            });
        }

        // ---------------------------------------------------------------------
        // Reuse EXACT Same LangStringSet JSON converter + comparer as Products
        // ---------------------------------------------------------------------
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
