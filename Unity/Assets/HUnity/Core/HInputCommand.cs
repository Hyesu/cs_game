using System;

namespace HUnity.Core
{
    [Flags]
    public enum HInputCommand
    {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Up = 1 << 2,
        Down = 1 << 3,
        MouseLeftDown = 1 << 10,
        MouseRightDown = 1 << 11,
        MouseLeftUp = 1 << 12,
        MouseRightUp = 1 << 13,
    }   
}