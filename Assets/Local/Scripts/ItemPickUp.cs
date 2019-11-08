using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        if (item.name == "Stone") {
            Inventory.instance.IncreaseStoneAmount(1);
        }
        else if (item.name == "Wood") {
            Inventory.instance.IncreaseWoodAmount(1);
        }
        else
        {
            Inventory.instance.Add(item);
        }
        
        Destroy(gameObject);
    }
}
