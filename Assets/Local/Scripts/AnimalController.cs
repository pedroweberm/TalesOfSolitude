using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public SpawnPoint home = null;

    public int feedReward = 5;
    public int feedCost = 10;

    public int petReward = 1;

    public void Feed()
    {
        PlayerStats.instance.ChangeSync(feedReward);
        PlayerStats.instance.playerCurrentHunger -= feedCost;
    }

    public void Attack()
    {
        StartCoroutine(CombatManager.instance.EnterCombat(this.gameObject));
    }

    public void Pet()
    {
        PlayerStats.instance.ChangeSync(petReward);
    }

    public void Kill()
    {
        if (home != null)
        {
            Destroy(gameObject);
            home.OnKillAnimal();
        }
        else
        {
            Debug.LogWarning("This animal can't be killed because it doesn't have a home");
        }
    }
}
