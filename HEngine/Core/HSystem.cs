using DesignTable.Core;

namespace HEngine.Core
{
    public class HSystem
    {
        protected DContext D;
        protected HSystemProvider Provider;
        
        public virtual void Initialize()
        {
        }

        public virtual void BeginPlay()
        {
        }

        public virtual void EndPlay()
        {
        }

        public void SetDataContext(DContext dCtx)
        {
            D = dCtx;
        }

        public void SetProvider(HSystemProvider provider)
        {
            Provider = provider;
        }

        protected T GetSystem<T>() where T : HSystem
        {
            return Provider?.GetSystem<T>();
        }
    }   
}