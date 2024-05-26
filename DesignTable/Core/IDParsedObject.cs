using System.Collections.Generic;

namespace DesignTable.Core;

public interface IDParsedObject
{
    void Initialize(string serialized);

    int GetId();
    string GetStrId();
    
    string GetString(string fieldName);
    
    int GetInt(string fieldName);
    long GetLong(string fieldName);
    float GetFloat(string fieldName);
    double GetDouble(string fieldName);

    IEnumerable<string> GetStrArray(string fieldName);
    IEnumerable<int> GetIntArray(string fieldName);
    IEnumerable<IDParsedObject> GetObjArray(string fieldName);
}