using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;
    public List<Item> items = new List<Item>();
    public int stone_amount;
    public int wood_amount;

    private void Awake()
    {
        instance = this;
    }
    public void Add (Item item)
    {
        items.Add(item);
    }

    public void Remove (Item item)
    {
        items.Remove(item);
    }

    public void IncreaseStoneAmount(int amount)
    {
        stone_amount += amount;
    }

    public void IncreaseWoodAmount(int amount)
    {
        wood_amount += amount;
    }
}
