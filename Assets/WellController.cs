using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellController : MonoBehaviour
{
    public float maxWater;
    private float currentWater;
    public int refillRate = 1;
    public float refillAmount;
    public float drinkAmount;

    private void Start()
    {
        InvokeRepeating("RefillWell", TickManager.global.tickDuration * refillRate, TickManager.global.tickDuration * refillRate);

    }
    private void RefillWell()
    {
        if (currentWater + refillAmount > maxWater)
        {
            currentWater = maxWater;
        }
        else
        {
            currentWater += refillAmount;
        }
        return;
    }

    public void Drink()
    {
        if (PlayerStats.instance.playerCurrentThirst + drinkAmount > PlayerStats.instance.playerStartingThirst)
        {
            PlayerStats.instance.playerCurrentThirst = PlayerStats.instance.playerStartingThirst;
        }
        else
        {
            PlayerStats.instance.playerCurrentThirst += drinkAmount;
        }

        currentWater -= drinkAmount;
    }
}
