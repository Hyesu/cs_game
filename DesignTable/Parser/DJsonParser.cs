using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DesignTable.Core;
using Newtonsoft.Json.Linq;

namespace DesignTable.Parser
{
    public class DJsonParser : IDParser
    {
        public async Task<IEnumerable<IDParsedObject>> ParseAsync(string tablePath, string tableName)
        {
            var filePaths = Directory.EnumerateFiles(tablePath, "*.json");
            var parsedObjs = new List<IDParsedObject>();
            foreach (var filePath in filePaths)
            {
                using var sr = new StreamReader(filePath);
                string str = await sr.ReadToEndAsync();

                var json = JObject.Parse(str);
                var parsedObj = new DJsonParsedObject(json);
                parsedObj.Initialize();
                parsedObjs.Add(parsedObj);
            }

            return parsedObjs;
        }
    }   
}