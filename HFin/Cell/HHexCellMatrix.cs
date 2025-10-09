using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace HFin.Cell
{   
    public class HHexCellMatrix<T> where T : class, IHCell
    {
        private readonly float _radius;
        private readonly Dictionary<HCellIndex, T> _cells;
        
        private Dictionary<T, ImmutableArray<T>> _adjacents;

        public int CellCount => _cells.Count;

        public HHexCellMatrix(float radius)
        {
            _radius = radius;
            _cells = new();
            _adjacents = new();
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
            
            _adjacents = temp;
        }

        public void Add(IList<T> cells)
        {
            foreach (var cell in cells)
            {
                var v = cell.GetCenter2D();
                var index = v.ToHexIndexByFlatTop(_radius);
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

        public ImmutableArray<T> GetAllAdjacent(T cell)
        {
            return _adjacents.GetValueOrDefault(cell, ImmutableArray<T>.Empty);
        }
    }   
}