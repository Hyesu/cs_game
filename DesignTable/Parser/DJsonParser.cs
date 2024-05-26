using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DesignTable.Core;

namespace DesignTable.Parser;

public class DJsonParser : IDParser
{
    public async Task<IEnumerable<IDParsedObject>> ParseAsync(string tablePath)
    {
        var filePaths = Directory.EnumerateFiles(tablePath, "*.json");
        var parsedObjs = new List<IDParsedObject>();
        foreach (var filePath in filePaths)
        {
            using var sr = new StreamReader(filePath);
            string json = await sr.ReadToEndAsync();
            var parsedObj = new DJsonParsedObject();
            parsedObj.Initialize(json);
            parsedObjs.Add(parsedObj);
        }

        return parsedObjs;
    }
}