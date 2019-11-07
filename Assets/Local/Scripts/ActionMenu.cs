using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ActionMenuItem
{
    // this class - just a box to some data

    public string text;             // text to display on button
    public Button button;           // sample button prefab
    public Action<Image> action;    // delegate to method that needs to be executed when button is clicked

    public ActionMenuItem(string text, Button button, Action<Image> action)
    {
        this.text = text;
        this.button = button;
        this.action = action;
    }
}

public class ActionMenu : MonoBehaviour
{
    public Image contentPanel;              // content panel prefab
    public Canvas canvas;                   // link to main canvas, where will be Action Menu

    private static ActionMenu instance;    // some kind of singleton here

    public static ActionMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(ActionMenu)) as ActionMenu;
                if (instance == null)
                {
                    instance = new ActionMenu();
                }
            }
            return instance;
        }
    }

    public void CreateActionMenu(List<ActionMenuItem> items, Vector2 position)
    {
        // here we are creating and displaying Action Menu

        Image panel = Instantiate(contentPanel, new Vector3(position.x, position.y, 0), Quaternion.identity) as Image;
        panel.transform.SetParent(canvas.transform);
        panel.transform.SetAsLastSibling();
        panel.rectTransform.anchoredPosition = position;

        foreach (var item in items)
        {
            ActionMenuItem tempReference = item;
            Button button = Instantiate(item.button) as Button;
            Text buttonText = button.GetComponentInChildren(typeof(Text)) as Text;
            buttonText.text = item.text;
            button.onClick.AddListener(delegate { tempReference.action(panel); });
            button.transform.SetParent(panel.transform);
        }
    }
}