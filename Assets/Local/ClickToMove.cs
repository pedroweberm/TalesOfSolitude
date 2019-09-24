using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private Animator playerAnimator;
    private NavMeshAgent playerAgent;

    private bool playerRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAgent = GetComponent<NavMeshAgent>();

        playerAgent.updateRotation = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(clickRay, out hitInfo, 100))
            {
                playerAgent.SetDestination(hitInfo.point);
            }
        }

        
        if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
        {         
            playerRunning = false;    
        }
        
        else
        {
            playerRunning = true;
        }

        playerAnimator.SetBool("running", playerRunning);
    }

    private void LateUpdate()
    {
        if (playerAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(playerAgent.velocity.normalized);
        }
    }
}
