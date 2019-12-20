using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableCampfire : Interactable
{
    public GameObject actionMenu;

    Transform uiCanvas;
    Transform uiPanel;

    Button[] menuButtons;

    InteractionType interactionType = InteractionType.None;

    private void Start()
    {
        uiCanvas = transform.GetChild(0);
        uiPanel = uiCanvas.GetChild(0);

        menuButtons = uiPanel.GetComponentsInChildren<Button>();

        menuButtons[0].onClick.AddListener(Cook);

    }

    public override void Interact()
    {
        base.Interact();

        switch (interactionType)
        {
            case InteractionType.Cook:
                Inventory.instance.selectedItem.Cook();
                break;

        }
    }

    public override void Update()
    {
        base.Update();
        actionMenu.SetActive(isFocused && !interacted && Inventory.instance.selectedItem.isFood);
    }

    public void Cook()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.Cook;
    }

}
