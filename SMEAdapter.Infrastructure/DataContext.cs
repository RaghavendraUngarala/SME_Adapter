using Microsoft.EntityFrameworkCore;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate;
using SMEAdapter.Infrastructure.ProductTechnicalDataTemplateConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Infrastructure
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDocumentAssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new TechnicalDataTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new TemplateSectionConfiguration());
            modelBuilder.ApplyConfiguration(new TemplatePropertyConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTechnicalDataConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTechnicalDataPropertyConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }
        public DbSet<ProductDocumentAssignment> ProductDocumentAssignments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<TechnicalDataTemplate> TechnicalDataTemplates { get; set; }
        public DbSet<TemplateSection> TemplateSections { get; set; }
        public DbSet<TemplateProperty> TemplateProperties { get; set; }
        public DbSet<ProductTechnicalData> ProductTechnicalData { get; set; }
        public DbSet<ProductTechnicalDataProperty> ProductTechnicalDataProperties { get; set; }
    }

    
}
