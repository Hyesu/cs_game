using DesignTable.Core;

namespace DesignTable.Entry
{
    public class DSampleEntry : DEntry
    {
        public readonly int NumberField;
        public readonly string StringField;

        public DSampleEntry(IdParsedObject parsedObject)
            : base(parsedObject)
        {
            NumberField = parsedObject.GetInt("NumberField");
            StringField = parsedObject.GetString("StringField");
        }
    }
}