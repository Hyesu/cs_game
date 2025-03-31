namespace HEngine.Actor
{
    public class HActorComponent
    {
        public readonly HActorComponentPreference Pref;

        public HActorComponent()
        {
            Pref = new();
            
            // if component need to tick, code "Pref.Tickable = true"
        }
        
        public virtual void Initialize()
        {
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
    }
}