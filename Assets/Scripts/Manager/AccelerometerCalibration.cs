using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AccelerometerCalibration : MonoBehaviour {

    //private static List<Vector3> forwardSamples, backwardSamples, leftSamples, rightSamples, restingSamples;
    private static VectorStats forward, backward, left, right, rest;

    public static float horizontalAxis {
        get {
            if (Vector3.Angle(rest.average, OSCManager.accelerationVector) < 20) {
                return 0;
            }
            else if (Vector3.Angle(left.average, OSCManager.accelerationVector) < Vector3.Angle(right.average, OSCManager.accelerationVector)) {
                return -1;
            }
            else {
                return 1;
            }
        }
    }

    public static float verticalAxis {
        get {
            if (Vector3.Angle(rest.average, OSCManager.accelerationVector) < 20) {
                return 0;
            }
            else if (Vector3.Angle(backward.average, OSCManager.accelerationVector) < Vector3.Angle(forward.average, OSCManager.accelerationVector)) {
                return -1;
            }
            else {
                return 1;
            }
        }
    }

    void Awake() {
        resetSamples();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            left.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            right.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
            forward.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            backward.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.Q)) {
            rest.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            resetSamples();
        }

        Debug.DrawRay(Vector3.zero, left.average, Color.red);
        Debug.DrawRay(Vector3.zero, right.average, Color.blue);
        Debug.DrawRay(Vector3.zero, forward.average, Color.green);
        Debug.DrawRay(Vector3.zero, backward.average, Color.yellow);
        Debug.DrawRay(Vector3.zero, rest.average, Color.white);
    }

    private void resetSamples() {
        forward.reset();
        backward.reset();
        left.reset();
        right.reset();
        rest.reset();
    }

    private struct VectorStats {
        public Vector3 average {get; private set;}
        public int count {get; private set;}

        public void reset() {
            average = Vector3.zero;
            count = 0;
        }

        public void add(Vector3 vector) {
            Vector3 total = average * count++ + vector;
            average = total / count;
        }
    }
}
