using UnityEngine;
using System.Collections;

public class SpawnRotatingRing : MonoBehaviour {

    [Header("Gameplay Settings")]
    public float radius;
    public float meshScale;
    public int amount;
    public Mesh mesh;
    public Material[] materials;
    public float spinAmount;

    void Start() {
        for (int i = 0; i < amount; i++) {
            float angle = i * 2 * Mathf.PI / amount;

            GameObject ring = new GameObject("Spawned Ring " + i);

            ring.transform.parent = transform;
            ring.transform.localPosition = (Mathf.Cos(angle) * Vector3.right + Mathf.Sin(angle) * Vector3.up) * radius;
            ring.transform.localEulerAngles = new Vector3(90 - angle * Mathf.Rad2Deg, 90, 0);

            GameObject model = new GameObject("Model");
            model.transform.parent = ring.transform;
            model.transform.localPosition = Vector3.zero;
            model.transform.localEulerAngles = Vector3.forward * angle * Mathf.Rad2Deg;
            model.transform.localScale = Vector3.one * meshScale;

            MeshFilter meshFilter = model.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            MeshRenderer meshRenderer = model.AddComponent<MeshRenderer>();
            meshRenderer.material = materials[i % materials.Length];
            SpinAroundLocalAxis spin = model.AddComponent<SpinAroundLocalAxis>();
            spin.spinAmount = Vector3.forward * spinAmount;
        }
    }

    void Update() {

    }
}
