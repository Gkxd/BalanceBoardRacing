using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBoostParticles : MonoBehaviour {
    [Header("Reference Settings")]
    public ParticleSystem boostParticles;
    public PlayerMovement playerMovement;

    [Header("Gameplay Settings")]
    public AnimationCurve emissionRamp;

    public Gradient colorRamp;

    void Update() {
        float t = playerMovement.getBoostRatio();
        boostParticles.startColor = Color.Lerp(colorRamp.Evaluate(t), Color.white, Random.Range(0, 0.25f));
        boostParticles.emissionRate = emissionRamp.Evaluate(t);
    }
}
