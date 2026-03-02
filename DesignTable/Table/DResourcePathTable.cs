using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesignTable.Core;
using DesignTable.Entry;
using DesignTable.Types;

namespace DesignTable.Table
{
    public class DResourcePathTable : DTable
    {
        private Dictionary<PresenterType, DResourcePath> _uniqueUis = new();
        
        public DResourcePathTable(string name, IDParser parser)
            : base(name, parser)
        {
        }
        
        protected override DEntry CreateEntry(IdParsedObject dParsedObject)
        {
            return new DResourcePath(dParsedObject);
        }

        public override void Initialize(IEnumerable<IdParsedObject> parsedObjects)
        {
            base.Initialize(parsedObjects);

            foreach (var dPath in All.OfType<DResourcePath>())
            {
                if (ResourceType.UI == dPath.Type && dPath.IsUnique)
                {
                    if (!Enum.TryParse<PresenterType>(dPath.SubType, out var presenterType))
                        throw new InvalidDataException($"invalid presenter type - strID({dPath.StrId}) subType({dPath.SubType})");
                    
                    _uniqueUis.Add(presenterType, dPath);
                }
            }
        }

        public string GetUIPrefab(PresenterType presenterType)
        {
            if (!_uniqueUis.TryGetValue(presenterType, out var dPath))
                throw new InvalidDataException($"presenter type is not unique ui - presenterType({presenterType})");

            return dPath.Prefab;
        }

        public string GetPrefabPathBySid(string sid)
        {
            var dPath = Get<DResourcePath>(sid);
            return dPath?.Prefab ?? string.Empty;
        }
    }
   
}