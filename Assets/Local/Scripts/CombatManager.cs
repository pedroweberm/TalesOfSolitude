using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PolyPerfect;
using TMPro;
public enum BattleState { START, PLAYER, ENEMY, WON, LOST, PLAYERCHOSE, RUN }
public class CombatManager : MonoBehaviour
{
    public BattleState state;

    public TMP_Text dialogueText;
    public TMP_Text playerHarmonyText;
    public TMP_Text playerNameText;

    public static CombatManager instance;
    public GameObject UIPanel;
    public GameObject animal = null;
    public GameObject combatAnimal = null;
    public GameObject mainCamera;
    public Transform cameraFocus;
    public Transform combatPlayer;
    public bool isCombat = false;
    public GameObject combatCanvas;

    Unit enemy;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    
    private Vector3 spawnPoint = new Vector3(10142.21f, 0.0f, 9376.951f);
    private Quaternion rot = new Quaternion(0.0f, -136.611f, 0.0f, 1.0f);
    private float previousWanderZone;

    private void Awake()
    {
        combatCanvas.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public IEnumerator EnterCombat(GameObject targetAnimal)
    {
        //Debug.Log("entre combat");
        UIPanel.SetActive(false);
        animal = targetAnimal;
        isCombat = true;

        mainCamera.GetComponent<CameraController>().MoveToFixed(new Vector3(10125.5f, 8.9f, 9337.0f), cameraFocus.position);

        combatAnimal = Instantiate(animal, spawnPoint, rot);
        animal.transform.LookAt(combatPlayer);

        previousWanderZone = animal.GetComponent<WanderScript>().wanderZone;
        animal.GetComponent<WanderScript>().enabled = false;

        enemy = animal.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemy.unitName + " approaches...";

        //playerHarmonyText.text = (PlayerStats.instance.unitLvl.ToString());
        //playerNameText.text = (PlayerStats.instance.unitName.ToString());

        combatCanvas.SetActive(true);

        playerHUD.SetHUD(PlayerStats.instance);
        enemyHUD.SetHUD(enemy);

        yield return new WaitForSeconds(2f);

        SetMult();

        state = BattleState.PLAYER;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
        return;

    }

    void SetMult()
    {

        if (PlayerStats.instance.unitLvl == 0)
        {
            PlayerStats.instance.dmgMult = 3f;
            PlayerStats.instance.healMult = 0.25f;
        }
        else if (PlayerStats.instance.unitLvl < 10)
        {
            PlayerStats.instance.dmgMult = 2f;
            PlayerStats.instance.healMult = 0.5f;

        }
        else if (PlayerStats.instance.unitLvl < 25)
        {
            PlayerStats.instance.dmgMult = 1.5f;
            PlayerStats.instance.healMult = 0.75f;

        }
        else if (PlayerStats.instance.unitLvl < 40)
        {
            PlayerStats.instance.dmgMult = 1.2f;
            PlayerStats.instance.healMult = 1.0f;
        }
        else if (PlayerStats.instance.unitLvl == 100)
        {
            PlayerStats.instance.healMult = 3f;
            PlayerStats.instance.dmgMult = 0.5f;
        }
        else if (PlayerStats.instance.unitLvl > 90)
        {
            PlayerStats.instance.healMult = 2f;
            PlayerStats.instance.dmgMult = 0.75f;
        }
        else if (PlayerStats.instance.unitLvl > 75)
        {
            PlayerStats.instance.healMult = 1.5f;
            PlayerStats.instance.dmgMult = 1.0f;
        }
        else if (PlayerStats.instance.unitLvl > 60)
        {
            PlayerStats.instance.healMult = 1.2f;
            PlayerStats.instance.dmgMult = 1.0f;
        }
        else
        {
            PlayerStats.instance.healMult = 1f;
            PlayerStats.instance.dmgMult = 1f;
        }

    }

    public void OnRunButton()
    {
        if (state != BattleState.PLAYER || state == BattleState.PLAYERCHOSE)
        {
            return;
        }

        state = BattleState.RUN;
        StartCoroutine(PlayerRun());
    }
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYER || state == BattleState.PLAYERCHOSE)
        {
            return;
        }

