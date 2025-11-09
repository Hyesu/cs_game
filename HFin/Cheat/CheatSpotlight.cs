using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using HEngine.Core;
using HEngine.Extensions;

namespace HFin.Cheat
{
    public class CheatSpotlight
    {
        private readonly Dictionary<string, Func<ImmutableArray<string>, HResultCode>> _executors = new();
        private readonly Dictionary<string, CheatAction> _actions = new();

        public IEnumerable<CheatAction> All => _actions.Values;
   
        public HResultCode Add(CheatAction action, Func<ImmutableArray<string>, HResultCode> executor)
        {
            if (string.IsNullOrEmpty(action.Command))
                return HResultCode.InvalidArgument;

            var safeCmd = action.Command.ToStringKey();
            _actions[safeCmd] = action;
            _executors[safeCmd] = executor;
            return HResultCode.Success;
        }

        public HResultCode Execute(string query)
        {
            if (string.IsNullOrEmpty(query))
                return HResultCode.InvalidArgument;

            var tokens = query.Split();
            var safeCmd = tokens[0].ToStringKey();
            if (!_executors.TryGetValue(safeCmd, out var executor))
                return HResultCode.CheatSpotlightNotFoundCommand;

            var args = tokens.Skip(1).ToImmutableArray();
            return executor(args);
        }
    }   
}