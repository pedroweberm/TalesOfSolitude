﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactRadius = 3.0f;
    public Transform interactionTransform;

    bool isFocused = false;
    Transform player;

    bool interacted = false;

    public virtual void Interact()
    {
        // Scripts de outros objetos vao sobrescrever este metodo
        Debug.Log("Interacting with " + transform.name);
    }

    private void Awake()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
    }

    private void Update()
    {
        if (isFocused && !interacted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= interactRadius)
            {
                Interact();
                interacted = true;
            }
        }
    }

    public void onFocused (Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        interacted = false;
    }

    public void onDefocused ()
    {
        isFocused = false;
        player = null;
        interacted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionTransform.position, interactRadius);
    }
}
