using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;
    public Interactable currentFocus;

    PlayerMovement playerMovement;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Trying to create more than 1 player controller");
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            if (Inventory.instance.selectedItem.isFood)
                Inventory.instance.selectedItem.Eat();
        }
            if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(clickRay, out hitInfo, 300, groundLayer))
            {
                playerMovement.MoveToPoint(hitInfo.point);

                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(clickRay, out hitInfo, 300))
            {
                Interactable interactableHit = hitInfo.collider.GetComponent<Interactable>();
                if (interactableHit != null)
                {
                    SetFocus(interactableHit);
                }
            }
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != currentFocus)
        {
            if (currentFocus != null)
                currentFocus.onDefocused();

            currentFocus = newFocus;
            Debug.Log("New focus " + currentFocus.transform.name);
        }
        newFocus.onFocused(transform);
    }

    public void RemoveFocus()
    {
        if (currentFocus != null)
            currentFocus.onDefocused();

        currentFocus = null;
        playerMovement.StopFollowing();
    }

    public void FollowFocus(Interactable target)
    {
        Debug.Log("Follow " + target.transform.name + "current " + currentFocus.transform.name);
        if (currentFocus != null)
            if (target == currentFocus)
                playerMovement.FollowTarget(currentFocus);
            else
                Debug.Log("Trying to follow target that is not focused");
    }
}
