using DesignTable.Entry;
using HEngine.Core;
using HUnity.Components;
using UnityEngine;

namespace HUnity.Core
{
    public class HGameActor : HActor
    {
        public readonly HUnityActorMetaComponent Meta;
        public readonly HUnityGameObjectComponent GameObject;
        public readonly HUnityMovementComponent Movement;

        public HGameActor(IHWorld world, long id, DActor dActor, Transform rootTransform)
            : base(world, id)
        {
            Meta = RegisterComponent<HUnityActorMetaComponent>();
            GameObject = RegisterComponent<HUnityGameObjectComponent>();
            Movement = RegisterComponent<HUnityMovementComponent>();

            Meta.SetSpawnInfo(dActor, rootTransform);
        }
    }
}