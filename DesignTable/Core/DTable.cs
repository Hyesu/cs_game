using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DesignTable.Core
{
    public class DTable
    {
        private readonly string _name;
        private readonly IDParser _parser;

        private readonly Dictionary<int, DEntry> _entries;
        private readonly Dictionary<string, DEntry> _entriesByStrId;

        public string Name => _name;
        public IDParser Parser => _parser;
        public IEnumerable<DEntry> All => _entries.Values;

        public DTable(string name, IDParser parser)
        {
            _name = name;
            _parser = parser;

            _entries = new();
            _entriesByStrId = new();
        }

        protected virtual DEntry CreateEntry(IDParsedObject parsedObject)
        {
            throw new InvalidOperationException($"not implemented ency-section entry creator");
        }

        public virtual void Initialize(IEnumerable<IDParsedObject> parsedObjects)
        {
            var entries = parsedObjects
                .Select(CreateEntry);
            foreach (var entry in entries)
            {
                AddEntry(entry);
            }
        }

        public virtual void PostInitialize(IReadOnlyDictionary<Type, DTable> allSections)
        {
        }

        protected T GetInternal<T>(int id) where T : DEntry
        {
            if (!_entries.TryGetValue(id, out var entry))
                return null;

            return entry as T;
        }

        protected T GetByStrIdInternal<T>(string strId) where T : DEntry
        {
            if (!_entriesByStrId.TryGetValue(strId, out var entry))
                return null;

            return entry as T;
        }

        private void AddEntry(DEntry entry)
        {
            if (_entries.ContainsKey(entry.Id))
            {
                throw new InvalidDataException($"duplicate ency-entry id- table({_name}) id({entry.Id})");
            }

            if (_entriesByStrId.ContainsKey(entry.StrId))
            {
                throw new InvalidDataException($"duplicate ency-entry strId- table({_name}) strId({entry.StrId})");
            }

            _entries.Add(entry.Id, entry);
            _entriesByStrId.Add(entry.StrId, entry);
        }
    }
}