using System.IO;
using Newtonsoft.Json.Linq;
using HEngine.Extensions;

namespace HEngine.Core
{
    public class HConfiguration
    {
        public static HConfiguration Shared = new();

        public string DesignTableRoot { get; private set; } = string.Empty;
        public float CameraDefaultSpeed { get; private set; } = 50;
    
        public void Init(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();
                var jsonObj = JObject.Parse(json);

                DesignTableRoot = jsonObj.GetString("DesignTableRoot");
                CameraDefaultSpeed = jsonObj.GetFloat("CameraDefaultSpeed");
            }
        }
    }   
}