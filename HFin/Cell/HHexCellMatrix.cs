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
        public IEnumerable<T> Cells => _cells.Values;
        public IEnumerable<HCellIndex> Indices => _cells.Keys;

        public HHexCellMatrix(float radius)
        {
            Radius = radius;
            
            _cells = new();
            _adjacentCells = new();
        }

        public T Get(HCellIndex index)
        {
            return _cells.GetValueOrDefault(index);
        }

        public T GetByPosition(Vector2 pos)
        {
            var index = pos.ToHexIndexByFlatTop(Radius);
            return Get(index);
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

            var dstCell = GetByPosition(dst);
            if (dstCell != null && !dstCell.IsTraversable)
            {
                return false;
            }

            return FindPathByAStar(src, dst, out path);
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

        private bool FindPathByAStar(Vector2 src, Vector2 dst, out List<Vector2> path)
        {
            path = null;

            var srcIndex = src.ToHexIndexByFlatTop(Radius);
            var dstIndex = dst.ToHexIndexByFlatTop(Radius);
            if (!_cells.ContainsKey(srcIndex) || !_cells.ContainsKey(dstIndex))
            {
                return false;
            }

            // f = g + h, g: 실제 비용, h: 목적지까지 추정 거리
            var openSet = new PriorityQueue<HCellIndex, double>();
            var gCost = new Dictionary<HCellIndex, double>();
            var cameFrom = new Dictionary<HCellIndex, HCellIndex>();

            gCost[srcIndex] = 0;
            openSet.Enqueue(srcIndex, Vector2.Distance(src, dst));

            while (openSet.TryDequeue(out var current, out _))
            {
                if (current == dstIndex)
                {
                    path = ReconstructPath(src, dst, srcIndex, dstIndex, cameFrom);
                    return true;
                }

                var currentCell = _cells[current];
                foreach (var neighbor in GetAllAdjacent(currentCell))
                {
                    if (!neighbor.IsTraversable)
                    {
                        continue;
                    }

                    var neighborCenter = neighbor.GetCenter2D();
                    var neighborIndex = neighborCenter.ToHexIndexByFlatTop(Radius);
                    var tentativeG = gCost[current] + Vector2.Distance(currentCell.GetCenter2D(), neighborCenter);

                    if (!gCost.TryGetValue(neighborIndex, out var existingG) || tentativeG < existingG)
                    {
                        gCost[neighborIndex] = tentativeG;
                        cameFrom[neighborIndex] = current;
                        openSet.Enqueue(neighborIndex, tentativeG + Vector2.Distance(neighborCenter, dst));
                    }
                }
            }

            return false;
        }

        private List<Vector2> ReconstructPath(Vector2 src, Vector2 dst, HCellIndex srcIndex, HCellIndex dstIndex, Dictionary<HCellIndex, HCellIndex> cameFrom)
        {
            // cameFrom을 역추적하여 srcIndex → dstIndex 순서로 스택에 쌓음
            var cellPath = new Stack<HCellIndex>();
            var current = dstIndex;
            while (current != srcIndex)
            {
                cellPath.Push(current);
                current = cameFrom[current];
            }
            cellPath.Push(srcIndex);

            var result = new List<Vector2> { src };
            foreach (var index in cellPath)
            {
                var center = _cells[index].GetCenter2D();
                if (result[^1] != center)
                {
                    result.Add(center);
                }
            }

            if (result[^1] != dst)
            {
                result.Add(dst);
            }

            return result;
        }
    }   
}