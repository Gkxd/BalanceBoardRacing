using UnityEngine;
using System.Collections;

public class BoostPad : MonoBehaviour {
    [Header("Reference Settings")]
    public new Collider collider;
    [Header("Gameplay Settings")]
    [Range(0f, 1f)]
    public float strength;

    void OnTriggerEnter(Collider other) {
        PlayerMovement controller;
        if (controller = other.GetComponent<PlayerMovement>()) {
            controller.setBoost(strength);
            //AudioSource.PlayClipAtPoint(soundEffect, transform.position, 1);
        }

        ScoreManager scoreManager = (ScoreManager) GameObject.FindObjectOfType(typeof(ScoreManager));
        scoreManager.onBoostPad();
    }
}
