using DesignTable.Core;

namespace HEngine.Core
{
    public interface IHWorld
    {
        DContext  GetDContext();
        public T GetSystem<T>() where T : HSystem;
    }   
}