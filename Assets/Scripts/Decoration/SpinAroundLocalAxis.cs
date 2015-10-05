using UnityEngine;
using System.Collections;

public class SpinAroundLocalAxis : MonoBehaviour {

    [Header("Gameplay Settings")]
    public Vector3 spinAmount;

    void Update() {
        transform.localEulerAngles += spinAmount * Time.deltaTime;
    }
}
