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
    }
}