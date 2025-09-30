using System.Collections.Generic;

namespace Feature.Inventory
{
    public class HInventory<T> where T : class, IHCollectible
    {
        private readonly Dictionary<int, T> _inventory = new();

        public T? Find(int dataId)
        {
            return _inventory.GetValueOrDefault(dataId);
        }

        public bool Has(int dataId)
        {
            return _inventory.ContainsKey(dataId);
        }

        public bool Put(T item, out T putItem)
        {
            putItem = null;

            if (null == item || 0 >= item.DataId || 0 >= item.Amount)
            {
                return false;
            }

            if (!_inventory.TryGetValue(item.DataId, out var inventoryItem))
            {
                if (!_inventory.TryAdd(item.DataId, item))
                {
                    return false;
                }

                putItem = item;
                return true;
            }

            if (!inventoryItem.Add(item.Amount))
            {
                return false;
            }

            putItem = item;
            return true;
        }

        public bool Remove(int dataId, int amount)
        {
            if (0 >= dataId || 0 >= amount)
            {
                return false;
            }

            if (!_inventory.TryGetValue(dataId, out var item))
            {
                return false;
            }

            if (!item.Sub(amount))
            {
                return false;
            }

            if (0 >= item.Amount)
            {
                return _inventory.Remove(dataId);
            }

            return true;
        }
    }
}