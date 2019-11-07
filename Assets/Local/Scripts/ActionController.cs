using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{

    public Button sampleButton;                         // sample button prefab
    private List<ActionMenuItem> ActionMenuItems;     // list of items in menu

    void Awake()
    {
        // Here we are creating and populating our future Action Menu.
        // I do it in Awake once, but as you can see, 
        // it can be edited at runtime anywhere and anytime.

        ActionMenuItems = new List<ActionMenuItem>();
        Action<Image> equip = new Action<Image>(EquipAction);
        Action<Image> use = new Action<Image>(UseAction);
        Action<Image> drop = new Action<Image>(DropAction);

        ActionMenuItems.Add(new ActionMenuItem("Equip", sampleButton, equip));
        ActionMenuItems.Add(new ActionMenuItem("Use", sampleButton, use));
        ActionMenuItems.Add(new ActionMenuItem("Drop", sampleButton, drop));
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            ActionMenu.Instance.CreateActionMenu(ActionMenuItems, new Vector2(pos.x, pos.y));
        }

    }

    void EquipAction(Image ActionPanel)
    {
        Debug.Log("Equipped");
        Destroy(ActionPanel.gameObject);
    }

    void UseAction(Image ActionPanel)
    {
        Debug.Log("Used");
        Destroy(ActionPanel.gameObject);
    }

    void DropAction(Image ActionPanel)
    {
        Debug.Log("Dropped");
        Destroy(ActionPanel.gameObject);
    }
}