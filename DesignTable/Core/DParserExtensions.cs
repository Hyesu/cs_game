using System;

namespace DesignTable.Core
{
    public static class DParserExtensions
    {
        public static T GetEnum<T>(this IDParsedObject parsed, string fieldName) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), parsed.GetString(fieldName));
        }
    
    }   
}