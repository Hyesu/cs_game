using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class DPhaseTable : DTable
    {
        public DPhaseTable(string name, IDParser parser)
            : base(name, parser)
        {
        }
        
        protected override DEntry CreateEntry(IdParsedObject dParsedObject)
        {
            return new DPhase(dParsedObject);
        }
    }
}