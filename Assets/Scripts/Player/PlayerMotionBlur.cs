using UnityEngine;
using System.Collections;

public class PlayerMotionBlur : MonoBehaviour {
    [Header("Reference Settings")]
    public UnityStandardAssets.ImageEffects.MotionBlur motionBlur;

    private float blurAmount = 0.3f;
    private float targetBlurAmount = 0.3f;

    void Update() {
        blurAmount = Mathf.Lerp(blurAmount, targetBlurAmount, Time.deltaTime);
        motionBlur.blurAmount = blurAmount;
    }

    public void setBlurSuddenly(float f) {
        targetBlurAmount = blurAmount = f;
    }

    public void setBlurGradually(float f) {
        targetBlurAmount = f;
    }
}
