using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.ValueObjects
{
    public sealed class LangString : ValueObject
    {
        public string Language { get; private set; } = default!;
        public string Text { get; private set; } = default!;

        private LangString() { } // for EF/serialization

        public LangString(string language, string text)
        {
            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentException("Language code is required.", nameof(language));

            Language = language.Trim().ToLowerInvariant();
            Text = text?.Trim() ?? string.Empty;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Language;
            yield return Text;
        }
    }
}
