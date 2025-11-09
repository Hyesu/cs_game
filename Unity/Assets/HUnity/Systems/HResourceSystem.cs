using HEngine.Core;
using Unity.Properties;
using UnityEngine;

namespace HUnity.Systems
{
    public class HResourceSystem : HSystem
    {
        public GameObject LoadPrefab(string prefabPath)
        {
            // TODO: Addressable, 리소스 캐시 등 
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (!prefab)
                throw new InvalidPathException($"not found prefab - path({prefabPath})");

            return prefab;
        }

        public Sprite LoadSprite(string path)
        {
            // TODO: Addressable, 리소스 캐시 등 
            Sprite sprite = Resources.Load<Sprite>(path);
            if (!sprite)
            {
                Debug.LogError($"not found sprite from path - path({path})");
                return null;
            }

            return sprite;
        }
    }
}