using SMEAdapter.Domain.ValueObjects;

namespace SMEAdapter.Domain.Entities
{
    public class ProductDocument
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public byte[] Data { get; set; } = Array.Empty<byte>();

        public Guid? ProductId { get; private set; }
        public Product Product { get; private set; }
        public ICollection<ProductDocumentAssignment> DocumentAssignments { get; private set; }
                 = new List<ProductDocumentAssignment>();

        public DocumentVersion Version { get; private set; } = new();
        public DocumentIdentifier Identifier { get; private set; } = new();
        public DocumentClassification Classification { get; private set; } = new();

        public ProductDocument() { }

        public ProductDocument(string fileName, string contentType, byte[] data)
        {
            FileName = fileName;
            ContentType = contentType;
            Data = data;
        }
        public enum DocumentOwnershipType
        {
            Owned = 0,
            Shared = 1
        }

        public DocumentOwnershipType OwnershipType { get;  private set; } = DocumentOwnershipType.Owned;

        public void MarkAsShared()
        {
            OwnershipType = DocumentOwnershipType.Shared;
            ProductId = null;
        }

        public void AssignToProduct(Guid productId)
        {
            OwnershipType = DocumentOwnershipType.Owned;
            ProductId = productId;
        }
        public void SetProduct(Guid productId) => ProductId = productId;

        public void ReplaceFile(string fileName, string contentType, byte[] data)
        {
            FileName = fileName;
            ContentType = contentType;
            Data = data;
        }

        public void UpdateVersion(
            string? language,
            string? version,
            LangStringSet? title,
            LangStringSet? subTitle,
            LangStringSet? description,
            LangStringSet? keywords,
            string? statusValue,
            DateTime? statusSetDate,
            string? orgName,
            string? orgOfficialName)
        {
            Version.Language = language;
            Version.Version = version;
            Version.Title = title;
            Version.SubTitle = subTitle;
            Version.Description = description;
            Version.Keywords = keywords;
            Version.StatusValue = statusValue;
            Version.StatusSetDate = statusSetDate;
            Version.OrganisationName = orgName;
            Version.OrganisationOfficialName = orgOfficialName;
        }

        public void UpdateIdentifier(string? valueId, string? domainId)
        {
            Identifier.ValueId = valueId;
            Identifier.DomainId = domainId;
        }

        public void UpdateClassification(
            string? classificationSystem,
            LangStringSet className,
            string? classLang,
            string? classDescription,
            string? classId)
        {
            Classification.Update(classificationSystem, className, classLang, classDescription, classId);
        }
    }

    public class DocumentVersion
    {
        public string? Language { get; set; }
        public string? Version { get; set; }

        public LangStringSet? Title { get; set; } = new(null);
        public LangStringSet? SubTitle { get; set; } = new(null);
        public LangStringSet? Description { get; set; } = new(null);
        public LangStringSet? Keywords { get; set; } = new(null);

        public string? StatusValue { get; set; }
        public DateTime? StatusSetDate { get; set; }

        public string? OrganisationName { get; set; }
        public string? OrganisationOfficialName { get; set; }
    }

    public class DocumentIdentifier
    {
        public string? ValueId { get; set; }
        public string? DomainId { get; set; }
    }

    public class DocumentClassification
    {
        public string? ClassificationSystem { get; set; }

        public LangStringSet ClassName { get; private set; } = new(null);

        public string? ClassLang { get; set; }
        public string? ClassDescription { get; set; }
        public string? ClassId { get; set; }

        public void Update(
            string? classificationSystem,
            LangStringSet className,
            string? classLang,
            string? classDescription,
            string? classId)
        {
            ClassificationSystem = classificationSystem;
            ClassName = className ?? new LangStringSet(null);
            ClassLang = classLang;
            ClassDescription = classDescription;
            ClassId = classId;
        }
    }
}