        state = BattleState.PLAYERCHOSE;
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYER || state == BattleState.PLAYERCHOSE)
        {
            return;
        }

        state = BattleState.PLAYERCHOSE;
        StartCoroutine(PlayerHeal());
    }
    IEnumerator PlayerRun()
    {
        dialogueText.text = "You ran away!";

        yield return new WaitForSeconds(2f);

        Destroy(combatAnimal);
        mainCamera.GetComponent<CameraController>().ReturnFromFixed();

        isCombat = false;

        UIPanel.SetActive(true);
        combatCanvas.SetActive(false);
    }
    IEnumerator PlayerAttack()
    {
        //if (PlayerStats.instance.unitLvl == 0)
        //{
        //    PlayerStats.instance.dmgMult = 3f;
        //    PlayerStats.instance.healMult = 0.25f;
        //}
        //else if (PlayerStats.instance.unitLvl < 10)
        //{
        //    PlayerStats.instance.dmgMult = 2f;
        //    PlayerStats.instance.healMult = 0.5f;

        //}
        //else if (PlayerStats.instance.unitLvl < 25)
        //{
        //    PlayerStats.instance.dmgMult = 1.5f;
        //    PlayerStats.instance.healMult = 0.75f;

        //}
        //else if (PlayerStats.instance.unitLvl < 40)
        //{
        //    PlayerStats.instance.dmgMult = 1.2f;
        //}
        //else
        //{
        //    PlayerStats.instance.dmgMult = 1f;
        //}

        int i = 0;
        float damage;

        for (i = 0; i < 25; i++)
        {
            damage = Random.Range(1, 10);
            dialogueText.text = "Your damage is...  " + damage.ToString();
            yield return new WaitForSeconds(0.1f);
            PlayerStats.instance.dmg = damage;
        }

        yield return new WaitForSeconds(1.5f);

        bool isDead = enemy.TakeDamage(PlayerStats.instance.dmg * PlayerStats.instance.dmgMult);

        enemyHUD.SetHP(enemy.currentHp);

        dialogueText.text = (PlayerStats.instance.dmg * PlayerStats.instance.dmgMult).ToString("F2") + " damage dealt!";

        yield return new WaitForSeconds(2f);


        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(LeaveCombat());
        }
        else
        {
            state = BattleState.ENEMY;
            StartCoroutine(EnemyTurn());
        }


    }

    IEnumerator PlayerHeal()
    {
        //if (PlayerStats.instance.unitLvl == 100)
        //{
        //    PlayerStats.instance.healMult = 3f;
        //    PlayerStats.instance.dmgMult = 0.5f;

        //}
        //else if (PlayerStats.instance.unitLvl > 90)
        //{
        //    PlayerStats.instance.healMult = 2f;
        //    PlayerStats.instance.dmgMult = 0.75f;

        //}
        //else if (PlayerStats.instance.unitLvl > 75)
        //{
        //    PlayerStats.instance.healMult = 1.5f;
        //}
        //else if (PlayerStats.instance.unitLvl > 60)
        //{
        //    PlayerStats.instance.healMult = 1.2f;
        //}
        //else
        //{
        //    PlayerStats.instance.healMult = 1f;
        //}

        float heal;
        int i;

        for (i = 0; i < 25; i++)
        {
            heal = Random.Range(3, 6);
            dialogueText.text = "You heal for...  " + heal.ToString();
            yield return new WaitForSeconds(0.1f);
            PlayerStats.instance.heal = heal;
        }

        yield return new WaitForSeconds(1.5f);

        PlayerStats.instance.Heal(PlayerStats.instance.heal * PlayerStats.instance.healMult);

        playerHUD.SetHP(PlayerStats.instance.currentHp);

        dialogueText.text = (PlayerStats.instance.heal * PlayerStats.instance.healMult).ToString("F2") + " HP restored!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMY;
        StartCoroutine(EnemyTurn());


    }


    IEnumerator EnemyTurn()
    {
        float chance = Random.Range(0.0f, 1.0f);

        if (chance < 0.2f)
        {
            enemy.Heal(enemy.heal);

            dialogueText.text = "Enemy healed " + enemy.heal.ToString() + " HP.";

            enemyHUD.SetHP(enemy.currentHp);

            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYER;
            PlayerTurn();

        }
        else
        {
            bool isDead = PlayerStats.instance.TakeDamage(enemy.dmg);
            dialogueText.text = "Enemy dealt " + enemy.dmg.ToString("F2") + " damage.";
            playerHUD.SetHP(PlayerStats.instance.currentHp);

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.LOST;
                StartCoroutine(LeaveCombat());
            }
            else
            {
                state = BattleState.PLAYER;
                PlayerTurn();

            }
        }
    }


    IEnumerator LeaveCombat()
    {

        if (state == BattleState.WON)
        {
            dialogueText.text = "You won!";

            yield return new WaitForSeconds(2f);

            animal.GetComponent<AnimalController>().Kill();
            int i = 0;
            bool fullInv;

            dialogueText.text = "Enemy dropped " + enemy.amountDropped.ToString() + " " + enemy.drop.item_name + ".";

            yield return new WaitForSeconds(2f);

            for (i = 0; i < enemy.amountDropped; i++)
            {
                fullInv = Inventory.instance.Add(enemy.drop);
                
                if (fullInv)
                {
                    dialogueText.text = "Added " + (i+1).ToString() + " to inventory.";

                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    dialogueText.text = "Your inventory is full.";

                    break;
                }
            }

            yield return new WaitForSeconds(2f);

        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You died.";

            yield return new WaitForSeconds(2f);

            Application.Quit();
        }

        PlayerStats.instance.ChangeSync(-1);

        Destroy(combatAnimal);
        mainCamera.GetComponent<CameraController>().ReturnFromFixed();

        isCombat = false;

        UIPanel.SetActive(true);
        
        combatCanvas.SetActive(false);
    }
}
