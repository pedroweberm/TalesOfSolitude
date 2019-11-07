using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    #region Singleton
    public static TickManager global;

    void Awake()
    {
        if (global != null)
        {
            Debug.LogWarning("More than 1 TickManager");
            return;
        }
        global = this;
    }
    #endregion

    public float tickDuration = 5.0f;

    [System.NonSerialized]
    public int tickCount = 0;

    private void Start()
    {
        InvokeRepeating("IncrementTicks", tickDuration, tickDuration);
    }

    void IncrementTicks()
    {
        tickCount++;
    }
}
