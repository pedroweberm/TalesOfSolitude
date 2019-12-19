using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public int treeHardChopReward = 15;
    public int treeSoftChopReward = 5;
    public int softChopThreshold = 5;
    public int chopRestoreTime = 5;

    public int softChopPenalty = 2;
    public int hardChopPenalty = 5;

    public float interactableRadius = 3.0f;

    int softChopCount = 0;
    int lastChopTick;

    private void Update()
    {
        if (TickManager.global.tickCount - lastChopTick >= chopRestoreTime)
        {
            if (softChopCount > 0) softChopCount--;
            lastChopTick = TickManager.global.tickCount;
        }
    }

    public void HardChop()
    {
        DestroyTree();
        Inventory.instance.IncreaseWoodAmount(treeHardChopReward);
    }

    public void SoftChop()
    {
        lastChopTick = TickManager.global.tickCount;
        softChopCount++;

        Inventory.instance.IncreaseWoodAmount(treeSoftChopReward);
        if (softChopCount >= softChopThreshold)
        {
            DestroyTree();
        }
    }

    public void Water()
    {

    }

    void DestroyTree()
    {
        Destroy(this.gameObject);
    }
}
