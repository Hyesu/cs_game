using Newtonsoft.Json.Linq;
using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class DSampleTable : DTable
    {
        public DSampleTable(string path)
            : base(nameof(DSampleTable), path)
        {
        }

        protected override DEntry CreateEntry(JObject jsonObj)
        {
            return new DSampleEntry(jsonObj);
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