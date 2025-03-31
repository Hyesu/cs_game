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

        public bool RegisterComponent(HActorComponent component)
        {
            return false;
        }

        public bool UnregisterComponent(HActorComponent component)
        {
            return false;
        }

        public void Initialize()
        {
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