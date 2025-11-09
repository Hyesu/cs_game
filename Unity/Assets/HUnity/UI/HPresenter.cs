using UnityEngine;
using DesignTable.Core;
using DesignTable.Types;
using HEngine.Core;
using HUnity.Core;
using HUnity.Extensions;
using HUnity.Systems;

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

            return Create<T>(prefab);
        }

        public static T Create<T>(GameObject prefabObj) where T : HPresenter, new()
        {
            var gameInstance = HGameInstance.GetInstance();
            var provider = gameInstance.SystemProvider;
         
            var presenter = new T();
            presenter.D = gameInstance.DContext;
            presenter._viewPrefab = prefabObj;
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

        public void Attach(Transform parentTransform)
        {
            ViewObj = Object.Instantiate(_viewPrefab, parentTransform);
            BindView();
            
            OnConstructed();
        }

        public void Detach()
        {
            if (ViewObj)
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