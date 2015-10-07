using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour {
    [Header("Reference Settings")]
    public PlayerMovement controller;
    public new Camera camera;

    private float targetFOV = 90;
    private float cameraFOV = 90;

    void Update() {
        transform.position = Vector3.Lerp(transform.position, controller.gameObject.transform.position, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, controller.gameObject.transform.rotation, 0.02f);

        cameraFOV = Mathf.Lerp(cameraFOV, targetFOV, Time.deltaTime);
        camera.fieldOfView = cameraFOV;
    }

    public void setFOV(float f) {
        targetFOV = f;
    }
}