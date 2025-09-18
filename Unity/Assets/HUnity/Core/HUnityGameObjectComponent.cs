using UnityEngine;
using HEngine.Core;
using HUnity.Extensions;
using Salmon;
using Salmon.Actor;
using Salmon.Actor.Script;

namespace HUnity.Core
{
    public class HUnityGameObjectComponent : HActorComponent
    {
        private GameObject _gameObj;
        private AppearanceComponent _appearance;
        
        public GameObject GameObj => _gameObj;
        
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
            return _gameObj.transform.position;
        }

        public Vector3 GetForward()
        {
            return _gameObj.transform.forward;
        }

        public void SetPosition(Vector3 pos)
        {
            _gameObj.transform.position = pos;
        }

        private void CreateGameObject()
        {
            if (_gameObj != null)
            {
                Debug.LogError($"game object already exist - actorId({GetOwner().Id})");
                return;
            }

            var resourceSys = GetOwner().GetSystem<HResourceSystem>();
            var meta = GetOwner().GetComponent<ActorMetaComponent>();
            var prefab = resourceSys.LoadPrefab(meta.D.Prefab);
            
            _gameObj = Object.Instantiate(prefab, meta.RootTransform);
            _gameObj.name = $"{meta.D.Type}_{GetOwner().Id}";
            _appearance = _gameObj.GetComponent<AppearanceComponent>();
            
            _appearance.SetName(meta.D.Name);
        }

        private void DestroyGameObject()
        {
            if (_gameObj != null)
            {
                _gameObj.SafeDestroy();
                _gameObj = null;
            }
        }
    }
}