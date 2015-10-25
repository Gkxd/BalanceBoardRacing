using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckCorrectDirection : MonoBehaviour {
    [Header("Collider Settings")]
    public LayerMask rightMask;
    public float maximumRightRaycastDistance;

    [Header("Player UI")]
    public UI_WrongWayAlpha wrongDirection;

    void Update() {
        // Casts rays to right of player and checks if "right" mesh is hit to determine correct direction
        if (raycastRightWall()) {
            //Debug.Log("Right");
            wrongDirection.isWrongWay = false;
        }
        else {
            //Debug.Log("Not Right");
            wrongDirection.isWrongWay = true;
        }

        foreach (Ray r in raycastRays()) {
            Debug.DrawRay(r.origin, r.direction.normalized * maximumRightRaycastDistance, Color.green);
        }
    }

    private bool raycastRightWall() {
        foreach (Ray r in raycastRays()) {
            if (Physics.Raycast(r, maximumRightRaycastDistance, rightMask)) {
                return true;
            }
        }
        return false;
    }

    private IEnumerable<Ray> raycastRays() {
        yield return new Ray(transform.position, transform.right);
        yield return new Ray(transform.position, transform.right + transform.forward * 0.5f);
        yield return new Ray(transform.position, transform.right - transform.forward * 0.5f);
        yield return new Ray(transform.position, transform.right + transform.forward * 1.5f);
        yield return new Ray(transform.position, transform.right - transform.forward * 1.5f);
    }
}
