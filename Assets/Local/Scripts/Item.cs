using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string item_name = "Item";
    public Sprite icon = null;
    public bool isResource = false;
    public bool isFood = false;

    public virtual void Cook()
    {

    }
    public virtual void Use()
    {
        Debug.Log("Using item");
    }
}
