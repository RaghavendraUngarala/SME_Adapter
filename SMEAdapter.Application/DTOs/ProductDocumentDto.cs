using SMEAdapter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMEAdapter.Application.DTOs
{
    public class ProductDocumentDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public string? Language { get; set; }
        public string? Version { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Keywords { get; set; }
        public string? State { get; set; }
        public DateTime? StateDate { get; set; }
        public string? OrganisationName { get; set; }
        public string? OrganisationOfficialName { get; set; }

        public string? ValueId { get; set; }
        public string? DomainId { get; set; }

        public string? ClassificationSystem { get; set; }
        public Dictionary<string, string>? ClassName { get; set; }
        public string? ClassLang { get; set; }
        public string? ClassDescription { get; set; }
        public string? ClassId { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }



}