using System.Collections.Generic;
using UnityEngine;
using DesignTable.Entry;
using HEngine.Core;
using HEngine.Utility;
using HUnity.Core;
using Herring;

namespace HUnity.Systems
{
    public class HActorSystem : HSystem
    {
        private static readonly string ActorRootObjName = "Root_Actor";
        
        private readonly Dictionary<long, HActor> _actors = new();
        private readonly HAtomicLong _idGenerator = new(0);

        private GameObject _rootObj;
        
        public HActor Possessed { get; private set; }
        
        public override void BeginPlay()
        {
            base.BeginPlay();
            
            var root = HerringGameInstance.Active.GameObjRoot;
            var actorRoot = root.transform.Find(ActorRootObjName);
            if (!actorRoot)
            {
                Debug.LogError($"not found root object for actor under game object root - name({ActorRootObjName})");
                return;
            }
            
            _rootObj = actorRoot.gameObject;
        }

        public override void EndPlay()
        {
            base.EndPlay();
            
            foreach (var actor in _actors.Values)
            {
                actor.EndPlay();
            }
            _actors.Clear();
        }

        public void Update(float dt)
        {
            foreach (var actor in _actors.Values)
            {
                actor.Tick(dt);
            }
        }

        public HActor Get(long id)
        {
            return _actors.GetValueOrDefault(id);
        }
        
        public HActor Spawn(string actorStrId)
        {
            var dActor = D.Actor.Get<DActor>(actorStrId);
            if (null == dActor)
            {
                Debug.LogError($"not found actor data - actorStrId({actorStrId})");
                return null;
            }
            
            var id = _idGenerator.Increment();
            var actor = new HGameActor(HGameInstance.GetInstance(), id, dActor, _rootObj.transform);
            actor.Initialize();
            _actors.Add(id, actor);
            
            actor.BeginPlay();
            Debug.Log($"actor spawned - actorId({id}) actorStrId({actorStrId})");
            return actor;
        }

        public void Possess(HActor actor)
        {
            Possessed = actor;
            Debug.Log($"actor possessed - actorId({actor.Id})");
        }

        public void Unpossess()
        {
            Possessed = null;
            Debug.Log($"actor unpossessed");
        }
    }
}