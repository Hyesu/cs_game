using DesignTable.Entry;
using HEngine.Core;
using UnityEngine;

namespace HUnity.Core
{
    public class HUnityActorMetaComponent : HActorComponent
    {
        public DActor D { get; private set; }
        public Transform RootTransform { get; private set; }

        public void SetSpawnInfo(DActor dActor, Transform rootTransform)
        {
            D = dActor;
            RootTransform = rootTransform;
        }
    }
}