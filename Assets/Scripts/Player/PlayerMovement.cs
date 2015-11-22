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
    public LayerMask wallMask;

    private float currentTurnAngle;
    private float currentFallSpeed;

    private Vector3 previousSurfaceNormal;
    private Vector3 previousForwardOrientation;

    private float currentSpeed;
    private float boostSpeed;
    public float boost { get; set; } //The accumulated boost from pickups

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
            //currentSpeed -= deceleration * Time.deltaTime;
            currentSpeed += acceleration / 5 * Time.deltaTime;
        }
        //*/
        /*
        if (AccelerometerCalibration.verticalAxis > 0f) {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (AccelerometerCalibration.verticalAxis < 0f) {
            currentSpeed -= breakSpeed * Time.deltaTime;
        }
        else {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        //*/
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxAccelerationSpeed);
        currentSpeed += boostSpeed;

        boostSpeed = Mathf.Lerp(boostSpeed, 0, boostDecay * Time.deltaTime);

        //Debug.Log("V " + AccelerometerCalibration.verticalAxis + " H " + AccelerometerCalibration.horizontalAxis);

        float turnAngle = maxTurnAmount * Input.GetAxis("Horizontal") * Time.deltaTime;
        //float turnAngle = maxTurnAmount * AccelerometerCalibration.horizontalAxis * Time.deltaTime;
        currentTurnAngle += turnAngle;


        //This has to do with the track directionality
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

        Debug.DrawRay(transform.position, forwardOrientation * 5, Color.red);
        Debug.DrawRay(transform.position, forward * 5, Color.yellow);
        /*
        if (Physics.Raycast(transform.position, forward, out raycastInfo, 5, wallMask)) {
            //Vector3 newForward = (raycastInfo.point + raycastInfo.normal - transform.position);
            Vector3 newForward = 5*forward + raycastInfo.normal;

            newForward = Vector3.ProjectOnPlane(newForward, surfaceNormal).normalized;

            Vector3 crossA = Vector3.Cross(forwardOrientation, forward);
            Vector3 crossB = Vector3.Cross(forwardOrientation, newForward);


            float newTurnAngle;
            if (Vector3.Dot(crossA, crossB) > 0) {
                newTurnAngle = Vector3.Angle(forwardOrientation, newForward);
            }
            else {
                newTurnAngle = -Vector3.Angle(forwardOrientation, newForward);
            }

            currentTurnAngle = newTurnAngle;
            forward = Quaternion.AngleAxis(currentTurnAngle, surfaceNormal) * forwardOrientation;
        } */

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

    void OnCollisionStay(Collision collision) {
        //currentSpeed = 0.1f;
        //boostSpeed = 0;
    }

    public void setBoost(float f) {
        float speedIncrease = additionalBoostSpeed * f;

        currentSpeed += speedIncrease;

        if (currentSpeed > maxAccelerationSpeed) {
            boostSpeed = currentSpeed - maxAccelerationSpeed;
            currentSpeed = maxAccelerationSpeed;
        }
    }

    public float getBoostRatio() {
        return boostSpeed / additionalBoostSpeed;
    }
}
