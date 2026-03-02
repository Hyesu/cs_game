using System.Collections.Generic;

namespace DesignTable.Core
{
    public interface IdParsedObject
    {
        int GetId();
        string GetStrId();
    
        string GetString(string fieldName);
        bool GetBool(string fieldName);
    
        int GetInt(string fieldName);
        long GetLong(string fieldName);
        float GetFloat(string fieldName);
        double GetDouble(string fieldName);

        IEnumerable<string> GetStrArray(string fieldName);
        IEnumerable<int> GetIntArray(string fieldName);
        IEnumerable<IdParsedObject> GetObjArray(string fieldName);
    }
}