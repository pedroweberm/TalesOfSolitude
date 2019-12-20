using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimalController))]
public class InteractableAnimal : Interactable
{
    public GameObject actionMenu;

    AnimalController animalController;
    Transform uiCanvas;
    Transform uiPanel;

    Button[] menuButtons;

    InteractionType interactionType = InteractionType.None;

    private void Start()
    {
        animalController = GetComponent<AnimalController>();

        uiCanvas = transform.GetChild(0);
        uiPanel = uiCanvas.GetChild(0);

        menuButtons = uiPanel.GetComponentsInChildren<Button>();

        menuButtons[0].onClick.AddListener(Feed);
        menuButtons[1].onClick.AddListener(Attack);
        menuButtons[2].onClick.AddListener(Pet);
    }

    public override void Interact()
    {
        base.Interact();

        switch (interactionType)
        {
            case InteractionType.Feed:
                animalController.Feed();
                break;
            case InteractionType.Attack:
                animalController.Attack();
                break;
            case InteractionType.Pet:
                animalController.Pet();
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        actionMenu.SetActive(isFocused && !interacted);
    }

    public void Feed()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.Feed;
    }

    public void Attack()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.Attack;
    }

    public void Pet()
    {
        PlayerController.instance.FollowFocus(this);
        interactionType = InteractionType.Pet;
    }
}