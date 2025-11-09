using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using HEngine.Extensions;

namespace HFin.Cell
{   
    public class HHexCellMatrix<T> where T : class, IHCell
    {
        public readonly float Radius;
        
        private readonly Dictionary<HCellIndex, T> _cells;
        private Dictionary<T, ImmutableArray<T>> _adjacentCells;
        
        public int CellCount => _cells.Count;
        public IEnumerable<T> Cells => _cells.Values;

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
            var minDesiredDistance = Vector2.Distance(src, dst);
            return FindFirstPathByBfs(src, dst, minDesiredDistance, out path);
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

        private struct PathCandidate
        {
            public double Distance;
            public readonly List<Vector2> Path;
            public readonly HashSet<HCellIndex> Marked;

            public PathCandidate(double distance, List<Vector2> path, HashSet<HCellIndex> marked)
            {
                Distance = distance;
                Path = path;
                Marked = marked;
            }

            public PathCandidate Clone()
            {
                var cloned = new PathCandidate(Distance, new(), new());
                cloned.Path.AddRange(Path);
                cloned.Marked.AddRange(Marked);

                return cloned;
            }
        }
        private bool FindFirstPathByBfs(Vector2 src, Vector2 dst, double minDesiredDistance, out List<Vector2> path)
        {
            path = null;

            var distanceBetweenCells = 2 * Radius;
            var distanceSquaredBetweenCells = distanceBetweenCells * distanceBetweenCells;

            Queue<PathCandidate> progresses = new();
            List<PathCandidate> completes = new();
            progresses.Enqueue(new(0f, new(){ src }, new()));
            
            while (progresses.TryDequeue(out var progress))
            {
                // 도착지까지 한 셀만에 이동 가능한지 확인하여 길찾기 종료 시도
                var lastSegment = progress.Path.Last();
                var distanceSquaredToDst = Vector2.DistanceSquared(lastSegment, dst);
                if (distanceSquaredToDst <= distanceSquaredBetweenCells)
                {
                    progress.Path.Add(dst);
                    progress.Distance += Math.Sqrt(distanceSquaredToDst);
                    if (progress.Distance <= minDesiredDistance)
                    {
                        path = progress.Path;
                        return true;
                    }
                    
                    completes.Add(progress);
                    continue;
                }
                
                // 길찾기 종료를 하지 못했으므로, 다음 인접셀을 후보에 등록
                var lastIndex = lastSegment.ToHexIndexByFlatTop(Radius);
                if (!_cells.TryGetValue(lastIndex, out var lastCell))
                {
                    continue;
                }

                var lastCellCenter = lastCell.GetCenter2D();
                if (lastCellCenter != lastSegment)
                {
                    // NOTE: 바로 continue 할 것이므로, 원본을 재사용할 수 있도록 함
                    progress.Path.Add(lastCellCenter);
                    progress.Distance += Vector2.Distance(lastSegment, lastCellCenter);
                    progress.Marked.Add(lastIndex);
                    
                    progresses.Enqueue(progress);
                    continue;
                }

                var adjacentCells = GetAllAdjacent(lastCell);
                foreach (var adjacentCell in adjacentCells)
                {
                    var adjacentCenter = adjacentCell.GetCenter2D();
                    var adjacentIndex = adjacentCenter.ToHexIndexByFlatTop(Radius);
                    if (progress.Marked.Contains(adjacentIndex))
                        continue;
                    
                    // NOTE: for를 돌면서 이전 값을 clone해야 하므로, 원본을 수정하지 않음
                    var newProgress = progress.Clone();
                    newProgress.Distance += distanceBetweenCells;
                    newProgress.Path.Add(adjacentCenter);
                    newProgress.Marked.Add(adjacentIndex);
                    
                    progresses.Enqueue(newProgress);
                }
            }

            if (0 >= completes.Count)
            {
                return false;
            }
            
            completes.Sort((lhs, rhs) => lhs.Distance < rhs.Distance ? -1 : 1);
            path = completes.First().Path;
            return true;
        }
    }   
}