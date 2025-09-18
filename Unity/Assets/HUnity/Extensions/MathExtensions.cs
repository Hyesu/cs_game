using UnityEngine;

namespace HUnity.Extensions
{
    public static class MathExtensions
    {
        public static bool IsNearlyEquals(this Vector3 lhs, in Vector3 rhs, float tolerance = 1e-4f)
        {
            return Mathf.Abs(lhs.x - rhs.x) <= tolerance &&
                   Mathf.Abs(lhs.y - rhs.y) <= tolerance &&
                   Mathf.Abs(lhs.z - rhs.z) <= tolerance;
        }
        
        public static bool IsNearlyEquals(this Vector2 lhs, in Vector2 rhs, float tolerance = 1e-4f)
        {
            return Mathf.Abs(lhs.x - rhs.x) <= tolerance &&
                   Mathf.Abs(lhs.y - rhs.y) <= tolerance;
        }

        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }

        public static Vector3 ToVector3(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
    }
}