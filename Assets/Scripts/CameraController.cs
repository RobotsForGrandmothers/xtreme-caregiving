using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Transform elevator;

    private void Awake() {
        elevator = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private Vector3 velocityCameraFollow;
    public Vector3 cameraPosition = new Vector3(0, 0, -4);

    
    private void FixedUpdate() {
        //transform.rotation = Quaternion.Euler(new Vector3(angle, ourDrone.GetComponent<DroneMovement>().currentYRotation, 0));
        LookAround();
    }

    public float smoothTime = 0.5f;
    void ReturnToPlayer() {
        transform.position = Vector3.SmoothDamp(transform.position, elevator.transform.TransformPoint(cameraPosition), ref velocityCameraFollow, smoothTime);
    }


    private float velocityCameraAngle;
    private float mouseMovementx = 0f;
    private float mouseMovementy = 0f;
    public float holdTime = 1f;
    void LookAround() {
        float mouseXInput = Mathf.Abs(Input.GetAxis("Mouse X"));
        float mouseYInput = Mathf.Abs(Input.GetAxis("Mouse Y"));
        if (mouseXInput > mouseMovementx) {
            mouseMovementx = Input.GetAxis("Mouse X");
        }
        if (mouseYInput > mouseMovementy) {
            mouseMovementy = Input.GetAxis("Mouse Y");
        }
        Vector3 pos = transform.position + (Vector3.up * Input.GetAxis("Mouse Y")) + transform.position + (Vector3.right * Input.GetAxis("Mouse X"));
        gameObject.transform.position = pos;
        //angle = Mathf.SmoothDamp(angle, desiredAngle, ref velocityCameraAngle, 0.25f);

    }

    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue) {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        return (NewValue);
    }
}
