using System.Collections.Generic;
using System.Linq;
using DesignTable.Core;
using DesignTable.Entry;

namespace DesignTable.Table
{
    public class DDialogTable : DTable
    {
        public IEnumerable<DDialog> As => All.OfType<DDialog>();
        
        public DDialogTable(string name, IDParser parser)
            : base(name, parser)
        {
        }
        
        protected override DEntry CreateEntry(IdParsedObject parsedObject)
        {
            return new DDialog(parsedObject);
        }

        public DDialog Get(int id)
        {
            return Get<DDialog>(id);
        }

        public DDialog GetByStrId(string strId)
        {
            return Get<DDialog>(strId);
        }
    }
   
}