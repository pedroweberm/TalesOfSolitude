using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing = 3f;
    public float zoomSpeed = 0.5f;
    public float minZoom = 0.75f;
    public float maxZoom = 1.25f;
    private float currentZoom = 1f;
    public Vector3 offset;



    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y + 58, target.position.z - 63);

        offset = transform.position - target.position;
    }

    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    void LateUpdate()
    {
        Vector3 targetCamPos = target.position + offset * currentZoom;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        CompensateForWalls(offset, ref targetCamPos);
        transform.LookAt(target.position + Vector3.up * 0.5f);

    }

    private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
        RaycastHit wallHit = new RaycastHit();

        if (Physics.Linecast(fromObject, toTarget, out wallHit))
        {
            toTarget = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);
        }
    }
}
