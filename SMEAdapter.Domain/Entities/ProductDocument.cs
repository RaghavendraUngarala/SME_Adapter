using SMEAdapter.Domain.ValueObjects;

namespace SMEAdapter.Domain.Entities
{
    public class ProductDocument
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string FileName { get;  set; } = string.Empty;
        public string ContentType { get;  set; } = string.Empty;
        public byte[] Data { get;  set; } = Array.Empty<byte>();

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        public DocumentVersion Version { get; private set; } = new();
        public DocumentIdentifier Identifier { get; private set; } = new();
        public DocumentClassification Classification { get; private set; } = new();

        public ProductDocument() { } // EF Core

        public ProductDocument(string fileName, string contentType, byte[] data)
        {
            FileName = fileName;
            ContentType = contentType;
            Data = data;
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
            string? title,
            string? summary,
            string? keywords,
            string? state,
            DateTime? stateDate,
            string? orgName,
            string? orgOfficialName)
        {
            Version.Language = language;
            Version.Version = version;
            Version.Title = title;
            Version.Summary = summary;
            Version.Keywords = keywords;
            Version.State = state;
            Version.StateDate = stateDate;
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
            LangStringSet className,   // ONLY THIS IS MULTILINGUAL
            string? classLang,
            string? classDescription,
            string? classId)
        {
            Classification.Update(
                classificationSystem,
                className,
                classLang,
                classDescription,
                classId);
        }
    }


    public class DocumentVersion
    {
        public string? Language { get; set; }  // Simple string used in dropdown
        public string? Version { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Keywords { get; set; }
        public string? State { get; set; }
        public DateTime? StateDate { get; set; }
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

        // 🔥 Multilingual class name
        public LangStringSet ClassName { get; private set; } = new(null);

        public string? ClassLang { get; set; }
        public string? ClassDescription { get; set; }
        public string? ClassId { get; set; }

        public void Update(string? classificationSystem,LangStringSet className,string? classLang,string? classDescription,string? classId)
        {
            ClassificationSystem = classificationSystem;
            ClassName = className ?? new LangStringSet(null);
            ClassLang = classLang;
            ClassDescription = classDescription;
            ClassId = classId;
        }
    }
}
