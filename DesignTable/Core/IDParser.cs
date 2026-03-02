using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesignTable.Core
{
    public interface IDParser
    {
        Task<IEnumerable<IdParsedObject>> ParseAsync(string tablePath, string tableName);
        IEnumerable<IdParsedObject> Parse(string tablePath, string tableName);
    }   
}