using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing = 15f;
    private bool isFixed = false;

    Vector3 offset;

    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y + 58, target.position.z - 63);

        offset = transform.position - target.position;

        transform.LookAt(target);
    }

    void FixedUpdate()
    {
        if (!isFixed)
        {
            Vector3 targetCamPos = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.LookAt(target);
        }
        
    }

    public void MoveToFixed(Vector3 moveTo, Vector3 focusPoint)
    {

        if (!isFixed)
        {
            transform.position = new Vector3(moveTo.x, moveTo.y, moveTo.z);

            transform.LookAt(focusPoint);

            isFixed = true;
        }
    }

    public void ReturnFromFixed()
    {
        
        isFixed = false;

    }

}
