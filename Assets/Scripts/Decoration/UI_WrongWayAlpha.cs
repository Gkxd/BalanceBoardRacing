using UnityEngine;
using System.Collections;

public class UI_WrongWayAlpha : MonoBehaviour {
    [Header("Reference Settings")]
    public Renderer renderer;

    [Header("Gameplay Settings")]
    public float frequency;

    public bool isWrongWay { private get; set; }

    private Material material;
    private Color currentColor;

    void Start() {
        material = renderer.material;
        currentColor = Color.clear;
    }

    void Update() {
        Color targetColor = Color.clear;
        if (isWrongWay) {
            targetColor = Color.white * (0.15f * Mathf.Sin(Time.time * frequency) + 1.05f);
        }

        currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * 10);

        material.SetColor("_Color", currentColor);
    }
}
