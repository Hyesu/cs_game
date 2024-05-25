using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using DesignTable.Core;
using HEngine.Extensions;

namespace DesignTable.Entry
{
    public class DSampleEntry : DEntry
    {
        public readonly int NumberField;
        public readonly string StringField;

        public DSampleEntry(JObject json)
            : base(json)
        {
            NumberField = json.GetInt("NumberField");
            StringField = json.GetString("StringField");
        }
    }
}