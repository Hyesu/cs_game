using System;
using System.Collections.Generic;

namespace Feature.Fsm
{
    public class HFsmMachine
    {
        private readonly Dictionary<int, HFsmState> _states = new();
        private readonly Dictionary<int, HFsmTransitionMap> _transitions = new();
        
        private HFsmState _curState;
        private HFsmTransitionMap _curTransitionMap;
    
        public void AddState(HFsmState state, bool isInitialState = false)
        {
            var hash = state.GetType().GetHashCode();
            _states.Add(hash, state);

            var transitionMap = new HFsmTransitionMap();
            _transitions.Add(hash, transitionMap);

            if (isInitialState)
            {
                _curState = state;
                _curTransitionMap = transitionMap;
            }
        }

        public void AddTransition(HFsmState prevState, HFsmTransition transition, HFsmState nextState)
        {
            if (HFsmTransition.None == transition)
                throw new InvalidOperationException($"cannot make transition with None transition");
            
            var prevHash = prevState.GetType().GetHashCode();
            var nextHash = nextState.GetType().GetHashCode();

            _transitions[prevHash].Add(transition, nextHash);
        }

        public void Start()
        {
            if (null == _curState)
                throw new InvalidOperationException($"not found initial state for fsm machine");
            
            _curState.Enter();
        }

        public void Update(float dt)
        {
            _curState.Update(dt);

            if (_curState.TryGetTransition(out var transition)
                && _curTransitionMap.TryGetNextStateHash(transition, out var nextStateHash))
            {
                var nextState = _states.GetValueOrDefault(nextStateHash);
                var nextTransitionMap = _transitions.GetValueOrDefault(nextStateHash);
                
                _curState.Exit();

                _curState = nextState!;
                _curTransitionMap = nextTransitionMap!;
                _curState.Enter();
            }
        }
    }   
}