using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        private int floorMask;
        bool mooving = false;
        float camRayLenght = 100f;
        NavMeshAgent playerAgent;
        bool isIdle = true;


        private void Start()
        {
            floorMask = LayerMask.GetMask("Floor");
            playerAgent = GetComponent<NavMeshAgent>();
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
            playerAgent.updatePosition = true;
            playerAgent.updateRotation = false;
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            } 
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isIdle = false;
                Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(clickRay, out hitInfo, camRayLenght, floorMask))
                {
                    playerAgent.SetDestination(hitInfo.point);
                }
            }

            if (!playerAgent.pathPending)
            {
                if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
                {
                    if (!playerAgent.hasPath || playerAgent.velocity.sqrMagnitude == 0f)
                    {
                        isIdle = true;
                        Debug.Log("Chegou ja");
                    }
                }
            }

            if (isIdle)
            {
                m_Character.Move(Vector3.zero, false, false);
            }
            else
            {
                // pass all parameters to the character control script
                m_Character.Move(playerAgent.desiredVelocity, false, m_Jump);
            }
        }
    }
}
