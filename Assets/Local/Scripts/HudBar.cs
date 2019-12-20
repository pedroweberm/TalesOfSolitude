using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HudBar : MonoBehaviour
{

    public TMP_Text woodAmount;
    public TMP_Text stoneAmount;


    void Update()
    {
        woodAmount.text = Inventory.instance.wood_amount.ToString();
        stoneAmount.text = Inventory.instance.stone_amount.ToString();
    }
}
