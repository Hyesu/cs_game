using UnityEngine;
using DesignTable.Core;
using DesignTable.Types;
using HEngine.Core;
using HUnity.Core;
using HUnity.Extensions;

namespace HUnity.UI
{
    public class HPresenter
    {
        public PresenterType Type { get; protected set; }
        
        protected GameObject ViewObj;
        protected DContext D;
        
        private GameObject _viewPrefab;
        private HSystemProvider _sysProvider;

        public static T Create<T>(string prefabPath) where T : HPresenter, new()
        {
            var gameInstance = HGameInstance.GetInstance();
            var provider = gameInstance.SystemProvider;
            var resourceSys = provider.GetSystem<HResourceSystem>();
            var prefab = resourceSys.LoadPrefab(prefabPath);
         
            var presenter = new T();
            presenter.D = gameInstance.DContext;
            presenter._viewPrefab = prefab;
            presenter._sysProvider = provider;

            return presenter;
        }

        protected HPresenter()
        {
        }

        protected virtual void BindView()
        {
        }

        protected virtual void OnConstructed()
        {
        }

        protected virtual void OnDestructed()
        {
        }

        public void Open(Transform parentTransform)
        {
            ViewObj = Object.Instantiate(_viewPrefab, parentTransform);
            BindView();
            
            OnConstructed();
        }

        public void Close()
        {
            if (ViewObj != null)
            {
                OnDestructed();
                
                ViewObj.SafeDestroy();
                ViewObj = null;
            }
        }

        protected T GetSystem<T>() where T : HSystem
        {
            return _sysProvider?.GetSystem<T>();
        }
    }
}