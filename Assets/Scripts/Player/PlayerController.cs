using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    [Header("Body Settings")]
    public float radius;

    [Header("Speed Settings")]
    public float maxSpeed;

    public float acceleration;
    public float deceleration;
    public float breakSpeed;

    public float maxTurnAmount;

    public float gravity;
    public float maxFallSpeed;

    [Header("Collider Settings")]
    public LayerMask trackMask;

    private float currentTurnAngle;
    private float currentSpeed;
    private float currentFallSpeed;

    private Vector3 previousSurfaceNormal;
    private Vector3 previousForwardOrientation;

    private new Rigidbody rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        previousSurfaceNormal = Vector3.up;
        previousForwardOrientation = Vector3.forward;
    }

    void FixedUpdate() {
        if (Input.GetAxis("Vertical") > 0) {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (Input.GetAxis("Vertical") < 0) {
            currentSpeed -= breakSpeed * Time.deltaTime;
        }
        else {
            currentSpeed -= deceleration * Time.deltaTime;
        }

        //Debug.Log(Input.GetAxis("Vertical") + " " + Input.GetAxis("Horizontal"));

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        float turnAngle = maxTurnAmount * Input.GetAxis("Horizontal") * Time.deltaTime;
        currentTurnAngle += turnAngle;

        RaycastHit raycastInfo;
        if (Physics.Raycast(transform.position, -transform.up, out raycastInfo, radius, trackMask)) {
            Vector3 surfaceNormal = raycastInfo.normal;

            if (raycastInfo.distance < radius) {
                rigidbody.position = raycastInfo.point + raycastInfo.normal.normalized * radius;
            }
            
            Vector3 forwardOrientation = Quaternion.FromToRotation(previousSurfaceNormal, surfaceNormal) * previousForwardOrientation;
            Vector3 forward = Quaternion.AngleAxis(currentTurnAngle, surfaceNormal) * forwardOrientation;
            rigidbody.rotation = Quaternion.LookRotation(forward, surfaceNormal);

            previousSurfaceNormal = surfaceNormal;
            previousForwardOrientation = forwardOrientation;

            currentFallSpeed = 0;
        }
        else {
            currentFallSpeed = Mathf.Min(currentFallSpeed + gravity * Time.deltaTime, maxFallSpeed);
        }

        rigidbody.velocity = transform.forward * currentSpeed - transform.up * currentFallSpeed;
    }
}
