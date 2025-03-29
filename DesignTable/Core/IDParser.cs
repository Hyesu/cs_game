using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesignTable.Core
{
    public interface IDParser
    {
        Task<IEnumerable<IDParsedObject>> ParseAsync(string tablePath, string tableName);
    }   
}