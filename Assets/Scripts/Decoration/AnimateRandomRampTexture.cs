using UnityEngine;
using System.Collections;

public class AnimateRandomRampTexture : MonoBehaviour {

    [Header("Reference Settings")]
    public new MeshRenderer renderer;

    [Header("Gameplay Settings")]
    public float stepInterval;
    public int materialIndex;

    void Start() {
        StartCoroutine(updateFrequency());
    }

    private IEnumerator updateFrequency() {
        while (true) {
            renderer.materials[materialIndex].SetTextureOffset("_MainTex", Vector2.up * Random.value);
            yield return new WaitForSeconds(stepInterval);
        }
    }
}
