using UnityEngine;
using HEngine.Core;
using HUnity.Extensions;
using HUnity.Systems;

namespace HUnity.Components
{
    public class HUnityGameObjectComponent : HActorComponent
    {
        private GameObject _unityObj;
        private HUnityAppearanceComponent _appearance;
        
        public GameObject UnityObj => _unityObj;
        
        public override void BeginPlay()
        {
            base.BeginPlay();
            CreateGameObject();
        }

        public override void EndPlay()
        {
            base.EndPlay();
            DestroyGameObject();
        }

        public Vector3 GetPosition()
        {
            return _unityObj.transform.position;
        }

        public Vector3 GetForward()
        {
            return _unityObj.transform.forward;
        }

        public T GetUnityComponent<T>() where T : MonoBehaviour
        {
            return _unityObj?.GetComponent<T>();
        }

        public void SetPosition(Vector3 pos)
        {
            _unityObj.transform.position = pos;
        }

        private void CreateGameObject()
        {
            if (_unityObj)
            {
                Debug.LogError($"game object already exist - actorId({GetOwner().Id})");
                return;
            }

            var rSys = GetOwner().GetSystem<HResourceSystem>();
            var meta = GetOwner().GetComponent<HUnityActorMetaComponent>();
            var prefab = rSys.LoadPrefab(meta.D.Prefab);
            
            _unityObj = Object.Instantiate(prefab, meta.RootTransform);
            _unityObj.name = $"{meta.D.Type}_{GetOwner().Id}";
        }

        private void DestroyGameObject()
        {
            if (_unityObj)
            {
                _unityObj.SafeDestroy();
                _unityObj = null;
            }
        }
    }
}