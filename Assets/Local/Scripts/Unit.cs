using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public float maxHp;
    public float currentHp;
    public int unitLvl;

    public float dmg;
    public float heal;

    public bool TakeDamage(float dmg)
    {
        currentHp -= dmg;

        if (currentHp <= 0)
        {
            return true;

        }
        else
        {
            return false;
        }

    }

    public void Heal(float heal)
    {
        currentHp += heal;

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

    }
}
