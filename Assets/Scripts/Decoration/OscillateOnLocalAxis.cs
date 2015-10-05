using UnityEngine;
using System.Collections;

public class OscillateOnLocalAxis : MonoBehaviour {
    [Header("Gameplay Settings")]
    public Vector3 amplitude;
    public float frequency;

    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.localPosition;
    }

    void Update() {
        transform.localPosition = initialPosition + amplitude * Mathf.Cos(frequency * Time.time * 2 * Mathf.PI);
    }
}
