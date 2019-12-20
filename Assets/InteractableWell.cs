using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WellController))]
public class InteractableWell : Interactable
{
    public GameObject actionMenu;

    WellController wellController;
    Transform uiCanvas;
    Transform uiPanel;

    Button[] menuButtons;

    InteractionType interactionType = InteractionType.None;

    private void Start()
    {
        wellController = GetComponent<WellController>();
        uiCanvas = transform.GetChild(0);
        uiPanel = uiCanvas.GetChild(0);

        menuButtons = uiPanel.GetComponentsInChildren<Button>();

        menuButtons[0].onClick.AddListener(Drink);

    }

    public override void Interact()
    {
        base.Interact();

        switch (interactionType)
        {
            case InteractionType.Drink:
                wellController.Drink();
                break;

        }
    }

    public override void Update()
    {
        base.Update();
        actionMenu.SetActive(isFocused && !interacted);
    }

    public void Drink()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.Drink;
    }

}
