using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TreeController))]
public class InteractableTree : Interactable
{
    public GameObject actionMenu;

    TreeController treeController;
    Transform uiCanvas;
    Transform uiPanel;

    Button[] menuButtons;

    InteractionType interactionType = InteractionType.None;

    private void Start()
    {
        treeController = GetComponent<TreeController>();

        uiCanvas = transform.GetChild(0);
        uiPanel = uiCanvas.GetChild(0);

        menuButtons = uiPanel.GetComponentsInChildren<Button>();

        menuButtons[0].onClick.AddListener(SoftChop);
        menuButtons[1].onClick.AddListener(HardChop);
        menuButtons[2].onClick.AddListener(Water);
    }

    public override void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.SoftChop:
                treeController.SoftChop();
                base.Interact();
                break;
            case InteractionType.HardChop:
                treeController.HardChop();
                base.Interact();
                break;
            case InteractionType.Water:
                treeController.Water();
                base.Interact();
                break;
        }

        interactionType = InteractionType.None;
    }

    public override void Update()
    {
        base.Update();
        actionMenu.SetActive(isFocused && !interacted);
    }

    public void SoftChop()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.SoftChop;
    }

    public void HardChop()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.HardChop;
    }

    public void Water()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.Water;
    }
}
