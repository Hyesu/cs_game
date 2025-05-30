﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HEngine.Core;
using HUnity.Core;
using DesignTable.Types;

namespace HUnity.UI
{
    public class UISystem : HSystem
    {
        private readonly Dictionary<PresenterType, HPresenter> _presenters = new();
        private GameObject _canvasRoot;

        public void SetUp(GameObject canvasRoot)
        {
            _canvasRoot = canvasRoot;
        }

        public bool IsOpened(PresenterType presenterType)
        {
            return _presenters.ContainsKey(presenterType);
        }

        public HPresenter Open(PresenterType presenterType)
        {
            if (_presenters.TryGetValue(presenterType, out var presenter))
            {
                return presenter;
            }

            var d = HGameInstance.GetInstance().DContext;
            var prefab = d.ResourcePath.GetUIPrefab(presenterType);
            presenter = HPresenterFactory.Shared.Create(presenterType, prefab);
            presenter.Open(_canvasRoot.transform);
            
            _presenters.Add(presenter.Type, presenter);
            return presenter;
        }

        public bool Close(PresenterType presenterType)
        {
            if (!_presenters.Remove(presenterType, out var removed))
            {
                return false;
            }
            
            removed.Close();
            return true;
        }

        public void CloseAll()
        {
            var types = _presenters.Keys.ToArray();
            foreach (var type in types)
            {
                Close(type);
            }
        }
    }
}