using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCanvasController : MonoBehaviour
{

    private void Update()
    {
        this.gameObject.SetActive(CombatManager.instance.isCombat);

        Debug.Log(CombatManager.instance.isCombat);
    }

}
