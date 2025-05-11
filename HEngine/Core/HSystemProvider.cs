using System;
using System.Collections.Generic;

namespace HEngine.Core
{
    public class HSystemProvider
    {
        private readonly Dictionary<Type, HSystem> _indexes = new();
        private readonly Dictionary<Type, HSystem> _systems = new();

        private void GetSystemTypeRecursively(Type? type, List<Type> types)
        {
            if (null == type || typeof(HSystem) == type)
            {
                return;
            }
            
            types.Add(type);
            GetSystemTypeRecursively(type.BaseType, types);
        }

        public T GetSystem<T>() where T : HSystem
        {
            return _indexes.GetValueOrDefault(typeof(T)) as T;
        }

        public T AddSystem<T>() where T : HSystem, new()
        {
            var system = new T();
            return AddSystem(system);
        }

        public T AddSystem<T>(T system) where T : HSystem
        {
            var types = new List<Type>();
            var originType = typeof(T);
            GetSystemTypeRecursively(originType, types);

            foreach (var type in types)
            {
                if (_indexes.ContainsKey(type))
                {
                    return null;
                }
            }

            foreach (var type in types)
            {
                _indexes.TryAdd(type, system);
            }
            
            _systems.TryAdd(originType, system);
            return system;
        }

        public void Initialize()
        {
            foreach (var system in _systems.Values)
            {
                system.Initialize();
            }
        }

        public void BeginPlay()
        {
            foreach (var system in _systems.Values)
            {
                system.BeginPlay();
            }
        }

        public void EndPlay()
        {
            foreach (var system in _systems.Values)
            {
                system.EndPlay();
            }
        }
    }   
}