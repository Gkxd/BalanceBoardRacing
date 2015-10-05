using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour {
    [Header("Reference Settings")]
    public PlayerController controller;
    public new Camera camera;

    [Header("Gameplay Settings")]
    public bool boostFOV = true; //should FOV change when you boost?

    private float FOV;

    void Start() {
        FOV = camera.fieldOfView;
    }

    void Update() {
        transform.position = Vector3.Lerp(transform.position, controller.gameObject.transform.position, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, controller.gameObject.transform.rotation, 0.02f);

        if (boostFOV) {
            if (controller.currentSpeed > controller.maxSpeed * 1.1f) {
                camera.fieldOfView = FOV + 2 * (controller.currentSpeed - controller.maxSpeed * 1.1f);
            }
            else camera.fieldOfView = FOV;
        }
    }
}