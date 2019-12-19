using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public int treeHardChopReward = 15;
    public int treeSoftChopReward = 5;

    public float interactableRadius = 3.0f;

    public void HardChop()
    {
        DestroyTree();
        Inventory.instance.IncreaseWoodAmount(treeHardChopReward);
    }

    public void SoftChop()
    {
        Inventory.instance.IncreaseWoodAmount(treeSoftChopReward);
    }

    public void Water()
    {

    }

    public void DestroyTree()
    {
        Destroy(this.gameObject);
    }
}
