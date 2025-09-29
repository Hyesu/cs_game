namespace Feature.Inventory;

public interface IHCollectible
{
    long Uid { get; }
    int DataId { get; }
    long Amount { get; }
    
    // TODO: 스택에 따른 슬롯 구분 기능
    // long MaxStack { get; }
    // bool IsStackable => 1 < MaxStack;
}