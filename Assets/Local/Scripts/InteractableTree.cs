using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTree : Interactable
{
    public GameObject actionMenu;

    public override void Interact()
    {
        base.Interact();
        actionMenu.SetActive(true);
    }
}
