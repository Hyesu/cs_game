using System;
using System.Numerics;

namespace HFin.Cell
{
    public static class HCellHelper
    {
        public static readonly HCellIndex[] HexDirections = new HCellIndex[] {
            new(+1,  0), // E
            new(+1, -1), // NE
            new( 0, -1), // NW
            new(-1,  0), // W
            new(-1, +1), // SW
            new( 0, +1)  // SE
        };

        public static HCellIndex Add(this HCellIndex lhs, HCellIndex rhs)
        {
            return new(lhs.Row + rhs.Row, lhs.Col + rhs.Col);
        }
        
        // 윗면이 평평한 정육각형 타일
        public static HCellIndex ToHexIndexByFlatTop(this Vector2 worldPos, float hexRadius)
        {
            // 1. 월드 좌표 → Axial 좌표 (q, r)
            var q = (Math.Sqrt(3f) / 3f * worldPos.X - 1f / 3f * worldPos.Y) / hexRadius;
            var r = (2f / 3f * worldPos.Y) / hexRadius;

            // 2. Axial → Cube 변환
            var x = q;
            var z = r;
            var y = -x - z;

            // 3. 반올림
            var rx = Math.Round(x);
            var ry = Math.Round(y);
            var rz = Math.Round(z);

            // 4. 정합 보정 (x + y + z = 0 유지)
            var dx = Math.Abs(rx - x);
            var dy = Math.Abs(ry - y);
            var dz = Math.Abs(rz - z);

            if (dx > dy && dx > dz)
                rx = -ry - rz;
            else if (dy > dz)
                ry = -rx - rz;
            else
                rz = -rx - ry;

            // 5. Cube → Axial로 되돌리기
            int qInt = (int)rx;
            int rInt = (int)rz;

            return new(qInt, rInt);
        }   
        
        public static Vector3 ToWorldPosByFlatTop(this HCellIndex cellIndex, float hexRadius)
        {
            var q = cellIndex.Row;
            var r = cellIndex.Col;

            var x = hexRadius * (Math.Sqrt(3f) * q + Math.Sqrt(3f) / 2f * r);
            var y = hexRadius * (3f / 2f * r);

            return new((float)x, 0f, (float)y);
        }
    }
}

