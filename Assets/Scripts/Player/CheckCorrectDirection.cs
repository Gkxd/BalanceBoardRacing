using UnityEngine;
using System.Collections;

public class CheckCorrectDirection : MonoBehaviour {
    [Header("Collider Settings")]
    public LayerMask rightMask;
    public float maximumRightRaycastDistance;

    [Header("Player UI")]
    public UI_WrongWayAlpha wrongDirection;

    void Update() {
        // Casts rays to right of player and checks if "right" mesh is hit to determine correct direction
        if (Physics.Raycast(transform.position, transform.right + transform.forward * 0.5f, maximumRightRaycastDistance, rightMask) ||
            Physics.Raycast(transform.position, transform.right - transform.forward * 0.5f, maximumRightRaycastDistance, rightMask)) {
            //Debug.Log("Right");
            wrongDirection.isWrongWay = false;
        }
        else {
            //Debug.Log("Not Right");
            wrongDirection.isWrongWay = true;
        }
    }
}
