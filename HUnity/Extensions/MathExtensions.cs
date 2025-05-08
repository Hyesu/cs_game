using UnityEngine;

namespace HUnity.Extensions
{
    public static class MathExtensions
    {
        public static bool NearlyEqual2D(this Vector3 lhs, in Vector3 rhs)
        {
            if (Mathf.Abs(lhs.x - rhs.x) > Mathf.Epsilon)
                return false;
        
            if (Mathf.Abs(lhs.y - rhs.y) > Mathf.Epsilon)
                return false;

            return true;
        }
    }
}