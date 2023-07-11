using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform followObject;
    public float speed = 10f;    
    public float sensitivity = 200f;    // 감도. 변경o
    public float clampAngleTop = 70f;      // 제한
    public float clampAngleBottom = -10f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;
    }

    //Update is called once per frame
    void Update()
    {
        rotX += -Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotY += -Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, 0f, clampAngleTop);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,
        followObject.position, speed * Time.deltaTime);

        // TransformPoint (local->world)
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);


        // 장애물인식...도해야할듯..........

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition,
            dirNormalized * finalDistance, Time.deltaTime * smoothness);

    }

}
