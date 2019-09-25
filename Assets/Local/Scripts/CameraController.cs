using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(200, 65, target.position.z - 50);

        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
