using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    [Header("Reference Settings")]
    public new MeshRenderer renderer;
    public new Collider collider;

    public enum Mode {
        BoostPad,
        Charge,
        JumpPad,
    }

    [Header("Gameplay Settings")]
    public Mode mode;
    [Range(0f, 1f)]
    public float strength;
    public bool respawn = true;
    public float respawnTimer = 100;

    void OnTriggerEnter(Collider other) {
        PlayerMovement controller;
        if (controller = other.GetComponent<PlayerMovement>()) {
            switch (mode) {
            case Mode.Charge: // Increase player's stored boost by value
                controller.boost = Mathf.Min(controller.boost + strength, 1);
                Hide();
                Invoke("Show", respawnTimer);
                break;
            case Mode.BoostPad: // Give instant speed boost
                controller.setBoost(strength);
                break;
            case Mode.JumpPad:
                controller.rigidbody.AddForce(transform.up * Mathf.Sqrt(strength) * 2000f);
                break;
            }
        }
    }

    void Hide() {
        renderer.enabled = false;
        collider.enabled = false;
    }

    void Show() {
        renderer.enabled = true;
        collider.enabled = true;
    }
}
