using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
    [Header("Reference Settings")]
    public new Rigidbody rigidbody;
    public PlayerMotionBlur motionBlur;
    public PlayerHead head;

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

    [Header("Player UI")]
    public GameObject wrongDirection;

    private RaycastHit wallHit;
    private int wallMask = 1 << 11; // Ray will only check right wall mesh

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
        if (Input.GetAxis("Vertical") > 0) {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (Input.GetAxis("Vertical") < 0) {
            currentSpeed -= breakSpeed * Time.deltaTime;
        }
        else {
            currentSpeed -= deceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxAccelerationSpeed);
        currentSpeed += boostSpeed;

        boostSpeed = Mathf.Lerp(boostSpeed, 0, boostDecay * Time.deltaTime);
        if (boostSpeed < 0.01f) {
            motionBlur.setBlurGradually(0.3f);
        }

        //Debug.Log(Input.GetAxis("Vertical") + " " + Input.GetAxis("Horizontal"));

        float turnAngle = maxTurnAmount * Input.GetAxis("Horizontal") * Time.deltaTime;
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

        //Casts Ray to right of player and checks if "right" mesh is hit to determine correct direction
        if (Physics.Raycast(rigidbody.position, transform.right * 500, out wallHit, 5000, wallMask))
        {
            if (wallHit.collider.gameObject.name != "Right")
            {
                wrongDirection.SetActive(true); //Activate WrongWay X
                Debug.Log("Not Right");
            }
            else
            {
                wrongDirection.SetActive(false); //Deactivate WrongWay X
                Debug.Log("Right");
            }
        }

        //Debugging to check directionality
        Debug.DrawRay(rigidbody.position, transform.right * 500, Color.red);

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
        }
    }
}
