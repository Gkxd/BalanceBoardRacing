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
        // Casts rays to left of player and checks if "right" mesh is hit to determine if player is facing wrong way
        // This gives better results than casting rays to the right
        wrongDirection.isWrongWay = raycastRightWall();
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
        yield return new Ray(transform.position, -transform.right);
        yield return new Ray(transform.position, -transform.right + transform.forward * 0.5f);
        yield return new Ray(transform.position, -transform.right - transform.forward * 0.5f);
    }
}
