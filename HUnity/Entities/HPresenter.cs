using Unity.Properties;
using UnityEngine;
using DesignTable.Types;

namespace HUnity.Entities
{
    public class HPresenter
    {
        public PresenterType Type { get; protected set; }
        
        protected GameObject ViewObj;
        private GameObject _viewPrefab;

        public static T Create<T>(string prefabPath) where T : HPresenter, new()
        {
            // TODO: Addressable 및 리소스 매니저 적용
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (null == prefab)
                throw new InvalidPathException($"not found ui prefab - path({prefabPath})");
         
            var presenter = new T();
            presenter._viewPrefab = prefab;

            return presenter;
        }

        public HPresenter()
        {
        }

        protected virtual void BindView()
        {
        }

        public void Open(Transform parentTransform)
        {
            ViewObj = Object.Instantiate(_viewPrefab, parentTransform);
            BindView();
        }

        public void Close()
        {
            if (ViewObj != null)
            {
                Object.Destroy(ViewObj);
                ViewObj = null;
            }
        }
    }
}