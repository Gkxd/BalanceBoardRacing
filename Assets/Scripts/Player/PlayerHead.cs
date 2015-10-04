using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour {

    public PlayerController controller;

    [SerializeField]
    private bool boostFOV = true; //should FOV change when you boost?
    private Camera cam;
    private float FOV;

    void Start ()
    {
        cam = Camera.main;
        FOV = cam.fieldOfView;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, controller.gameObject.transform.position, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, controller.gameObject.transform.rotation, 0.02f);

        if (boostFOV)
        {
            if (controller.currentSpeed > controller.maxSpeed * 1.1f)
            {
                cam.fieldOfView = FOV + 2 * (controller.currentSpeed - controller.maxSpeed * 1.1f);
            }
            else cam.fieldOfView = FOV;
        }

    }
}