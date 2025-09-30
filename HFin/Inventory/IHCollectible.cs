namespace Feature.Inventory
{
    public interface IHCollectible
    {
        int DataId { get; }
        long Amount { get; }
    
        // TODO: 스택에 따른 슬롯 구분 기능
        // long Uid { get; }
        // long MaxStack { get; }
        // bool IsStackable => 1 < MaxStack;

        bool Add(long amount);
        bool Sub(long amount);
    }   
}