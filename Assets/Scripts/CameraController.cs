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

    public float smoothTime = 0.5f;
    private void FixedUpdate() {
        
        //transform.rotation = Quaternion.Euler(new Vector3(angle, ourDrone.GetComponent<DroneMovement>().currentYRotation, 0));
        LookAround();
    }

    void ReturnToPlayer() {
        transform.position = Vector3.SmoothDamp(transform.position, elevator.transform.TransformPoint(cameraPosition) + Vector3.up * Input.GetAxis("Mouse Y"), ref velocityCameraFollow, smoothTime);
    }


    private float velocityCameraAngle;
    void LookAround() {
        float mouseXInput = Input.GetAxis("Mouse X");
        float mouseYInput = Input.GetAxis("Mouse Y");
        if (mouseXInput < 0) {
            //Code for action on mouse moving left
            print("Mouse moved left");
        }
        if (mouseXInput > 0) {
            //Code for action on mouse moving right
            print("Mouse moved right");
        }
        //angle = Mathf.SmoothDamp(angle, desiredAngle, ref velocityCameraAngle, 0.25f);

    }

    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue) {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        return (NewValue);
    }
}
