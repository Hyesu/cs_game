using Feature.IdGenerator;

namespace Feature.Inventory;

public class HInventory<T> where T : class, IHCollectible
{
    private readonly IHIdGenerator _idGenerator;

    public HInventory(IHIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }
    
    public T? Find(long uid)
    {
        return null;
    }

    public T? FindByDataId(int dataId)
    {
        return null;
    }

    public bool Has(int dataId)
    {
        return false;
    }
    
    // TODO: 스택 개념에 의한 슬롯 분할만들 때 OUT 다듬기
    public bool Put(T item, out T putItem)
    {
        putItem = null;
        return false;
    }

    public bool Remove(long uid, int amount)
    {
        return false;
    }

    public bool Take(long uid, int amount, out T taken)
    {
        taken = null;
        return false;
    }
}