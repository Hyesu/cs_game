using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Numerics;

namespace HFin.Cell
{   
    public class HHexCellMatrix<T> where T : class, IHCell
    {
        public readonly float Radius;
        
        private readonly Dictionary<HCellIndex, T> _cells;
        private Dictionary<T, ImmutableArray<T>> _adjacentCells;

        public int CellCount => _cells.Count;

        public HHexCellMatrix(float radius)
        {
            Radius = radius;
            
            _cells = new();
            _adjacentCells = new();
        }

        public ImmutableArray<T> GetAllAdjacent(T cell)
        {
            return _adjacentCells.GetValueOrDefault(cell, ImmutableArray<T>.Empty);
        }
        
        public void Add(IList<T> cells)
        {
            foreach (var cell in cells)
            {
                var v = cell.GetCenter2D();
                var index = v.ToHexIndexByFlatTop(Radius);
                if (!_cells.TryAdd(index, cell))
                    throw new InvalidOperationException($"duplicate hex index - vertex({v}) index({index})");
            }
            
            RebuildAdjacent();
        }

        public void Clear()
        {
            _cells.Clear();
            RebuildAdjacent();
        }

        public bool TryFindPath(Vector2 src, Vector2 dst, out List<Vector2> path)
        {
            path = null;
            return false;
        }

        private void RebuildAdjacent()
        {
            var temp = new Dictionary<T, ImmutableArray<T>>();
            foreach (var (index, cell) in _cells)
            {
                var adjacentCells = new List<T>();
                foreach (var direction in HCellHelper.HexDirections)
                {
                    var neighborIndex = index.Add(direction);
                    if (_cells.TryGetValue(neighborIndex, out var neighborCell))
                        adjacentCells.Add(neighborCell);
                }
                
                temp.Add(cell, adjacentCells.ToImmutableArray());
            }
            
            _adjacentCells = temp;
        }
    }   
}