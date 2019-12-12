using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNecessities : MonoBehaviour
{
    public int playerInitialHealth = 1000;
    public int playerMinHealth = 100;
    public float playerInitialHunger = 100;
    public float playerInitialThirst = 100;
    public float playerInitialSynchrony = 100;
    public int starvingDamage = 10;
    public int thirstyDamage = 50;

    private float playerHealth;
    private float playerMaxHealth;
    private float playerHunger;
    private float playerThirst;
    private float playerSynchrony;

    private bool playerThirsty = false;
    private bool playerStarving = false;

    private bool goodSynchrony = false;
    private bool badSynchrony = false;

    private int lastHungerTick = 0;
    private int lastThirstTick = 0;
    private int lastDamageTick = 0;

    private int currentTick = 0;

    public Image healthBar;
    public Image hungerBar;
    public Image thirstBar;

    public Image goodSynchronyBar;
    public GameObject goodIcon;
    public Image badSynchronyBar;
    public GameObject badIcon;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerInitialHealth;
        playerMaxHealth = playerInitialHealth;

        playerHunger = playerInitialHunger;
        playerThirst = playerInitialThirst;
        playerSynchrony = playerInitialSynchrony;
        goodSynchrony = playerSynchrony >= 50;
        badSynchrony = playerSynchrony < 50;

        currentTick = TickManager.global.tickCount;

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        currentTick = TickManager.global.tickCount;

        UpdateHunger(0);
        UpdateThirst(0);

        CheckNecessities();
        UpdateUI();
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
            UpdateSynchrony(-10);
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

    void UpdateSynchrony(float amount)
    {
        playerSynchrony += amount;
        goodSynchrony = playerSynchrony >= 50f;
        badSynchrony = playerSynchrony < 50f;
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

    void UpdateUI()
    {
        thirstBar.fillAmount = playerThirst / playerInitialThirst;
        hungerBar.fillAmount = playerHunger / playerInitialHunger;
        healthBar.fillAmount = playerHealth / playerMaxHealth;

        if (goodSynchrony)
        {
            goodSynchronyBar.fillAmount = playerSynchrony / playerInitialSynchrony;
            badSynchronyBar.fillAmount = 0f;
            goodIcon.SetActive(true);
            badIcon.SetActive(false);
        }
        else
        {
            badSynchronyBar.fillAmount = playerSynchrony / playerInitialSynchrony;
            goodSynchronyBar.fillAmount = 0f;
            badIcon.SetActive(true);
            goodIcon.SetActive(false);
        }
    }

    void Die()
    {
        Debug.Log("Dead");
    }
}
