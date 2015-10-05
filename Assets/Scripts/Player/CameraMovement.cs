using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    
    [Header("Gameplay Settings")]
    public bool useMouse;
    public float mouseSensitivityX;
    public float mouseSensitivityY;

    private float yaw, pitch;

    void Start() {
        yaw = transform.localEulerAngles.y;
        pitch = transform.localEulerAngles.x;
    }

    void Update() {
        if (useMouse) {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

            pitch = Mathf.Clamp(pitch, -60, 60);
        }

        transform.localEulerAngles = new Vector3(pitch, yaw, transform.localEulerAngles.z);
    }
}
