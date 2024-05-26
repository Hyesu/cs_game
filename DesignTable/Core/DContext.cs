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

        // supported parsers
        protected DJsonParser _jsonParser;
        
        public DContext(string rootPath)
        {
            _rootPath = rootPath;
            _tables = new();
            _jsonParser = new DJsonParser();

            // create indexes
            Sample = Add(new DSampleTable("Sample", _jsonParser));
            Dialog = Add(new DDialogTable("Dialog", _jsonParser));
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
            var tablePath = _rootPath + table.DirName;
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