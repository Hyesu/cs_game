using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class D___Table : DTable
    {
        public D___Table(string name, IDParser parser)
            : base(name, parser)
        {
        }
        
        protected override DEntry CreateEntry(IdParsedObject dParsedObject)
        {
            return new D___(dParsedObject);
        }
    }
}