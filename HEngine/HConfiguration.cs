using System;
using System.IO;
using HEngine.Extensions;
using Newtonsoft.Json.Linq;

namespace HEngine
{
    public class HConfiguration
    {
        private static HConfiguration _instance;

        public string DesignTableRoot { get; private set; }
    
        public static HConfiguration Instance
        {
            get
            {
                if (null == _instance)
                {
                    var filePath = HPath.FindFilePathByRecursively(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
                    if (string.IsNullOrEmpty(filePath))
                    {
                        throw new FileNotFoundException($"cannot find setting.json");
                    }
            
                    using (var sr = new StreamReader(filePath))
                    {
                        string json = sr.ReadToEnd();
                        var jsonObj = JObject.Parse(json);

                        var configuration = new HConfiguration
                        {
                            DesignTableRoot = jsonObj.GetString("DesignTableRoot"),
                        };

                        _instance = configuration;
                    }
                }

                return _instance;
            }
        }

        public static void Init()
        {
            // ready
            _ = HConfiguration.Instance;
        }
    }   
}