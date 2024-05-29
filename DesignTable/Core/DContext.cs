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
        protected readonly Dictionary<Type, DTable> _tables;

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

        public void Initialize()
        {
            var sectionTasks = _tables.Values
                .Select(LoadTableAsync);

            Task.WaitAll(sectionTasks.ToArray());

            foreach (var table in _tables.Values)
            {
                table.PostInitialize(_tables);
            }
        }

        private async Task<string> LoadTableAsync(DTable table)
        {
            var tablePath = _rootPath + table.Name;
            var parsedObjs = await table.Parser.ParseAsync(tablePath, table.Name);
            table.Initialize(parsedObjs);
            return table.Name;
        }

        protected T Add<T>(T table) where T : DTable
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