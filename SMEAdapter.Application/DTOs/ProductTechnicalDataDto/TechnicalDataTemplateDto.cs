using SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate;

namespace SMEAdapter.Application.DTOs.ProductTechnicalDataDto
{
    public class TechnicalDataTemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string IdtaSubmodelId { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public List<TemplateSectionDto> Sections { get; set; } = new();

        // Computed properties
        public int SectionCount => Sections?.Count ?? 0;
        public int TotalPropertyCount => Sections?.Sum(s => s.PropertyCount) ?? 0;
    }
}
