using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace HUnity.Core
{
    public class HInputController
    {
        private readonly Dictionary<Key, HInputCommand> _keyCommands = new();
        private readonly Dictionary<Key, HInputCommand> _keyUpCommands = new();
        private HInputCommand _command;
        
        private HDragAndDropEventListener _dragAndDropListener;
        private bool _active = true;
        
        public bool IsActive => _active;

        public void Initialize()
        {
            // TODO: 키맵 설정을 통해 할 수 있도록 해야 함
            _keyCommands.Add(Key.W, HInputCommand.Up);
            _keyCommands.Add(Key.A, HInputCommand.Left);
            _keyCommands.Add(Key.S, HInputCommand.Down);
            _keyCommands.Add(Key.D, HInputCommand.Right);
            
            _keyUpCommands.Add(Key.R, HInputCommand.Rotation);
        }

        public void Update(float dt)
        {
            ProduceCommands();
        }

        public void Activate(bool active)
        {
            if (active == _active)
            {
                return;
            }
            
            _active = active;
            Debug.Log($"input controller activated({!_active} -> {_active})");
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
            foreach (var (key, cmd) in _keyCommands)
            {
                if (Keyboard.current[key].isPressed)
                {
                    _command |= cmd;
                }
            }
            
            // key up inputs
            foreach (var (key, cmd) in _keyUpCommands)
            {
                if (Keyboard.current[key].wasReleasedThisFrame)
                {
                    _command |= cmd;
                }
            }
            
            // mouse inputs
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Mouse.current.leftButton.isPressed)
                {
                    _command |= HInputCommand.MouseLeftDown;
                    _dragAndDropListener?.Press(Mouse.current.position.value);
                }
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    _dragAndDropListener?.Press(Mouse.current.position.value);
                }
                if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    _command |= HInputCommand.MouseLeftUp;
                    _dragAndDropListener?.Release(Mouse.current.position.value);
                }

                if (Mouse.current.rightButton.isPressed)
                {
                    _command |= HInputCommand.MouseRightDown;
                }
                if (Mouse.current.rightButton.wasReleasedThisFrame)
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