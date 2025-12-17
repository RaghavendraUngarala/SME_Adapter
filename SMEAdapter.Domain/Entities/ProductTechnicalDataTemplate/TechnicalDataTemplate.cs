using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate
{
    public class TechnicalDataTemplate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Version { get; private set; }
        public string IdtaSubmodelId { get; private set; } // Reference to IDTA 02003
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private readonly List<TemplateSection> _sections = new();
        public IReadOnlyCollection<TemplateSection> Sections => _sections.AsReadOnly();

        // Constructor
        private TechnicalDataTemplate() { } // For EF Core

        public static TechnicalDataTemplate Create(
            string name,
            string description,
            string version,
            string idtaSubmodelId)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Template name is required", nameof(name));

            return new TechnicalDataTemplate
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Version = version,
                IdtaSubmodelId = idtaSubmodelId,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void AddSection(TemplateSection section)
        {
            if (_sections.Any(s => s.Name == section.Name))
                throw new InvalidOperationException($"Section '{section.Name}' already exists");

            _sections.Add(section);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateInfo(string name, string description, string version)
        {
            Name = name ?? Name;
            Description = description ?? Description;
            Version = version ?? Version;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
