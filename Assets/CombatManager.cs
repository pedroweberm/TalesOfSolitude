using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public GameObject animal = null;
    public GameObject mainCamera;
    public Transform cameraFocus;
    public Transform combatPlayer;
    public bool isCombat = false;
    
    private Vector3 spawnPoint = new Vector3(10142.21f, 0.0f, 9376.951f);
    private Quaternion rot = new Quaternion(0.0f, -136.611f, 0.0f, 1.0f);

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

    public void EnterCombat(GameObject targetAnimal)
    {
        animal = targetAnimal;
        isCombat = true;

        mainCamera.GetComponent<CameraController>().MoveToFixed(new Vector3(10125.5f, 8.9f, 9337.0f), cameraFocus.position);

        //Instantiate(animal, spawnPoint, rot);
        //animal.transform.LookAt(combatPlayer);

    }

    public void LeaveCombat()
    {

        mainCamera.GetComponent<CameraController>().ReturnFromFixed();

        isCombat = false;

        Destroy(animal);
    }
}
