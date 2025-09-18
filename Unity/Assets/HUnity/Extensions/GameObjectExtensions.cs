using UnityEngine;

namespace HUnity.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SafeDestroy(this GameObject obj)
        {
            if (obj != null)
            {
                obj.SetActive(false);
                Object.Destroy(obj);
            }
        }
        
        public static void RemoveChildren(this GameObject obj)
        {
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SafeDestroy();
            }
        }

        public static void RemoveChild(this GameObject obj, int index)
        {
            var child = obj.transform.GetChild(index);
            child?.gameObject.SafeDestroy();
        }

        public static T GetComponentFromParentRecursively<T>(this GameObject obj)
        {
            var parent = obj.transform.parent;
            while (parent != null)
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