using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HUnity.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SafeDestroy(this GameObject obj)
        {
            if (obj)
            {
                obj.SetActive(false);
                Object.Destroy(obj);
            }
        }

        public static GameObject GetChild(this GameObject obj, int index)
        {
            return obj.transform.GetChild(index).gameObject;
        }

        public static int GetChildCount(this GameObject obj)
        {
            return obj.transform.childCount;
        }

        public static GameObject AddChild(this GameObject obj, GameObject childPrefab, string name = "")
        {
            var child = Object.Instantiate(childPrefab, obj.transform);
            if (!string.IsNullOrEmpty(name))
            {
                child.name = name;
            }
            
            return child;
        }
        
        public static void RemoveChild(this GameObject obj, int index)
        {
            var child = obj.transform.GetChild(index);
            child?.gameObject.SafeDestroy();
        }
        
        public static void RemoveAllChildren(this GameObject obj)
        {
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SafeDestroy();
            }
        }

        public static IEnumerable<GameObject> Children(this GameObject obj)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                yield return obj.transform.GetChild(i).gameObject;
            }
        }

        public static T GetComponentFromParentRecursively<T>(this GameObject obj)
        {
            var parent = obj.transform.parent;
            while (parent)
            {
                var component = parent.GetComponent<T>();
                if (null != component)
                {
                    return component;
                }
                
                parent = parent.parent;
            }

            return default;
        }
    }
}