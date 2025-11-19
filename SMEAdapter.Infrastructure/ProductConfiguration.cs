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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            // Image + blob as before
            builder.Property(p => p.ImageUrl)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(p => p.CompanyLogo)
                   .HasColumnType("varbinary(max)")
                   .IsRequired(false);

            // ------- Top-level LangStringSet props -------
            ConfigureLangStringSet(builder.Property(p => p.ManufacturerName))
                .HasColumnName("ManufacturerName")
                .HasColumnType("nvarchar(max)");

            ConfigureLangStringSet(builder.Property(p => p.SerialNumber))
                .HasColumnName("SerialNumber")
                .HasColumnType("nvarchar(max)");

            // ------- Owned: ProductInfo (all LangStringSet) -------
            builder.OwnsOne(p => p.ProductInfo, pi =>
            {
                ConfigureLangStringSet(pi.Property(x => x.ProductDesignation))
                    .HasColumnName("ProductInfo_ProductDesignation")
                    .HasColumnType("nvarchar(max)");

                ConfigureLangStringSet(pi.Property(x => x.ProductRoot))
                    .HasColumnName("ProductInfo_ProductRoot")
                    .HasColumnType("nvarchar(max)");

                ConfigureLangStringSet(pi.Property(x => x.ProductFamily))
                    .HasColumnName("ProductInfo_ProductFamily")
                    .HasColumnType("nvarchar(max)");

                ConfigureLangStringSet(pi.Property(x => x.ProductType))
                    .HasColumnName("ProductInfo_ProductType")
                    .HasColumnType("nvarchar(max)");

                ConfigureLangStringSet(pi.Property(x => x.OrderCode))
                    .HasColumnName("ProductInfo_OrderCode")
                    .HasColumnType("nvarchar(max)");

                ConfigureLangStringSet(pi.Property(x => x.ArticleNumber))
                    .HasColumnName("ProductInfo_ArticleNumber")
                    .HasColumnType("nvarchar(max)");
            });

            // ------- Owned: AddressInfo (all LangStringSet) -------
            builder.OwnsOne(p => p.AddressInfo, ai =>
            {
                ConfigureLangStringSet(ai.Property(x => x.Street))
                    .HasColumnName("Address_Street")
                    .HasColumnType("nvarchar(max)");

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

            // ------- Relation: Documents (unchanged) -------
            builder.HasMany(p => p.Documents)
                   .WithOne(d => d.Product)
                   .HasForeignKey(d => d.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
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
