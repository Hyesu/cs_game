using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesignTable.Parser;
using DesignTable.Table;

namespace DesignTable.Core
{
    public class DContext
    {
        private readonly string _rootPath;
        private readonly Dictionary<Type, DTable> _tables;

        public IEnumerable<DTable> Tables => _tables.Values;

        // indexes ///////////////////
        public readonly DSampleTable Sample;
        public readonly DDialogTable Dialog;
        //////////////////////////////
        
        public DContext(string rootPath)
        {
            _rootPath = rootPath;
            _tables = new();
            
            var jsonParser = new DJsonParser();
            var xmlParser = new DXmlParser();

            // create indexes
            Sample = Add(new DSampleTable("Sample", xmlParser));
            Dialog = Add(new DDialogTable("Dialog", jsonParser));
        }

        public void Initialize(bool useAsync)
        {
            if (useAsync)
            {
                var sectionTasks = _tables.Values
                    .Select(LoadTableAsync)
                    .ToArray();

                Task.WaitAll(sectionTasks);
            }
            else
            {
                foreach (var table in _tables.Values)
                {
                    LoadTable(table);
                }
            }
            
            foreach (var table in _tables.Values)
            {
                table.PostInitialize(_tables);
            }
        }

        private async Task LoadTableAsync(DTable table)
        {
            var tablePath = _rootPath + table.Name;
            var parsedObjs = await table.Parser.ParseAsync(tablePath, table.Name);
            table.Initialize(parsedObjs);
        }

        private void LoadTable(DTable table)
        {
            var tablePath = _rootPath + table.Name;
            var parsedObjs = table.Parser.Parse(tablePath, table.Name);
            table.Initialize(parsedObjs);
        }

        private T Add<T>(T table) where T : DTable
        {
            _tables.Add(typeof(T), table);
            return table;
        }

        public T Get<T>() where T : DTable
        {
            return _tables.TryGetValue(typeof(T), out var table) ? table as T : null;
        }
    }
}