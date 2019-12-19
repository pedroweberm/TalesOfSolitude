using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent playerAgent;
    Animator playerAnimator;
    Transform followTarget;

    bool playerRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();

        playerAgent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerRunning = !(playerAgent.remainingDistance <= playerAgent.stoppingDistance);
        playerAnimator.SetBool("running", playerRunning);
        
        if (followTarget != null && followTarget.position != playerAgent.destination)
        {
            Debug.Log(followTarget.transform.name);
            playerAgent.SetDestination(followTarget.position);
            FaceTarget();
        }
    }

    private void LateUpdate()
    {
        if (playerAgent.velocity.sqrMagnitude > Mathf.Epsilon && playerRunning)
        {
            transform.rotation = Quaternion.LookRotation(playerAgent.velocity.normalized);
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        playerAgent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        playerAgent.stoppingDistance = newTarget.interactRadius * 0.8f;
        followTarget = newTarget.interactionTransform;
    }

    public void StopFollowing()
    {
        playerAgent.stoppingDistance = 1.5f;
        followTarget = null;
    }

    void FaceTarget()
    {
        Vector3 faceDirection = (followTarget.position - transform.position).normalized;
        Quaternion faceRotation = Quaternion.LookRotation(new Vector3(faceDirection.x, 0.0f, faceDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, faceRotation, Time.deltaTime * 5f);
    }
}
