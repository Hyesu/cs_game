using DesignTable.Core;
using DesignTable.Types;

namespace DesignTable.Entry
{
    public class DEvidence : DEntry
    {
        public readonly EvidenceType Type;
        public readonly string Icon;
        public readonly string Name;
        public readonly string Desc;
        
        public DEvidence(IdParsedObject parsedObject)
            : base(parsedObject)
        {
            Type = parsedObject.GetEnum<EvidenceType>("Type");
            Icon = parsedObject.GetString("Icon");
            Name = parsedObject.GetString("Name");
            Desc = parsedObject.GetString("Desc");
        }
    }
}