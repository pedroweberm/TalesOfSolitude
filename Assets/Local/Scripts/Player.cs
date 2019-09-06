using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Rigidbody playerRigidBody;
    float camRayLenght = 100f;
    int floorMask;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
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
