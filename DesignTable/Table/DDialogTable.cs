using Newtonsoft.Json.Linq;
using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class DDialogTable : DTable
    {
        public DDialogTable(string path, IDParser parser)
            : base(nameof(DDialogTable), path, parser)
        {
        }

        protected override DEntry CreateEntry(IDParsedObject parsedObject)
        {
            return new DDialog(parsedObject);
        }

        public DDialog Get(int id)
        {
            return GetInternal<DDialog>(id);
        }

        public DDialog GetByStrId(string strId)
        {
            return GetByStrIdInternal<DDialog>(strId);
        }
    }
   
}