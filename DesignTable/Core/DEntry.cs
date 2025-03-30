using System;
using Newtonsoft.Json.Linq;

namespace DesignTable.Core
{
    public class DEntry
    {
        public readonly int Id;
        public readonly string StrId;

        public DEntry(IDParsedObject parsedObject)
        {
            Id = parsedObject.GetId();
            StrId = parsedObject.GetStrId();
        }

        public virtual void Initialize(IDParsedObject parsedObject)
        {
            throw new InvalidOperationException($"not implemented ency-entry");
        }
    }
}