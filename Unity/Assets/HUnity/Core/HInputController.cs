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
        private bool _activeKeyInput = true;
        private bool _activeMouseInput = true;
        
        public bool IsActiveKeyInput => _activeKeyInput;
        public bool IsActiveMouseInput => _activeMouseInput;

        public void Initialize()
        {
            // TODO: 키맵 설정을 통해 할 수 있도록 해야 함
            _keyUpCommands.Add(Key.UpArrow, HInputCommand.Up);
            _keyUpCommands.Add(Key.LeftArrow, HInputCommand.Left);
            _keyUpCommands.Add(Key.DownArrow, HInputCommand.Down);
            _keyUpCommands.Add(Key.RightArrow, HInputCommand.Right);
            
            _keyUpCommands.Add(Key.Backquote, HInputCommand.Cheat);
        }

        public bool Update(float dt)
        {
            return ProduceCommands();
        }

        public void ActivateMouseInput(bool active)
        {
            if (active == _activeMouseInput)
            {
                return;
            }
            
            _activeMouseInput = active;
            Debug.Log($"mouse input activated({!_activeKeyInput} -> {_activeKeyInput})");
        }

        public void ActivateKeyInput(bool active)
        {
            if (active == _activeKeyInput)
            {
                return;
            }
            
            _activeKeyInput = active;
            Debug.Log($"key input activated({!_activeKeyInput} -> {_activeKeyInput})");
        }

        public HInputCommand FlushCommands()
        {
            var command = _command;
            _command = 0; // reset

            return command;
        }

        private bool ProduceCommands()
        {
            if (IsActiveKeyInput)
            {
                foreach (var (key, cmd) in _keyCommands)
                {
                    if (Keyboard.current[key].isPressed)
                    {
                        _command |= cmd;
                    }
                }
            
                foreach (var (key, cmd) in _keyUpCommands)
                {
                    if (Keyboard.current[key].wasReleasedThisFrame)
                    {
                        _command |= cmd;
                    }
                }
            }

            if (IsActiveMouseInput)
            {
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

            return _command != HInputCommand.None;
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