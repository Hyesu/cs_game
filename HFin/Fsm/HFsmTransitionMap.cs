using System.Collections.Generic;

namespace Feature.Fsm
{
    public class HFsmTransitionMap
    {
        private readonly Dictionary<HFsmTransition, int> _transitions = new();

        public bool TryGetNextStateHash(HFsmTransition transition, out int nextStateHash)
        {
            return _transitions.TryGetValue(transition, out nextStateHash);
        }
        
        public void Add(HFsmTransition transition, int nextStateHash)
        {
            _transitions.Add(transition, nextStateHash);
        }
    }   
}