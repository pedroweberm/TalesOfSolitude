﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string item_name = "Item";
    public Sprite icon = null;
    public bool isResource = false;

}
