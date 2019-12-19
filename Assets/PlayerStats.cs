using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Unit
{
    public static PlayerStats instance;

    public float dmgMult;
    public float healMult;

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
