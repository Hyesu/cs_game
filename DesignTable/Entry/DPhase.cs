using DesignTable.Core;
using DesignTable.Types;

namespace DesignTable.Entry
{
    public class DPhase : DEntry
    {
        public readonly int Episode;
        public readonly PhaseType Type;
        public readonly string Value;
        public readonly ConditionType CompleteCondition;
        public readonly string CompleteConditionValue;
        public readonly string NextPhase;

        public DPhase(IdParsedObject parsedObject)
            : base(parsedObject)
        {
            Episode = parsedObject.GetInt("Episode");
            Type = parsedObject.GetEnum<PhaseType>("Type");
            Value = parsedObject.GetString("Value");
            CompleteCondition = parsedObject.GetEnum<ConditionType>("CompleteCondition");
            CompleteConditionValue = parsedObject.GetString("CompleteConditionValue");
            NextPhase = parsedObject.GetString("NextPhase");
        }
    }
}