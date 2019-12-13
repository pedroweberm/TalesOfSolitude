using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInteract : Interactable
{

    public override void Interact()
    {
        base.Interact();

        CombatManager.instance.EnterCombat(this.gameObject);

    }

}
