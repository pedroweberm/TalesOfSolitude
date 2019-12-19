using UnityEngine;
using UnityEngine.UI;

public class PlayerNecessities : MonoBehaviour
{
    #region SINGLETON
    public static PlayerNecessities instance;

    void Awake ()
    {
        if (instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public float playerInitialHealth = PlayerStats.instance.maxHp;
    //public int playerMinHealth = 100;
    public float playerInitialHunger = PlayerStats.instance.playerStartingHunger;
    public float playerInitialThirst = PlayerStats.instance.playerStartingThirst;
    public float playerInitialSynchrony = 0;
    public int starvingDamage = 1;
    public int thirstyDamage = 3;

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
        playerHealth = PlayerStats.instance.currentHp;
        playerMaxHealth = PlayerStats.instance.maxHp;

        playerHunger = PlayerStats.instance.playerCurrentHunger;
        playerThirst = PlayerStats.instance.playerCurrentThirst;
        playerSynchrony = PlayerStats.instance.unitLvl;
        goodSynchrony = playerSynchrony >= 0;
        badSynchrony = playerSynchrony < 0;

        currentTick = TickManager.global.tickCount;

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        currentTick = TickManager.global.tickCount;

        UpdateHunger(-1);
        UpdateThirst(-2);
        CheckNecessities();

        playerHealth = PlayerStats.instance.currentHp;
        playerHunger = PlayerStats.instance.playerCurrentHunger;
        playerThirst = PlayerStats.instance.playerCurrentThirst;
        playerSynchrony = PlayerStats.instance.unitLvl;
        goodSynchrony = playerSynchrony >= 0;
        badSynchrony = playerSynchrony < 0;

        UpdateUI();
    }

    public void UpdateHealth(float amount)
    {
        PlayerStats.instance.currentHp += amount;
        playerHealth = PlayerStats.instance.currentHp;

        if (playerHealth <= 0)
        {
            Die();
        }
        if (playerHealth >= playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
            PlayerStats.instance.currentHp = playerMaxHealth;
        }
    }

    //bool updatemaxhealth(int amount)
    //{
    //    if (playermaxhealth + amount >= playerminhealth)
    //    {
    //        playermaxhealth += amount;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}

    public void UpdateHunger(int amount)
    {
        if (currentTick > lastHungerTick)
        {
            lastHungerTick = currentTick;
            PlayerStats.instance.playerCurrentHunger += amount;
            playerHunger = PlayerStats.instance.playerCurrentHunger;
        }
        if (playerHunger <= 0)
        {
            playerStarving = true;
            PlayerStats.instance.playerCurrentHunger = 0;
            playerHunger = PlayerStats.instance.playerCurrentHunger;
        }
    }

    public void UpdateThirst(int amount)
    {
        if (currentTick > lastThirstTick)
        {
            lastThirstTick = currentTick;
            PlayerStats.instance.playerCurrentThirst += amount;
            playerThirst = PlayerStats.instance.playerCurrentThirst;

        }
        if (playerThirst <= 0)
        {
            playerThirsty = true;
            PlayerStats.instance.playerCurrentThirst = 0;
            playerHunger = PlayerStats.instance.playerCurrentThirst;
        }
    }

    public void UpdateSynchrony(int amount)
    {
        PlayerStats.instance.unitLvl += amount;
        playerSynchrony += PlayerStats.instance.unitLvl;

        if (playerSynchrony < 0)
        {
            PlayerStats.instance.unitLvl = 0;
            playerSynchrony += PlayerStats.instance.unitLvl;
        }
        else if (playerSynchrony > 100)
        {
            PlayerStats.instance.unitLvl = 100;
            playerSynchrony += PlayerStats.instance.unitLvl;
        }
        goodSynchrony = playerSynchrony >= 50f;
        badSynchrony = playerSynchrony < 50f;
    }

    public void CheckNecessities()
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
            goodIcon.SetActive(false);
            badIcon.SetActive(true);
        }
    }

    void Die()
    {
        Application.Quit();
        Debug.Log("Dead.");
    }
}
