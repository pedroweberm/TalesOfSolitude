using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableRock : Interactable
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

        menuButtons[0].onClick.AddListener(Mine);
        menuButtons[1].onClick.AddListener(Destroy);
    }

    public override void Interact()
    {
        base.Interact();

        switch (interactionType)
        {
            case InteractionType.Mine:
                Debug.Log("Mining");
                break;
            case InteractionType.Destroy:
                Debug.Log("Destroying");
                break;
        }
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