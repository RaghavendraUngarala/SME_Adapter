using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Entities
{
    public class ProductDocument
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public byte[]? Data { get; set; }

        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
