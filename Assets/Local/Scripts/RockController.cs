using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public int mineRewardAmount = 5;
    public int destroyRewardAmount = 15;
    public int mineThreshold = 5;

    public int minePenalty = 2;
    public int destroyPenalty = 5;

    int mineCount = 0;

    public void Mine()
    {
        mineCount++;
        Inventory.instance.IncreaseStoneAmount(mineRewardAmount);
        if (mineCount >= mineThreshold)
        {
            DestroyRock();
        }
    }

    public void Destroy()
    {
        Inventory.instance.IncreaseStoneAmount(destroyRewardAmount);
        DestroyRock();
    }

    void DestroyRock()
    {
        Destroy(gameObject);
    }
}
