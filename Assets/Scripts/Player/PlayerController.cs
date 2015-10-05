using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    [Header("Reference Settings")]
    public new Rigidbody rigidbody;

    [Header("Body Settings")]
    public float radius;

    [Header("Speed Settings")]
    public float maxSpeed;
    public float boostSpeed = 10f;

    public float acceleration;
    public float deceleration;
    public float breakSpeed;

    public float maxTurnAmount;

    public float gravity;
    public float maxFallSpeed;

    [Header("Collider Settings")]
    public LayerMask trackMask;

    private float currentTurnAngle;
    private float currentFallSpeed;
    [HideInInspector]
    public float currentSpeed;

    private Vector3 previousSurfaceNormal;
    private Vector3 previousForwardOrientation;

    [HideInInspector]
    public float boost = 0;
    [HideInInspector]
    public float padBoost = 0; //hidden boost value for when going over boost pads
    [HideInInspector]
    public bool boosting = false;

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

        //Debug.Log(Input.GetAxis("Vertical") + " " + Input.GetAxis("Horizontal"));


        if (boosting)
        {
            currentSpeed += acceleration * boostSpeed / maxSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, boostSpeed);

            if (padBoost > 0) //pad boosting
            {
                padBoost -= 0.5f;
                if (padBoost <= 0)
                {
                    padBoost = 0;
                    boosting = false;
                }
            }
            else if (boost > 0) //charge boosting
            {
                boost -= 0.5f;
                if (boost <= 0)
                {
                    boost = 0;
                    boosting = false;
                }
            }
        }
        else
        {
            currentSpeed = Mathf.Clamp(currentSpeed, 0, boostSpeed); //min speed = 0

            if (currentSpeed > maxSpeed)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, currentSpeed * Time.deltaTime); //return to max speed after boosting
            }
        }

        float turnAngle = maxTurnAmount * Input.GetAxis("Horizontal") * Time.deltaTime;
        currentTurnAngle += turnAngle;

        RaycastHit raycastInfo;
        Vector3 surfaceNormal;
        if (Physics.Raycast(transform.position, -transform.up, out raycastInfo, radius, trackMask)) {
            surfaceNormal = raycastInfo.normal;

            if (raycastInfo.distance < radius)
            { //stop from going through the floor
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

    void Update()
    {
        if (Input.GetAxis("Jump") > 0f)
        { //When player jumps on board
            if (boost > 0)
            {
                boosting = true;
            }
        }
    }
}
