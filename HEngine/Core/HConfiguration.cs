using System;
using System.IO;
using Newtonsoft.Json.Linq;
using HEngine.Extensions;
using HEngine.Utility;

namespace HEngine.Core
{
    public class HConfiguration
    {
        public static HConfiguration Shared = new();

        public string DesignTableRoot { get; private set; } = string.Empty;
    
        public void Init()
        {
            var fileName = "setting.sample.json";
            var filePath = HPath.FindFilePathByRecursively(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (string.IsNullOrEmpty(filePath))
            {
                throw new FileNotFoundException($"cannot find file - fileName({fileName})");
            }
            
            using (var sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();
                var jsonObj = JObject.Parse(json);

                DesignTableRoot = jsonObj.GetString("DesignTableRoot");
            }
        }
    }   
}