using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TreeController))]
public class InteractableTree : Interactable
{
    public GameObject actionMenu;

    TreeController treeController;

    private void Start()
    {
        treeController = GetComponent<TreeController>();
    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void openMenu()
    {
        base.openMenu();
        //actionMenu.SetActive(true);
    }

    private void Update()
    {
        actionMenu.SetActive(isFocused);
    }

    public void SoftChop()
    {
        Debug.Log("soft");
        PlayerController.instance.FollowFocus(this);
    }
}
