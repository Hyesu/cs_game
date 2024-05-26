using System.Collections.Generic;

namespace DesignTable.Core;

public interface IDParser
{
    IEnumerable<IDParsedObject> Parse(string tablePath);
}