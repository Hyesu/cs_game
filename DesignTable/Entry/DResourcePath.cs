using DesignTable.Core;
using DesignTable.Types;

namespace DesignTable.Entry
{
    public class DResourcePath : DEntry
    {
        public readonly ResourceType Type;
        public readonly string SubType;
        public readonly string Prefab;
        public readonly bool IsUnique;
        
        public DResourcePath(IdParsedObject parsedObject)
            : base(parsedObject)
        {
            Type = parsedObject.GetEnum<ResourceType>("Type");
            SubType = parsedObject.GetString("SubType");
            Prefab = parsedObject.GetString("Prefab");
            IsUnique = parsedObject.GetBool("Unique");
        }
    }
}