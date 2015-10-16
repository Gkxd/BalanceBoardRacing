using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBoostParticles : MonoBehaviour {
    [Header("Reference Settings")]
    public ParticleSystem boostParticles;

    [Header("Gameplay Settings")]
    public float rampLength;
    public AnimationCurve rampStretch;
    public AnimationCurve emissionRamp;
    public float minEmission;
    public float maxEmission;

    public Gradient startColor1;

    private float boostTime;

    void Update() {
        if (boostTime > 0) {
            boostTime -= Time.deltaTime;
        }
        else {
            boostTime = 0;
        }

        float t = rampStretch.Evaluate(boostTime / rampLength);


        boostParticles.startColor = Color.Lerp(startColor1.Evaluate(t), Color.white, Random.Range(0, 0.25f));

        boostParticles.emissionRate = emissionRamp.Evaluate(t) * (maxEmission - minEmission) + minEmission;
    }

    public void addBoostTime(float time) {
        boostTime += time;
    }
}
