using System;

namespace HFin.Cell
{
    // TODO: 유니티에서 아직 공식적으로 C# 10.0을 지원하지를 않는다.
    //public readonly record struct HCellIndex(int Row, int Col);
    public readonly struct HCellIndex : IEquatable<HCellIndex>
    {
        public readonly int Row;
        public readonly int Col;

        public HCellIndex(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public bool Equals(HCellIndex other) => Row == other.Row && Col == other.Col;
        public override bool Equals(object? obj) => obj is HCellIndex other && Equals(other);

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }

        public static bool operator ==(HCellIndex a, HCellIndex b) => a.Equals(b);
        public static bool operator !=(HCellIndex a, HCellIndex b) => !a.Equals(b);

        public override string ToString() => $"({Row}, {Col})";
    }
}