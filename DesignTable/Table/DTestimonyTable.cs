using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class DTestimonyTable : DTable
    {
        public DTestimonyTable(string name, IDParser parser)
            : base(name, parser)
        {
        }
        
        protected override DEntry CreateEntry(IdParsedObject dParsedObject)
        {
            return new DTestimony(dParsedObject);
        }
    }
}