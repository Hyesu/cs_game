using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using DesignTable.Core;

namespace DesignTable.Parser;

public class DXmlParser : IDParser
{
    public async Task<IEnumerable<IDParsedObject>> ParseAsync(string tablePath, string dirName)
    {
         
        var filePaths = Directory.EnumerateFiles(tablePath, "*.xml");
        var parsedObjs = new List<IDParsedObject>();
        foreach (var filePath in filePaths)
        {
            using var sr = new StreamReader(filePath);
            string str = await sr.ReadToEndAsync();
            var doc = XDocument.Parse(str);
            foreach (var elem in doc.Descendants(dirName))
            {
                
            }
            // var parsedObj = new DJsonParsedObject();
            // parsedObj.Initialize(json);
            // parsedObjs.Add(parsedObj);
        }

        return parsedObjs;
    }
}