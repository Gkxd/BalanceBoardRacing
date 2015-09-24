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

    private new Rigidbody rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
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

        Debug.Log(Input.GetAxis("Vertical") + " " + Input.GetAxis("Horizontal"));

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        float turnAngle = maxTurnAmount * Input.GetAxis("Horizontal");
        currentTurnAngle += maxTurnAmount * Input.GetAxis("Horizontal") * Time.deltaTime;

        RaycastHit raycastInfo;
        if (Physics.Raycast(transform.position, -transform.up, out raycastInfo, radius, trackMask)) {
            Vector3 surfaceNormal = raycastInfo.normal;

            if (raycastInfo.distance < radius) {
                rigidbody.position = raycastInfo.point + raycastInfo.normal.normalized * radius;
            }

            transform.up = surfaceNormal;
            Vector3 facing = Quaternion.AngleAxis(currentTurnAngle, transform.up) * transform.forward;
            transform.LookAt(transform.position + facing, transform.up);

            currentFallSpeed = 0;
        }
        else {
            currentFallSpeed = Mathf.Min(currentFallSpeed + gravity * Time.deltaTime, maxFallSpeed);
        }

        rigidbody.velocity = transform.forward * currentSpeed - transform.up * currentFallSpeed;
    }
}
