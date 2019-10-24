using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{

    public int treeHardChopTime = 5;
    public int treeSoftChopTime = 2;

    public int treeHardChopReward = 15;
    public int treeSoftChopReward = 5;

    public float interactableRadius = 3.0f;

    public TickManager tickManager;

    bool isBeingChopped = false;
    int chopStartTick = 0;
    int timeToChop = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (isBeingChopped)
        {
            if (tickManager.tickCount - chopStartTick >= timeToChop)
            {
                DestroyTree();
            }
        }
    }

    public void ChopTree(bool isHardChop)
    {
        chopStartTick = tickManager.tickCount;

        if (isHardChop)
        {
            timeToChop = treeHardChopTime;
        }
        else
        {
            timeToChop = treeSoftChopTime;
        }

        isBeingChopped = true;
    }

    public void DestroyTree()
    {
        Destroy(this.gameObject);
    }
}
