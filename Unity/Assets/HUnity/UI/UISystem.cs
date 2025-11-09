using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HEngine.Core;
using HUnity.Core;
using DesignTable.Types;
using Herring.UI.Presenter;

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
            var d = HGameInstance.GetInstance().DContext;
            var prefab = d.ResourcePath.GetUIPrefab(presenterType);

            return Open(presenterType, prefab);
        }

        public HPresenter Open(PresenterType presenterType, string prefabPath)
        {
            if (_presenters.TryGetValue(presenterType, out var presenter))
            {
                return presenter;
            }

            presenter = HPresenterFactory.Shared.Create(presenterType, prefabPath);
            presenter.Attach(_canvasRoot.transform);
            
            _presenters.Add(presenter.Type, presenter);
            return presenter;
        }

        public bool Close(PresenterType presenterType)
        {
            if (!_presenters.Remove(presenterType, out var removed))
            {
                return false;
            }
            
            removed.Detach();
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

        public void ShowToastMessage(string text)
        {
            var presenter = Open(PresenterType.Toast) as ToastMessagePresenter;
            presenter!.Show(text);
        }
    }
}