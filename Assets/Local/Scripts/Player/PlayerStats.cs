using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Unit
{
    public static PlayerStats instance;

    public float dmgMult;
    public float healMult;

    public float playerCurrentHunger;
    public float playerCurrentThirst;

    public float playerStartingHunger;
    public float playerStartingThirst;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
