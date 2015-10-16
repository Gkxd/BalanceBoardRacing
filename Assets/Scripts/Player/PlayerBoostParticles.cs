using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBoostParticles : MonoBehaviour {
    [Header("Reference Settings")]
    public ParticleSystem boostParticles;

    [Header("Gameplay Settings")]
    public AnimationCurve emissionRamp;

    public float colorRampLength;
    public Gradient colorRamp;

    private float boostTime;

    void Update() {
        if (boostTime > 0) {
            boostTime -= Time.deltaTime;
        }
        else {
            boostTime = 0;
        }

        float t = boostTime / colorRampLength;
        boostParticles.startColor = Color.Lerp(colorRamp.Evaluate(t), Color.white, Random.Range(0, 0.25f));

        boostParticles.emissionRate = emissionRamp.Evaluate(boostTime);
    }

    public void addBoostTime(float time) {
        boostTime += time;
    }
}
