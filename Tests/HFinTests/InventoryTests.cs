using Feature.IdGenerator;
using Feature.Inventory;

namespace FeatureTest;

public class InventoryTests : FinTestFixture
{
    private readonly IHIdGenerator _idGenerator = new HIncrementalGenerator();
    private readonly int _dummyDataId = 1;
    
    class TestItem : IHCollectible
    {
        private readonly int _dataId;
        
        private long _amount;

        public int DataId => _dataId;
        public long Amount => _amount;

        public TestItem(int dataId, long amount)
        {
            _dataId = dataId;
            _amount = amount;
        }

        public bool Add(long amount)
        {
            _amount += amount;
            return true;
        }

        public bool Sub(long amount)
        {
            _amount -= amount;
            return true;
        }
    }
    
    /////////////////////////////////
    [Test]
    public void TestPut_Fail()
    {
        var inventory = new HInventory<TestItem>();
        var amount = Random.Shared.Next(1, 10);

        // 인수 테스트
        {
            var errItem1 = new TestItem(0, amount);
            var errItem2 = new TestItem(_dummyDataId, 0);

            var errResult1 = inventory.Put(null!, out _);
            var errResult2 = inventory.Put(errItem1, out _);
            var errResult3 = inventory.Put(errItem2, out _);
            
            Assert.That(errResult1, Is.False);
            Assert.That(errResult2, Is.False);
            Assert.That(errResult3, Is.False);
        }
    }

    [Test]
    public void TestPut_Success()
    {
        var inventory = new HInventory<TestItem>();
        var amount = Random.Shared.Next(1, 10);
        var item = new TestItem(_dummyDataId, amount);

        var result = inventory.Put(item, out var putItem);
        Assert.That(result, Is.True);
        Assert.That(putItem, Is.Not.Null);
        Assert.That(putItem.DataId, Is.EqualTo(item.DataId));
        Assert.That(putItem.Amount, Is.EqualTo(item.Amount));
        
        // 인벤토리 체크
        Assert.That(inventory.Has(item.DataId), Is.True);

        var inventoryItem = inventory.Find(putItem.DataId);
        Assert.That(inventoryItem, Is.Not.Null);
        Assert.That(inventoryItem.DataId, Is.EqualTo(item.DataId));
        Assert.That(inventoryItem.Amount, Is.EqualTo(item.Amount));
    }

    [Test]
    public void TestRemove_Fail()
    {
        var inventory = new HInventory<TestItem>();
        var amount = Random.Shared.Next(1, 10);
        var item = new TestItem(_dummyDataId, amount);
        
        inventory.Put(item, out var putItem);
        
        // 인수 테스트
        {
            var errResult1 = inventory.Remove(0, amount);
            var errResult2 = inventory.Remove(int.MaxValue, amount);
            
            Assert.That(errResult1, Is.False);
            Assert.That(errResult2, Is.False);
        }
    }

    [Test]
    public void TestRemove_Success()
    {
        var inventory = new HInventory<TestItem>();
        var amount = Random.Shared.Next(2, 10);
        var removeAmount = Random.Shared.Next(1, amount);
        var remainAmount = amount - removeAmount;
        var item = new TestItem(_dummyDataId, amount);
        
        inventory.Put(item, out var putItem);

        // 일부 삭제하고 확인
        var result1 = inventory.Remove(putItem.DataId, removeAmount);
        Assert.That(result1, Is.True);
        Assert.That(inventory.Has(item.DataId), Is.True);

        var inventoryItem1 = inventory.Find(putItem.DataId);
        Assert.That(inventoryItem1, Is.Not.Null);
        Assert.That(inventoryItem1.DataId, Is.EqualTo(item.DataId));
        Assert.That(inventoryItem1.Amount, Is.EqualTo(remainAmount));
        
        // 나머지 다 삭제하고 확인
        var result2 = inventory.Remove(putItem.DataId, remainAmount);
        Assert.That(result2, Is.True);
        Assert.That(inventory.Has(item.DataId), Is.False);

        var inventoryItem2 = inventory.Find(putItem.DataId);
        Assert.That(inventoryItem2, Is.Null);
    }
}