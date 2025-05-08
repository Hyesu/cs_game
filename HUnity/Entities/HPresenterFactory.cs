using System;
using System.Collections.Generic;
using UnityEngine;
using DesignTable.Types;

namespace HUnity.Entities
{
    public class HPresenterFactory
    {
        public static readonly HPresenterFactory Shared = new();
        
        private readonly Dictionary<PresenterType, Func<string, HPresenter>> _factory
            = new Dictionary<PresenterType, Func<string, HPresenter>>();

        public void Register(PresenterType type, Func<string, HPresenter> constructor)
        {
            _factory.Add(type, constructor);
        }
        
        public HPresenter Create(PresenterType type, string prefab)
        {
            if (!_factory.TryGetValue(type, out var constructor))
            {
                Debug.LogError($"not registered presenter in factory - presenterType({type}) prefab({prefab})");
                return null;
            }

            return constructor.Invoke(prefab);
        }
    }
}