using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TreeController))]
public class InteractableRock : Interactable
{
    public GameObject actionMenu;

    RockController rockController;
    Transform uiCanvas;
    Transform uiPanel;

    Button[] menuButtons;

    InteractionType interactionType = InteractionType.None;

    private void Start()
    {
        rockController = GetComponent<RockController>();

        uiCanvas = transform.GetChild(0);
        uiPanel = uiCanvas.GetChild(0);

        menuButtons = uiPanel.GetComponentsInChildren<Button>();

        menuButtons[0].onClick.AddListener(Mine);
        menuButtons[1].onClick.AddListener(Destroy);
    }

    public override void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.Mine:
                rockController.Mine();
                break;
            case InteractionType.Destroy:
                rockController.Destroy();
                break;
        }

        base.Interact();
        interactionType = InteractionType.None;
    }

    public override void Update()
    {
        base.Update();
        actionMenu.SetActive(isFocused && !interacted);
    }

    public void Mine()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.Mine;
    }

    public void Destroy()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.Destroy;
    }
}