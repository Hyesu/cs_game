using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace HEngine.Actor
{
    public class HActor
    {
        private Dictionary<Type, HActorComponent> _components; // TODO: thread-safe
        private ImmutableArray<HActorComponent> _tickables;
        
        private bool _hasInitialized;
        private bool _hasBegun;

        public HActor()
        {
            _components = new();
            _tickables = ImmutableArray<HActorComponent>.Empty;
        }

        private void GetComponentTypeRecursively(Type? type, List<Type> types)
        {
            if (null == type || typeof(HActorComponent) == type)
            {
                return;
            }
            
            types.Add(type);
            GetComponentTypeRecursively(type.BaseType, types);
        }

        public T GetComponent<T>() where T : HActorComponent
        {
            return _components.GetValueOrDefault(typeof(T)) as T;
        }

        public T RegisterComponent<T>() where T : HActorComponent, new()
        {
            var types = new List<Type>();
            GetComponentTypeRecursively(typeof(T), types);

            foreach (var type in types)
            {
                if (_components.ContainsKey(type))
                {
                    // TODO: duplicate registered component
                    return null;
                }
            }

            var component = new T();
            foreach (var type in types)
            {
                _components.TryAdd(type, component);
            }
            
            return component;
        }

        public void UnregisterComponent(HActorComponent component)
        {
            var types = new List<Type>();
            GetComponentTypeRecursively(component.GetType(), types);

            foreach (var type in types)
            {
                if (_components.TryGetValue(type, out var c) && c == component)
                {
                    _components.Remove(type);
                }
            }
        }

        public void Initialize()
        {
            _tickables = _components.Values
                .Where(x => x.Pref.Tickable)
                .ToImmutableArray();
            
            foreach (var component in _components.Values)
            {
                component.Initialize();
            }

            _hasInitialized = true;
        }

        public void BeginPlay()
        {
            // TODO: 디버그 assert 같은거 추가할 수 있는지 확인하고, hasInitialized가 true여야 한다는 단정 추가 가능하면 하기
            
            foreach (var component in _components.Values)
            {
                component.BeginPlay();
            }

            _hasBegun = true;
        }

        public void Tick(float dt)
        {
            // TODO: 디버그 assert 같은거 추가할 수 있는지 확인하고, hasInitialized가 true여야 한다는 단정 추가 가능하면 하기
            
            foreach (var component in _tickables)
            {
                component.Tick(dt);
            }
        }
        
        public void EndPlay()
        {
            if (!_hasBegun)
            {
                // TODO: 로거 붙이고 begin 되지 않은 액터가 end되었다고 알려줘야 할 수 있겠음
                return;
            }
            
            foreach (var component in _components.Values)
            {
                component.EndPlay();
            }
        }
    }
}