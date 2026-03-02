using Newtonsoft.Json.Linq;
using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class DSampleTable : DTable
    {
        public DSampleTable(string name, IDParser parser)
            : base(name, parser)
        {
        }
        
        protected override DEntry CreateEntry(IdParsedObject dParsedObject)
        {
            return new DSampleEntry(dParsedObject);
        }

        public DSampleEntry Get(int id)
        {
            return Get<DSampleEntry>(id);
        }

        public DSampleEntry GetByStrId(string strId)
        {
            return Get<DSampleEntry>(strId);
        }
    }
   
}