using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
    [Header("Reference Settings")]
    public new Rigidbody rigidbody;
    public PlayerMotionBlur motionBlur;
    public PlayerBoostParticles boostParticles;

    [Header("Body Settings")]
    public float radius;

    [Header("Speed Settings")]
    public float maxAccelerationSpeed;
    public float additionalBoostSpeed;

    public float acceleration;
    public float deceleration;
    public float breakSpeed;
    public float boostDecay;

    public float maxTurnAmount;

    public float gravity;
    public float maxFallSpeed;

    [Header("Collider Settings")]
    public LayerMask trackMask;

    private float currentTurnAngle;
    private float currentFallSpeed;

    private Vector3 previousSurfaceNormal;
    private Vector3 previousForwardOrientation;

    private float currentSpeed;
    private float boostSpeed;
    public float boost { get; set; } // The accumulated boost from pickups

    void Start() {
        previousSurfaceNormal = transform.up;
        previousForwardOrientation = transform.forward;
    }

    void FixedUpdate() {
        ///*
        if (Input.GetAxis("Vertical") > 0) {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (Input.GetAxis("Vertical") < 0) {
            currentSpeed -= breakSpeed * Time.deltaTime;
        }
        else {
            currentSpeed -= deceleration * Time.deltaTime;
            //currentSpeed += acceleration * Time.deltaTime;
        }
        //*/
        /*
        if (AccelerometerCalibration.horizontalAxis > 0.6f) {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (AccelerometerCalibration.horizontalAxis < -0.6f) {
            currentSpeed -= breakSpeed * Time.deltaTime;
        }
        else {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        //*/
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxAccelerationSpeed);
        currentSpeed += boostSpeed;

        boostSpeed = Mathf.Lerp(boostSpeed, 0, boostDecay * Time.deltaTime);
        if (boostSpeed < 0.01f) {
            motionBlur.setBlurGradually(0.3f);
        }

        //Debug.Log("V " + Input.GetAxis("Vertical") + " H " + Input.GetAxis("Horizontal"));

        float turnAngle = maxTurnAmount * Input.GetAxis("Horizontal") * Time.deltaTime;
        //float turnAngle = maxTurnAmount * AccelerometerCalibration.verticalAxis * Time.deltaTime;
        currentTurnAngle += turnAngle;

        RaycastHit raycastInfo;
        Vector3 surfaceNormal;
        if (Physics.Raycast(transform.position, -transform.up, out raycastInfo, radius, trackMask)) {
            surfaceNormal = raycastInfo.normal;

            if (raycastInfo.distance < radius) { // Stop from going through the floor
                rigidbody.position = raycastInfo.point + raycastInfo.normal.normalized * radius;
            }

            currentFallSpeed = 0;
        }
        else {
            surfaceNormal = previousSurfaceNormal;

            currentFallSpeed = Mathf.Min(currentFallSpeed + gravity * Time.deltaTime, maxFallSpeed);
        }

        Vector3 forwardOrientation = Quaternion.FromToRotation(previousSurfaceNormal, surfaceNormal) * previousForwardOrientation;
        Vector3 forward = Quaternion.AngleAxis(currentTurnAngle, surfaceNormal) * forwardOrientation;
        rigidbody.rotation = Quaternion.LookRotation(forward, surfaceNormal);

        previousSurfaceNormal = surfaceNormal;
        previousForwardOrientation = forwardOrientation;

        rigidbody.velocity = transform.forward * currentSpeed - transform.up * currentFallSpeed;
    }

    void Update() {
        if (Input.GetAxis("Jump") > 0) { // When player jumps on board
            if (boost > 0) {
                setBoost(boost);
                boost = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            if (boost < 1) {
                Debug.Log("Adding 0.1 boost.");
                boost += 0.1f;
            }
        }
    }

    public void setBoost(float f) {
        float speedIncrease = additionalBoostSpeed * f;

        currentSpeed += speedIncrease;

        if (currentSpeed > maxAccelerationSpeed) {
            boostSpeed = currentSpeed - maxAccelerationSpeed;
            currentSpeed = maxAccelerationSpeed;

            motionBlur.setBlurSuddenly(0.7f);
            boostParticles.addBoostTime(6);
        }
        else {
            boostParticles.addBoostTime(2);
        }

    }
}
