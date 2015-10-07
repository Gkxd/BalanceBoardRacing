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
    public float value;
    public bool respawn = true;
    public float respawnTimer = 100;

    void OnTriggerEnter(Collider other) {
        PlayerMovement controller;
        if (controller = other.GetComponent<PlayerMovement>()) {
            switch (mode) { //functionality depending on mode
            case Mode.Charge: //increase players stored boost by value
                controller.boost = Mathf.Min(controller.boost + value, 1);
                Hide();
                Invoke("Show", respawnTimer);
                break;
            case Mode.BoostPad: //give instant speed boost
                controller.setBoost(value);
                break;
            case Mode.JumpPad:
                controller.rigidbody.AddForce(transform.up * Mathf.Sqrt(value) * 2000f);
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
