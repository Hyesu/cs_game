using DesignTable.Core;
using DesignTable.Types;

namespace DesignTable.Entry
{
    public class DActor : DEntry
    {
        public readonly ActorType Type;
        public readonly string Name;
        public readonly string Desc;
        public readonly string Portrait;
        public readonly string Icon;
        public readonly string Prefab;
        public readonly float Speed;

        public bool HasPortrait => !string.IsNullOrEmpty(Portrait);
        public bool HasIcon => !string.IsNullOrEmpty(Icon);
        
        public DActor(IdParsedObject parsedObject)
            : base(parsedObject)
        {
            Type = parsedObject.GetEnum<ActorType>("Type");
            Name = parsedObject.GetString("Name");
            Desc = parsedObject.GetString("Desc");
            Portrait = parsedObject.GetString("Portrait");
            Icon = parsedObject.GetString("Icon");
            Prefab = parsedObject.GetString("Prefab");
            Speed = parsedObject.GetFloat("Speed");
        }
    }
}