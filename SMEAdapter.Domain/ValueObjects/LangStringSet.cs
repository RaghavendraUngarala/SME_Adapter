using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMEAdapter.Domain.ValueObjects
{
    public sealed class LangStringSet : ValueObject
    {
        private readonly List<LangString> _items = new();
        public IReadOnlyCollection<LangString> Items => _items.AsReadOnly();

        private LangStringSet() { } // EF
        public LangStringSet(IEnumerable<LangString>? Items)
            => _items = Items?.ToList() ?? new List<LangString>();

        public string? Get(string lang) =>
            _items.FirstOrDefault(x => x.Language.Equals(lang, StringComparison.OrdinalIgnoreCase))?.Text;

        public LangStringSet AddOrReplace(string lang, string text)
        {
            var existing = _items.FirstOrDefault(x => x.Language.Equals(lang, StringComparison.OrdinalIgnoreCase));
            if (existing is not null) _items.Remove(existing);
            _items.Add(new LangString(lang, text));
            return new LangStringSet(_items);
        }

        public LangStringSet Remove(string lang)
        {
            var existing = _items.FirstOrDefault(x => x.Language.Equals(lang, StringComparison.OrdinalIgnoreCase));
            if (existing is not null) _items.Remove(existing);
            return new LangStringSet(_items);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
            => _items.OrderBy(i => i.Language); // structural equality
        public static LangStringSet FromDictionary(IDictionary<string, string>? dict)
        {
            if (dict is null || dict.Count == 0) 
                return new LangStringSet(null);
            var items = dict.Select(kv => new LangString(kv.Key, kv.Value));
                return new LangStringSet(items);
        }

        public Dictionary<string, string> ToDictionary()
            => Items
               .GroupBy(x => x.Language, StringComparer.OrdinalIgnoreCase)
               .ToDictionary(g => g.Key, g => g.Last().Text, StringComparer.OrdinalIgnoreCase);

        /// Primary display; falls back to any available text if lang missing.
        public string GetOrFallback(string lang = "en")
            => Get(lang) ?? Items.FirstOrDefault()?.Text ?? string.Empty;
    }
}
