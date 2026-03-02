using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class DEvidenceTable : DTable
    {
        public DEvidenceTable(string name, IDParser parser)
            : base(name, parser)
        {
        }
        
        protected override DEntry CreateEntry(IdParsedObject dParsedObject)
        {
            return new DEvidence(dParsedObject);
        }
    }
}