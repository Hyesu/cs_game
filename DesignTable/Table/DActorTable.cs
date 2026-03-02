using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class DActorTable : DTable
    {
        public DActorTable(string name, IDParser parser)
            : base(name, parser)
        {
        }
        
        protected override DEntry CreateEntry(IdParsedObject dParsedObject)
        {
            return new DActor(dParsedObject);
        }
    }
}