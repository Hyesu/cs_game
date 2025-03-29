using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using DesignTable.Core;

namespace DesignTable.Parser
{
    public class DXmlParser : IDParser
    {
        public async Task<IEnumerable<IDParsedObject>> ParseAsync(string tablePath, string tableName)
        {    
            var filePaths = Directory.EnumerateFiles(tablePath, "*.xml");
            var parsedObjs = new List<IDParsedObject>();
            foreach (var filePath in filePaths)
            {
                using var sr = new StreamReader(filePath);
                string str = await sr.ReadToEndAsync();
                var root = XElement.Parse(str);

                var objs = root
                    .Elements(tableName)
                    .Select(x => new DXmlParsedObject(x)); 
            
                parsedObjs.AddRange(objs);
            }

            return parsedObjs;
        }
    }   
}