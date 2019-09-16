using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Rigidbody playerRigidBody;
    float camRayLenght = 100f;
    int floorMask;
    NavMeshAgent playerAgent;
    Animator m_Animator;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;


    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
        playerAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        playerAgent.updatePosition = false;
    }

    public void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(clickRay, out hitInfo, camRayLenght, floorMask))
            {
                playerAgent.SetDestination(hitInfo.point);
            }
        }

        Vector3 worldDeltaPosition = playerAgent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        m_TurnAmount = Mathf.Atan2(velocity.x, velocity.y);
        m_ForwardAmount = velocity.y;

        Debug.Log(velocity);

        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", false);
        m_Animator.SetBool("OnGround", true);
        m_Animator.SetFloat("Jump", 0);
        m_Animator.SetFloat("JumpLeg", 0);
    }

    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = playerAgent.nextPosition;
    }

    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();

        movement = move * speed * Time.deltaTime;

        Debug.Log(movement);

        playerRigidBody.MovePosition(transform.position + movement);
    }

    public void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLenght, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }
}
