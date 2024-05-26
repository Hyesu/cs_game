using Newtonsoft.Json.Linq;
using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class DSampleTable : DTable
    {
        public DSampleTable(string dirName, IDParser parser)
            : base(nameof(DSampleTable), dirName, parser)
        {
        }

        protected override DEntry CreateEntry(IDParsedObject dParsedObject)
        {
            return new DSampleEntry(dParsedObject);
        }

        public DSampleEntry Get(int id)
        {
            return GetInternal<DSampleEntry>(id);
        }

        public DSampleEntry GetByStrId(string strId)
        {
            return GetByStrIdInternal<DSampleEntry>(strId);
        }
    }
   
}