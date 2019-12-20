using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item
{
    
    public float regenAmout;
    public int ticksToCook;
    private int ticksCooking;
    public Food cookedDrop;
    public int cookCost;

    public new bool isFood = true;

    public override void Cook()
    {
        if (ticksToCook == -1)
        {
            return;
        }
        if (Inventory.instance.wood_amount > cookCost && Inventory.instance.stone_amount > 5)
        {
            Inventory.instance.Remove(this);
            Inventory.instance.Add(cookedDrop);
            Inventory.instance.wood_amount -= cookCost;
            Inventory.instance.stone_amount -= 5;
        }
        
        
    }

    public void Eat()
    {
        Inventory.instance.Remove(this);
        PlayerStats.instance.currentHp += regenAmout;
    }




}
