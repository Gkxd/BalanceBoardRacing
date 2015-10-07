using UnityEngine;
using System.Collections;

public class AnimateRampTexture : MonoBehaviour {

    [Header("Reference Settings")]
    public new MeshRenderer renderer;

    [Header("Gameplay Settings")]
    public float stepSize;
    public float speed;

    void Update() {
        float offset = speed * stepSize * Time.time;
        renderer.material.SetTextureOffset("_MainTex", Vector2.right * offset);
    }
}