using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public Transform InventoryHoldPoinBase { get; private set; }
    [field: SerializeField] public Transform InventoryHoldPoint { get; private set; }

    private int _capacity = 5;
    private List<Item> _items = new List<Item>();
    private float _divider = 2;

    public int Capacity => _capacity;
    public bool IsAbleToTake => _items.Count < Capacity;
    public bool IsEmpty => _items.Count == 0;

    public void AddToInventory(Item item)
    {
        _items.Add(item);
        InventoryHoldPoint.position = InventoryHoldPoint.position + (transform.up / _divider);
    }

    public Item TakeItems(FoodTypes foodType)
    {
        if (_items.Count > 0)
        {
            Item itemToRemove = TryFindWantedItem(foodType);

            if (itemToRemove != null)
            {
                return itemToRemove;
            }
        }

        return null;
    }

    public void RemoveFromInventory(Item item)
    {
        _items.Remove(item);
        InventoryHoldPoint.position = InventoryHoldPoint.position - (transform.up / _divider);
    }

    public Item TryFindWantedItem(FoodTypes foodType)
    {
        if (foodType != FoodTypes.Non)
        {
            Item item = _items.FirstOrDefault(i => i.FoodType == foodType);
            return item;
        }
        else
        {
            return _items[_items.Count-1];
        }
    }
}
