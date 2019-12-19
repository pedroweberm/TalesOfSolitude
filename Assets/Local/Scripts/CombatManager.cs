using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyPerfect;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public GameObject animal = null;
    public GameObject mainCamera;
    public Transform cameraFocus;
    public Transform combatPlayer;
    public bool isCombat = false;
    public GameObject combatCanvas;
    
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

    public void EnterCombat(GameObject targetAnimal)
    {
        Debug.Log("entre combat");
        animal = targetAnimal;
        isCombat = true;

        mainCamera.GetComponent<CameraController>().MoveToFixed(new Vector3(10125.5f, 8.9f, 9337.0f), cameraFocus.position);

        Instantiate(animal, spawnPoint, rot);
        animal.transform.LookAt(combatPlayer);

        previousWanderZone = animal.GetComponent<WanderScript>().wanderZone;
        animal.GetComponent<WanderScript>().enabled = false;

        combatCanvas.SetActive(true);
    }

    public void LeaveCombat()
    {
        mainCamera.GetComponent<CameraController>().ReturnFromFixed();

        isCombat = false;
        Destroy(animal);
        combatCanvas.SetActive(false);
    }
}
