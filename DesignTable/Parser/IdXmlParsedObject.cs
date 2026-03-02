using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DesignTable.Core;

namespace DesignTable.Parser
{
    public class DXmlParsedObject : IdParsedObject
    {
        private readonly XElement _element;
        private readonly int _id;
        private readonly string _strId;

        public DXmlParsedObject(XElement element)
        {
            _element = element;
            _id = int.Parse(element.Attribute("Id")!.Value);
            _strId = element.Attribute("StrId")!.Value;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetStrId()
        {
            return _strId;
        }

        public string GetString(string fieldName)
        {
            return _element.Attribute(fieldName)?.Value ?? string.Empty;
        }

        public bool GetBool(string fieldName)
        {
            return bool.TryParse(GetString(fieldName), out var result) && result;
        }
        
        public int GetInt(string fieldName)
        {
            return int.Parse(GetString(fieldName));
        }

        public long GetLong(string fieldName)
        {
            return long.Parse(GetString(fieldName));
        }

        public float GetFloat(string fieldName)
        {
            return float.Parse(GetString(fieldName));
        }

        public double GetDouble(string fieldName)
        {
            return double.Parse(GetString(fieldName));
        }

        public IEnumerable<string> GetStrArray(string fieldName)
        {
            return GetString(fieldName)
                .Split("|")
                .Where(x => !string.IsNullOrEmpty(x));
        }

        public IEnumerable<int> GetIntArray(string fieldName)
        {
            return GetStrArray(fieldName)
                .Select(int.Parse);
        }

        public IEnumerable<IdParsedObject> GetObjArray(string fieldName)
        {
            throw new NotSupportedException($"not supported object array in xml parser");
        }
    }
}