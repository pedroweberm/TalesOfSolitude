using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Tree", menuName = "Resource/Tree")]
public class Tree : Resource
{
    public int treeHardChopTime = 5;
    public int treeSoftChopTime = 2;

    private List<ActionMenuItem> ActionMenuItems;
    public Button sampleButton;

    void createMenu()
    {

        ActionMenuItems = new List<ActionMenuItem>();

        Action<Image> softChop = new Action<Image>(SoftChopAction);
        Action<Image> hardChop = new Action<Image>(HardChopAction);

        ActionMenuItems.Add(new ActionMenuItem("Soft Chop", sampleButton, softChop));
        ActionMenuItems.Add(new ActionMenuItem("Hard Chop", sampleButton, hardChop));
    }

    void SoftChopAction(Image ActionPanel)
    {
        Debug.Log("Equipped");
        Destroy(ActionPanel.gameObject);
    }

    void HardChopAction(Image ActionPanel)
    {
        Debug.Log("Used");
        Destroy(ActionPanel.gameObject);
    }
}
