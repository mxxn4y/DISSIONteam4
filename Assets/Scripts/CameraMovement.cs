using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform followObject;
    public Transform realCamera;

    public Vector3 dirNormalized;
    public Vector3 finalDir;

    private float rotX;
    private float rotY;
    public float min;
    public float max;
    public float finalDistance;
    public float speed = 30f;               // 속도
    public float sensitivity = 200f;        // 감도. 변경가능
    public float clampAngleTop = 70f;       // 제한
    public float clampAngleBottom = 40f;
    public float smoothness = 10f;


    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;
    }

    void Update()
    {
        rotX += -Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, 0f, clampAngleTop);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,
        followObject.position, speed * Time.deltaTime);

        // TransformPoint (local->world)
        finalDir = transform.TransformPoint(dirNormalized * max);

        // 인식
        RaycastHit hit;
        if(Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, min, max);
        } else
        {
            finalDistance = max;
        }

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition,
            dirNormalized * finalDistance, Time.deltaTime * smoothness);

    }

}
