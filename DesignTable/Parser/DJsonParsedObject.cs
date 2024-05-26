using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using DesignTable.Core;

namespace DesignTable.Parser;

public class DJsonParsedObject : IDParsedObject
{
    private readonly JObject _json;
    
    private int _id;
    private string _strId;

    public DJsonParsedObject(JObject json)
    {
        _json = json;
        _id = 0;
        _strId = string.Empty;
    }
    
    public void Initialize()
    {   
        _id = GetInt("Id");
        _strId = GetString("StrId");
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
        if (!_json.TryGetValue(fieldName, out var value))
        {
            return null;
        }

        return value.ToString();
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
        if (!_json.TryGetValue(fieldName, out var value))
        {
            return Enumerable.Empty<string>();
        }

        return value.ToArray()
            .Select(x => x.ToString());
    }

    public IEnumerable<int> GetIntArray(string fieldName)
    {
        return GetStrArray(fieldName)
            .Select(int.Parse);
    }

    public IEnumerable<IDParsedObject> GetObjArray(string fieldName)
    {   
        if (!_json.TryGetValue(fieldName, out var value))
        {
            return Enumerable.Empty<IDParsedObject>();
        }

        return value.ToArray()
            .Select(x => x.ToObject<JObject>())
            .Select(x => new DJsonParsedObject(x));
    }
}