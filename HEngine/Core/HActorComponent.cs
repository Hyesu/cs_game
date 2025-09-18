namespace HEngine.Core
{
    public class HActorComponent
    {
        public HActorComponentPreference Pref;

        private HActor _owner;

        public HActorComponent()
        {
            Pref = new();
            
            // if component need to tick, code "Pref.Tickable = true"
        }
        
        public virtual void Initialize(HActor owner)
        {
            _owner = owner;
        }

        public virtual void BeginPlay()
        {
        }

        public virtual void Tick(float dt)
        {
        }

        public virtual void EndPlay()
        {
        }

        public HActor GetOwner()
        {
            return _owner;
        }
    }
}