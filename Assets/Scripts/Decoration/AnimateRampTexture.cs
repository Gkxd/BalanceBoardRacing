using UnityEngine;
using System.Collections;

public class AnimateRampTexture : MonoBehaviour {

    [Header("Reference Settings")]
    public new MeshRenderer renderer;

    [Header("Gameplay Settings")]
    public float stepSize;
    public float speed;

    public int materialIndex;

    void Update() {
        float offset = speed * stepSize * Time.time;
        renderer.materials[materialIndex].SetTextureOffset("_MainTex", Vector2.right * offset);
    }
}