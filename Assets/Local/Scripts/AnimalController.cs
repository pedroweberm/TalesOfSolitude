using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public SpawnPoint home = null;
    
    public void Kill()
    {
        if (home != null)
        {
            home.OnKillAnimal();
        }
        else
        {
            Debug.LogWarning("This animal can't be killed because it doesn't have a home");
        }
    }
}
