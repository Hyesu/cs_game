using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace HEngine.Actor
{
    public class HActor
    {
        private Dictionary<Type, HActorComponent> _components;
        private ImmutableArray<HActorComponent> _tickables;

        public HActor()
        {
            _components = new();
            _tickables = ImmutableArray<HActorComponent>.Empty;
        }

        public T GetComponent<T>() where T : HActorComponent
        {
            return _components.GetValueOrDefault(typeof(T)) as T;
        }

        public T RegisterComponent<T>() where T : HActorComponent, new()
        {
            return new T();
        }

        public bool UnregisterComponent(HActorComponent component)
        {
            return false;
        }

        public void Initialize()
        {
            foreach (var component in _components.Values)
            {
                component.Initialize();
            }
        }

        public void BeginPlay()
        {
            foreach (var component in _components.Values)
            {
                component.BeginPlay();
            }
        }

        public void Tick(float dt)
        {
            foreach (var component in _tickables)
            {
                component.Tick(dt);
            }
        }
        
        public void EndPlay()
        {
            foreach (var component in _components.Values)
            {
                component.EndPlay();
            }
        }
    }
}