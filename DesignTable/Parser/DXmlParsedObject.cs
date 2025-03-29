using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using DesignTable.Core;

namespace DesignTable.Parser;

public class DXmlParsedObject : IDParsedObject
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
            .Split(";");
    }

    public IEnumerable<int> GetIntArray(string fieldName)
    {
        return GetStrArray(fieldName)
            .Select(int.Parse);
    }

    public IEnumerable<IDParsedObject> GetObjArray(string fieldName)
    {
        throw new NotSupportedException($"not supported object array in xml parser");
    }
}