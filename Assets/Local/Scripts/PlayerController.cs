using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;
    public Interactable currentFocus;

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
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

    void SetFocus(Interactable newFocus)
    {
        currentFocus = newFocus;
        playerMovement.FollowTarget(newFocus);
    }

    void RemoveFocus()
    {
        currentFocus = null;
        playerMovement.StopFollowing();
    }
}
