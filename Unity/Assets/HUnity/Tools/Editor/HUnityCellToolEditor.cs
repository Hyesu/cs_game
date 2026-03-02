using System;
using System.Collections.Generic;
using System.Linq;
using Herring.Components;
using Herring.Systems;
using HFin.Cell;
using HUnity.Extensions;
using UnityEditor;
using UnityEngine;

namespace HUnity.Tools.Editor
{
    [CustomEditor((typeof(HUnityCellToolComponent)))]
    public class HUnityCellToolEditor : UnityEditor.Editor
    {
        private readonly CellSystem _cellSys = new();
        private readonly List<HCellIndex> _indices = new();
        
        private (int Min, int Max) _rowBoundary = (0, 0);
        private (int Min, int Max) _colBoundary = (0, 0);
        private int _gridSize = 0;
        
        private int _selectedGridIndex = -1;
        private HCellIndex? _selectedCellIndex;

        private void OnEnable()
        {
            RefreshCellBoundary();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            DrawRefreshButton();
            DrawCellGridButtons();
            DrawSelectedCell();
        }

        private void DrawRefreshButton()
        {
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Refresh", GUILayout.Height(30)))
            {
                OnClickedRefreshButton();
            }
        }

        private void DrawCellGridButtons()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(GetRowColLabel(), EditorStyles.boldLabel);

            if (_gridSize == 0)
            {
                return;
            }

            var labels = _indices
                .Select(x => GetCellLabel(x.Row, x.Col))
                .ToArray();

            _selectedGridIndex = GUILayout.SelectionGrid(_selectedGridIndex, labels, _gridSize);
    
            if (GUI.changed && _selectedGridIndex >= 0 && _selectedGridIndex < _indices.Count)
            {
                var selected = _indices[_selectedGridIndex];
                OnClickedGridButton(selected.Row, selected.Col);
            }
            
            ///////////// Local Methods
            string GetRowColLabel()
            {
                return $"Row({_rowBoundary.Min} ~ {_rowBoundary.Max}) Col({_colBoundary.Min} ~ {_colBoundary.Max})";
            }

            string GetCellLabel(int row, int col)
            {
                var cell = _cellSys.GetByIndex(new(row, col));
                if (cell)
                {
                    return string.IsNullOrEmpty(cell.CellName) ? $"{row} {col}" : cell.CellName;
                }

                return string.Empty;
            }
        }

        private void DrawSelectedCell()
        {
            if (!_selectedCellIndex.HasValue)
            {
                return;
            }

            var cellIndex = _selectedCellIndex.Value;
            var cellComponent = _cellSys.GetByIndex(cellIndex);
            
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(GetCellNameLabel(), EditorStyles.boldLabel);

            if (cellComponent)
            {
                EditorGUILayout.LabelField($"Name: {cellComponent.CellName}", EditorStyles.label);
                EditorGUILayout.LabelField($"Desc: {cellComponent.CellDesc}", EditorStyles.label);
                
                if (GUILayout.Button("Focus", GUILayout.Height(30)))
                {
                    OnClickedFocusButton(cellComponent);
                }
            }
            else
            {
                if (GUILayout.Button("Create Cell", GUILayout.Height(30)))
                {
                    OnClickedCreateCellButton(cellIndex);
                }
            }
            
            // ////////////////// Local Methods
            string GetCellNameLabel()
            {
                return cellComponent != null
                    ? $"{cellComponent.gameObject.name} ({cellIndex.Row}, {cellIndex.Col})"
                    : $"Empty Cell ({cellIndex.Row}, {cellIndex.Col})";
            }
        }

        private void RefreshCellBoundary()
        {
            _cellSys.Reload();
            
            if (_cellSys.CellCount <= 0)
            {
                _rowBoundary = new(0, 0);
                _colBoundary = new(0, 0);
            }
            else
            {
                var minRow = int.MaxValue;
                var maxRow = int.MinValue;
                var minCol = int.MaxValue;
                var maxCol = int.MinValue;

                foreach (var index in _cellSys.AllIndices)
                {
                    minRow = Math.Min(minRow, index.Row);
                    maxRow = Math.Max(maxRow, index.Row);
                    minCol = Math.Min(minCol, index.Col);
                    maxCol = Math.Max(maxCol, index.Col);
                }

                _rowBoundary = new(minRow, maxRow);
                _colBoundary = new(minCol, maxCol);
            }

            _indices.Clear();
            _gridSize = 0;
            
            int startRow = _rowBoundary.Min - 1;
            int endRow = _rowBoundary.Max + 1;
            int startCol = _colBoundary.Min - 1;
            int endCol = _colBoundary.Max + 1;
            int rowCount = endRow - startRow;
            int colCount = endCol - startCol;

            if (rowCount < 0 || colCount < 0)
            {
                Debug.LogError($"failed to calculate row/col - rowCount({rowCount}) colCount({colCount})");
                return;
            }

            _indices.AddRange(Enumerable.Range(startRow, rowCount + 1)
                .SelectMany(row => Enumerable.Range(startCol, colCount + 1)
                    .Select(col => new HCellIndex(row, col))));
            
            _gridSize = colCount + 1;
        }
        private void OnClickedRefreshButton()
        {
            RefreshCellBoundary();
        }

        private void OnClickedGridButton(int row, int col)
        {
            _selectedCellIndex = new(row, col);
        }

        private void OnClickedFocusButton(CellComponent cell)
        {
            Selection.activeObject = cell.gameObject;
            SceneView.lastActiveSceneView?.FrameSelected();
        }

        private void OnClickedCreateCellButton(HCellIndex cellIndex)
        {
            var cell = _cellSys.GetByIndex(cellIndex);
            if (cell)
            {
                Debug.LogWarning($"already cell created at index({cellIndex})");
                return;
            }

            var cellRootObj = _cellSys.GetCellRootGameObject();
            var toolComponent = (HUnityCellToolComponent)target;
            var cellObj = cellRootObj.AddChild(toolComponent.cellPrefab, $"Cell_{cellIndex.Row}_{cellIndex.Col}");
            var position = cellIndex.ToWorldPosByFlatTop(_cellSys.CellRadius);
            cellObj.transform.position = position.ToUnityVector3();

            RefreshCellBoundary();
        }
    }
}