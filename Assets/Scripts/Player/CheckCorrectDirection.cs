using UnityEngine;
using System.Collections;

public class CheckCorrectDirection : MonoBehaviour {
    [Header("Collider Settings")]
    public LayerMask rightMask;
    public float maximumRightRaycastDistance;

    [Header("Player UI")]
    public GameObject wrongDirection;

    void Update() {
        // Casts rays to right of player and checks if "right" mesh is hit to determine correct direction
        if (Physics.Raycast(transform.position, transform.right + transform.forward, maximumRightRaycastDistance, rightMask) ||
            Physics.Raycast(transform.position, transform.right - transform.forward, maximumRightRaycastDistance, rightMask)) {
            //Debug.Log("Right");
            //wrongDirection.SetActive(true); //Activate WrongWay X
        }
        else {
            //Debug.Log("Not Right");
            //wrongDirection.SetActive(false); //Deactivate WrongWay X
        }

        //Debugging to check directionality
        /*
        Debug.DrawRay(transform.position, (transform.right + transform.forward) * maximumRightRaycastDistance, Color.red);
        Debug.DrawRay(transform.position, (transform.right - transform.forward) * maximumRightRaycastDistance, Color.red);
        */
    }
}
