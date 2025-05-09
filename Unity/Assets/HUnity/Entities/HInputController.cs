using System.Collections.Generic;
using UnityEngine;

namespace HUnity.Entities
{
    public class HInputController
    {
        private readonly Dictionary<KeyCode, HInputCommand> _commandKeys = new();
        private HInputCommand _command;

        public void Initialize()
        {
            // TODO: 키맵 설정을 통해 할 수 있도록 해야 함
            _commandKeys.Add(KeyCode.W, HInputCommand.Up);
            _commandKeys.Add(KeyCode.A, HInputCommand.Left);
            _commandKeys.Add(KeyCode.S, HInputCommand.Down);
            _commandKeys.Add(KeyCode.D, HInputCommand.Right);
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
            foreach (var (keyCode, cmd) in _commandKeys)
            {
                if (Input.GetKey(keyCode))
                {
                    _command |= cmd;
                }
            }
        }
    }
}