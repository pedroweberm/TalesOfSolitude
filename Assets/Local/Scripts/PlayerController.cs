using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    
    private Player m_Player;                  // A reference to the Player on the object
    private Vector3 m_Move;                   // the world-relative desired move direction, calculated from the camForward and user input.


    private void Awake()
    {
        m_Player = GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        m_Move.Set(h, 0f, v);

        m_Player.Turning();

        m_Player.Move(m_Move);
    }

    
}
