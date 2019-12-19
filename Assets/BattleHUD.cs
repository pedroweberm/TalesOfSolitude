using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text lvlText;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        lvlText.text = unit.unitLvl.ToString();

        hpSlider.maxValue = unit.maxHp;
        hpSlider.value = unit.currentHp;
        
    }

    public void SetHP(float hp)
    {
        hpSlider.value = hp;
    }
}
