using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{

    public float tickDuration = 5.0f;

    [System.NonSerialized]
    public int tickCount = 0;

    private float timeCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount >= tickDuration)
        {
            timeCount = 0;
            tickCount++;
            Debug.Log(tickCount);
        }
    }
}
