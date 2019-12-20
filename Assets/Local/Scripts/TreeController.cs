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
    public int waterReward = 1;
    public int waterCost = 5;

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
        PlayerStats.instance.ChangeSync(-hardChopPenalty);
        Inventory.instance.IncreaseWoodAmount(treeHardChopReward);
    }

    public void SoftChop()
    {
        lastChopTick = TickManager.global.tickCount;
        softChopCount++;
        PlayerStats.instance.ChangeSync(-softChopPenalty);

        Inventory.instance.IncreaseWoodAmount(treeSoftChopReward);
        if (softChopCount >= softChopThreshold)
        {
            DestroyTree();
        }
    }

    public void Water()
    {
        if (softChopCount > 0)
        {
            PlayerStats.instance.ChangeSync(waterReward);
            softChopCount--;
            PlayerStats.instance.playerCurrentThirst -= waterCost;
        }
    }

    void DestroyTree()
    {
        Destroy(this.gameObject);
    }
}
