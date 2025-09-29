using Feature.IdGenerator;
using Feature.Inventory;

namespace FeatureTest;

public class InventoryTests : FinTestFixture
{
    private readonly IHIdGenerator _idGenerator = new HIncrementalGenerator();
    private readonly int _dummyDataId = 1;
    
    class TestItem : IHCollectible
    {
        private readonly long _uid;
        private readonly int _dataId;
        
        private long _amount;

        public long Uid => _uid;
        public int DataId => _dataId;
        public long Amount => _amount;

        public TestItem(long uid, int dataId, long amount)
        {
            _uid = uid;
            _dataId = dataId;
            _amount = amount;
        }
    }
    
    /////////////////////////////////
    [Test]
    public void TestPut_Fail()
    {
        var inventory = new HInventory<TestItem>(_idGenerator);
        var amount = Random.Shared.Next(1, 10);

        // 인수 테스트
        {
            var errItem1 = new TestItem(0, 0, amount);
            var errItem2 = new TestItem(0, _dummyDataId, 0);

            var errResult1 = inventory.Put(errItem1, out _);
            var errResult2 = inventory.Put(errItem2, out _);
            
            Assert.That(errResult1, Is.False);
            Assert.That(errResult2, Is.False);
        }
    }

    [Test]
    public void TestPut_Success()
    {
        var inventory = new HInventory<TestItem>(_idGenerator);
        var amount = Random.Shared.Next(1, 10);
        var item = new TestItem(0, _dummyDataId, amount);

        var result = inventory.Put(item, out var putItem);
        Assert.That(result, Is.True);
        Assert.That(putItem, Is.Not.Null);
        Assert.That(putItem.Uid, Is.GreaterThan(0));
        Assert.That(putItem.DataId, Is.EqualTo(item.DataId));
        Assert.That(putItem.Amount, Is.EqualTo(item.Amount));
        
        // 인벤토리 체크
        Assert.That(inventory.Has(item.DataId), Is.True);

        var inventoryItem = inventory.Find(putItem.Uid);
        Assert.That(inventoryItem, Is.Not.Null);
        Assert.That(inventoryItem.DataId, Is.EqualTo(item.DataId));
        Assert.That(inventoryItem.Amount, Is.EqualTo(item.Amount));

        var inventoryItemByDataId = inventory.FindByDataId(item.DataId);
        Assert.That(inventoryItemByDataId, Is.Not.Null);
        Assert.That(inventoryItemByDataId, Is.EqualTo(inventoryItem));
    }

    [Test]
    public void TestRemove_Fail()
    {
        var inventory = new HInventory<TestItem>(_idGenerator);
        var amount = Random.Shared.Next(1, 10);
        var item = new TestItem(0, _dummyDataId, amount);
        
        inventory.Put(item, out var putItem);
        
        // 인수 테스트
        {
            var errResult1 = inventory.Remove(0, amount);
            var errResult2 = inventory.Remove(long.MaxValue, amount);
            var errResult3 = inventory.Remove(putItem.Uid, 0);
            var errResult4 = inventory.Remove(putItem.Uid, amount + 1);
            
            Assert.That(errResult1, Is.False);
            Assert.That(errResult2, Is.False);
            Assert.That(errResult3, Is.False);
            Assert.That(errResult4, Is.False);
        }
    }

    [Test]
    public void TestRemove_Success()
    {
        var inventory = new HInventory<TestItem>(_idGenerator);
        var amount = Random.Shared.Next(2, 10);
        var removeAmount = Random.Shared.Next(1, amount);
        var remainAmount = amount - removeAmount;
        var item = new TestItem(0, _dummyDataId, amount);
        
        inventory.Put(item, out var putItem);

        // 일부 삭제하고 확인
        var result1 = inventory.Remove(putItem.Uid, removeAmount);
        Assert.That(result1, Is.True);
        Assert.That(inventory.Has(item.DataId), Is.True);

        var inventoryItem1 = inventory.Find(putItem.Uid);
        Assert.That(inventoryItem1, Is.Not.Null);
        Assert.That(inventoryItem1.DataId, Is.EqualTo(item.DataId));
        Assert.That(inventoryItem1.Amount, Is.EqualTo(remainAmount));
        
        // 나머지 다 삭제하고 확인
        var result2 = inventory.Remove(putItem.Uid, remainAmount);
        Assert.That(result2, Is.True);
        Assert.That(inventory.Has(item.DataId), Is.False);

        var inventoryItem2 = inventory.Find(putItem.Uid);
        Assert.That(inventoryItem2, Is.Null);
    }
    
    [Test]
    public void TestTake_Fail()
    {
        var inventory = new HInventory<TestItem>(_idGenerator);
        var amount = Random.Shared.Next(1, 10);
        var item = new TestItem(0, _dummyDataId, amount);
        
        inventory.Put(item, out var putItem);
        
        // 인수 테스트
        {
            var errResult1 = inventory.Take(0, amount, out _);
            var errResult2 = inventory.Take(long.MaxValue, amount, out _);
            var errResult3 = inventory.Take(putItem.Uid, 0, out _);
            var errResult4 = inventory.Take(putItem.Uid, amount + 1, out _);
            
            Assert.That(errResult1, Is.False);
            Assert.That(errResult2, Is.False);
            Assert.That(errResult3, Is.False);
            Assert.That(errResult4, Is.False);
        }
    }

    [Test]
    public void TestTake_Success()
    {
        var inventory = new HInventory<TestItem>(_idGenerator);
        var amount = Random.Shared.Next(2, 10);
        var takeAmount = Random.Shared.Next(1, amount);
        var remainAmount = amount - takeAmount;
        var item = new TestItem(0, _dummyDataId, amount);
        
        inventory.Put(item, out var putItem);

        // 일부 꺼내고 확인
        var result1 = inventory.Take(putItem.Uid, takeAmount, out var taken1);
        Assert.That(result1, Is.True);
        Assert.That(taken1, Is.Not.Null);
        Assert.That(taken1.Uid, Is.Not.EqualTo(putItem.Uid));
        Assert.That(taken1.DataId, Is.EqualTo(putItem.DataId));
        Assert.That(taken1.Amount, Is.EqualTo(takeAmount));
        Assert.That(inventory.Has(item.DataId), Is.True);

        var inventoryItem1 = inventory.Find(putItem.Uid);
        Assert.That(inventoryItem1, Is.Not.Null);
        Assert.That(inventoryItem1.DataId, Is.EqualTo(item.DataId));
        Assert.That(inventoryItem1.Amount, Is.EqualTo(remainAmount));
        
        // 나머지 다 꺼내고 확인
        var result2 = inventory.Take(putItem.Uid, remainAmount, out var taken2);
        Assert.That(result2, Is.True);
        Assert.That(taken2.Uid, Is.Not.EqualTo(putItem.Uid));
        Assert.That(taken2.Uid, Is.Not.EqualTo(taken1.Uid));
        Assert.That(taken2.DataId, Is.EqualTo(putItem.DataId));
        Assert.That(taken2.Amount, Is.EqualTo(remainAmount));
        Assert.That(inventory.Has(item.DataId), Is.False);

        var inventoryItem2 = inventory.Find(putItem.Uid);
        Assert.That(inventoryItem2, Is.Null);
    }
}