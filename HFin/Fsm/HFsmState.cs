namespace HFin.Fsm
{
    public class HFsmState
    {
        private HFsmTransition _transition;
    
        // fsm machine에서만 호출
        public void Enter(IHFsmTransitionArg arg)
        {
            _transition = HFsmTransition.None;
            OnEnter(arg);
        }

        public void Update(float dt)
        {
            OnUpdate(dt);
        }

        public void Exit()
        {
            OnExit();
        }

        public bool TryGetTransition(out HFsmTransition transition)
        {
            transition = _transition;
            return transition != HFsmTransition.None;
        }

        // 하위에서 override하여 기능 확장
        protected virtual void OnEnter(IHFsmTransitionArg arg)
        {
        }

        protected virtual void OnUpdate(float dt)
        {
        }

        protected virtual void OnExit()
        {
        }
    
        // 트랜지션을 명령하고 시픈 경우 state 하위 타입에서 호출
        protected void SetTransition(HFsmTransition transition)
        {
            _transition = transition;
        }
    }   
}