using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public int max_items;
    public int stone_amount;
    public int wood_amount;

    public Item selectedItem;

    public void SelectItem(int index)
    {
        if (index != -1)
        {
            selectedItem = items[index];
        }
        else
        {
            selectedItem = null;
        }
    }

    public bool Add (Item item)
    {
        if ( items.Count < max_items )
        {
            items.Add(item);
            if(items.Count == 1)
            {
                SelectItem(0);
            }
            return true;
        }
        else
        {
            return false;
        }
        
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
