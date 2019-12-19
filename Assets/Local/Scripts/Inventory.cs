using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region SINGLETON
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Trying to create more than 1 inventory");
            return;
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public List<Item> items = new List<Item>();
    public int stone_amount;
    public int wood_amount;

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
