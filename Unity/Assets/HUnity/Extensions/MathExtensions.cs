using System;
using UnityEngine;

namespace HUnity.Extensions
{
    public static class MathExtensions
    {
        public const int CellSize = 1;
        
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

        public static System.Numerics.Vector2 ToSystemVector2(this Vector3 vector)
        {
            return new System.Numerics.Vector2(vector.x, vector.z);
        }

        public static System.Numerics.Vector3 ToSystemVector3(this Vector3 vector)
        {
            return new System.Numerics.Vector3(vector.x, vector.y, vector.z);
        }

        public static Vector2 ToUnityVector2(this System.Numerics.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector3 ToUnityVector3(this System.Numerics.Vector2 vector)
        {
            return  new Vector3(vector.X, 0, vector.Y);
        }
        
        public static Vector3 ToUnityVector3(this System.Numerics.Vector3 vector)
        {
            return  new Vector3(vector.X, vector.Y, vector.Z);
        }
        
        public static Vector3 NormalizeToCell(this Vector3 pos)
        {
            return new Vector3(
                (float)Math.Floor(pos.x / CellSize),
                0,
                (float)Math.Floor(pos.z / CellSize)
            );
        }
        
        public static Vector2 NormalizeToCell2D(this Vector3 pos)
        {
            return new Vector2(
                (float)Math.Floor(pos.x / CellSize),
                (float)Math.Floor(pos.z / CellSize)
            );
        }
        
        public static float DistanceSquared(this Vector2 a, Vector2 b)
        {
            Vector2 diff = a - b;
            return diff.sqrMagnitude;
        }
    }
}