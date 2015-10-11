using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBoostParticles : MonoBehaviour {
    [Header("Reference Settings")]
    public List<ParticleSystem> enableOnBoost;
    public List<ParticleSystem> disableOnBoost;

    private float boostTime;

    void Start() {
        disableBoostParticles();
    }

    void Update() {
        if (boostTime > 0) {
            boostTime -= Time.deltaTime;
            enableBoostParticles();
        }
        else {
            boostTime = 0;
            disableBoostParticles();
        }
    }

    private void enableBoostParticles() {
        foreach (ParticleSystem p in enableOnBoost) {
            p.enableEmission = true;
        }
        foreach (ParticleSystem p in disableOnBoost) {
            p.enableEmission = false;
        }
    }

    private void disableBoostParticles() {
        foreach (ParticleSystem p in enableOnBoost) {
            p.enableEmission = false;
        }
        foreach (ParticleSystem p in disableOnBoost) {
            p.enableEmission = true;
        }
    }

    public void addBoostTime(float time) {
        boostTime += time;
    }
}
