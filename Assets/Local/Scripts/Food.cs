using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item
{
    
    public float regenAmout;
    public Food cookedDrop;
    public int cookCost;

    public void Reset()
    {
        isFood = true;
    }

    public override void Cook()
    {
        if (Inventory.instance.wood_amount > cookCost && Inventory.instance.stone_amount > 5)
        {
            Inventory.instance.Remove(this);
            Inventory.instance.Add(cookedDrop);
            Inventory.instance.wood_amount -= cookCost;
            Inventory.instance.stone_amount -= 5;
        }
        
        
    }

    public override void Eat()
    {
        Inventory.instance.Remove(this);
        Inventory.instance.selectedItem = null;

        if (PlayerStats.instance.playerCurrentHunger + regenAmout > PlayerStats.instance.playerStartingHunger)
        {
            PlayerStats.instance.playerCurrentHunger = PlayerStats.instance.playerStartingHunger;
        }
        else
        {
            PlayerStats.instance.playerCurrentHunger += regenAmout;
        }

        if (PlayerStats.instance.currentHp + 0.5f * regenAmout > PlayerStats.instance.maxHp)
        {
            PlayerStats.instance.currentHp = PlayerStats.instance.maxHp;
        }
        else
        {
            PlayerStats.instance.currentHp += 0.5f * regenAmout;
        }
    }
}
