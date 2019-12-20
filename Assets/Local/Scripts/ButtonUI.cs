using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonUI : MonoBehaviour
{
    public int index;

    private void Start() {
    }
    public void onClick ()
    {
        Inventory.instance.SelectItem(index);
    }
}

