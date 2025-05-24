using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HUnity.Core
{
    public class HInputController
    {
        private readonly Dictionary<KeyCode, HInputCommand> _keyCommands = new();
        private HInputCommand _command;
        
        private HDragAndDropEventListener _dragAndDropListener;

        public void Initialize()
        {
            // TODO: 키맵 설정을 통해 할 수 있도록 해야 함
            _keyCommands.Add(KeyCode.W, HInputCommand.Up);
            _keyCommands.Add(KeyCode.A, HInputCommand.Left);
            _keyCommands.Add(KeyCode.S, HInputCommand.Down);
            _keyCommands.Add(KeyCode.D, HInputCommand.Right);
        }

        public void Update(float dt)
        {
            ProduceCommands();
        }

        public HInputCommand FlushCommands()
        {
            var command = _command;
            _command = 0; // reset

            return command;
        }

        private void ProduceCommands()
        {
            // key inputs
            foreach (var (keyCode, cmd) in _keyCommands)
            {
                if (Input.GetKey(keyCode))
                {
                    _command |= cmd;
                }
            }

            // mouse inputs
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _command |= HInputCommand.MouseLeftDown;
                    _dragAndDropListener?.Press(Input.mousePosition);
                }
                if (Input.GetMouseButton(0))
                {
                    _dragAndDropListener?.Press(Input.mousePosition);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    _command |= HInputCommand.MouseLeftUp;
                    _dragAndDropListener?.Release(Input.mousePosition);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    _command |= HInputCommand.MouseRightDown;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    _command |= HInputCommand.MouseRightUp;
                }
            }
        }

        public void AddDragAndDropListener(HDragAndDropEventListener listener)
        {
            _dragAndDropListener = listener;
        }

        public void ClearDragAndDropListener()
        {
            _dragAndDropListener = null;
        }
    }
}