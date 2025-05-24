using HEngine.Core;
using Unity.Properties;
using UnityEngine;

namespace HUnity.Core
{
    public class HResourceSystem : HSystem
    {
        public GameObject LoadPrefab(string prefabPath)
        {
            // TODO: Addressable, 리소스 캐시 등 
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (null == prefab)
                throw new InvalidPathException($"not found ui prefab - path({prefabPath})");

            return prefab;
        }
    }
}