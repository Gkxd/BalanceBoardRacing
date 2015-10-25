using UnityEngine;
using System.Collections;

public class PlayerMotionBlur : MonoBehaviour {
    [Header("Reference Settings")]
    public UnityStandardAssets.ImageEffects.MotionBlur motionBlur;
    public PlayerMovement playerMovement;

    [Header("Gameplay Settings")]
    public AnimationCurve blurRamp;

    private float blurAmount = 0.3f;
    private float targetBlurAmount = 0.3f;

    void Update() {
        motionBlur.blurAmount = blurRamp.Evaluate(playerMovement.getBoostRatio());
    }
}
