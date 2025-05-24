using System;
using UnityEngine;

namespace HUnity.Core
{
    public class HDragAndDropEventListener
    {
        private readonly Action<Vector2> _onStarted;
        private readonly Action<Vector2> _onUpdated;
        private readonly Action<Vector2> _onEnded;

        private bool _isDragging;
        private Vector2 _startPos;
        
        public bool IsDragging => _isDragging;
        public Vector2 StartPosition => _startPos;

        public HDragAndDropEventListener(Action<Vector2> onStarted, Action<Vector2> onUpdated, Action<Vector2> onEnded)
        {
            _onStarted = onStarted;
            _onUpdated = onUpdated;
            _onEnded = onEnded;
        }

        public void Press(Vector2 mousePosition)
        {
            if (!_isDragging)
            {
                _isDragging = true;
                _startPos = mousePosition;
                _onStarted.Invoke(mousePosition);
            }
            else
            {
                _onUpdated.Invoke(mousePosition);
            }
        }

        public void Release(Vector2 mousePosition)
        {
            if (!_isDragging)
            {
                return;
            }
            
            _isDragging = false;
            _onEnded.Invoke(mousePosition);
        }
    }
}