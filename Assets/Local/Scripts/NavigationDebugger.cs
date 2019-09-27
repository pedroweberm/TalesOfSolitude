using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationDebugger : MonoBehaviour
{

    public NavMeshAgent playerAgent;

    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAgent.hasPath)
        {
            line.positionCount = playerAgent.path.corners.Length;
            line.SetPositions(playerAgent.path.corners);
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
    }
}
