using System.Collections.Generic;
using System.Collections.Immutable;

namespace HFin.Cell
{
    public class HCellMatrix2D<T> where T : IHCell
    {
        private readonly int _cellSize;
        private readonly int _numDirection;

        public int CellCount => 0;

        public HCellMatrix2D(int cellSize, int numDirection)
        {
            _cellSize = cellSize;
            _numDirection = numDirection;
        }

        public void Add(IList<T> cells)
        {
        }

        public void Clear()
        {
        }

        public ImmutableArray<T> GetAllAdjacent(T cell)
        {
            return ImmutableArray<T>.Empty;
        }
    }   
}