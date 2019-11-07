using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNecessities : MonoBehaviour
{
    public int playerInitialHealth = 1000;
    public int playerMinHealth = 100;
    public int playerInitialHunger = 100;
    public int playerInitialThirst = 100;
    public int starvingDamage = 10;
    public int thirstyDamage = 50;

    private int playerHealth;
    private int playerMaxHealth;
    private int playerHunger;
    private int playerThirst;

    private bool playerThirsty = false;
    private bool playerStarving = false;

    private int lastHungerTick = 0;
    private int lastThirstTick = 0;
    private int lastDamageTick = 0;

    private int currentTick = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerInitialHealth;
        playerMaxHealth = playerInitialHealth;

        playerHunger = playerInitialHunger;
        playerThirst = playerInitialThirst;

        currentTick = TickManager.global.tickCount;
    }

    // Update is called once per frame
    void Update()
    {
        currentTick = TickManager.global.tickCount;

        UpdateHunger(0);
        UpdateThirst(0);

        CheckNecessities();
    }

    void UpdateHealth(int amount)
    {
        playerHealth += amount;

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    bool UpdateMaxHealth(int amount)
    {
        if (playerMaxHealth + amount >= playerMinHealth)
        {
            playerMaxHealth += amount;
            return true;
        }
        else
        {
            return false;
        }

    }

    void UpdateHunger(int amount)
    {
        playerHunger += amount;

        if (currentTick > lastHungerTick)
        {
            lastHungerTick = currentTick;
            playerHunger -= 1;
        }
        if (playerHunger <= 0)
        {
            playerStarving = true;
        }
    }

    void UpdateThirst(int amount)
    {
        playerThirst += amount;

        if (currentTick > lastThirstTick)
        {
            lastThirstTick = currentTick;
            playerThirst -= 1;
        }
        if (playerThirst <= 0)
        {
            playerThirsty = true;
        }
    }

    void CheckNecessities()
    {
        if (currentTick > lastDamageTick)
        {
            lastDamageTick = currentTick;
            if (playerStarving)
            {
                UpdateHealth(-starvingDamage);
            }
            if (playerThirsty)
            {
                UpdateHealth(-thirstyDamage);
            }
        }
    }

    void Die()
    {
        Debug.Log("Dead");
    }
}
