using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSCManager : MonoBehaviour {
    public static Vector3 accelerationVector {
        get {
            return new Vector3(accelerometerX, accelerometerY, accelerometerZ);
        }
    }

    private static float accelerometerX;
    private static float accelerometerY;
    private static float accelerometerZ;

    void Awake() {
        OSCHandler.Instance.Init();
        Debug.Log("Connect app to: " + Network.player.ipAddress + ":5555");
    }

    void Update() {
        OSCHandler.Instance.UpdateLogs();
        List<string> serverLog = OSCHandler.Instance.Servers["BalanceBoardRacing"].log;

        string output = "";
        foreach (string s in serverLog) {
            output += s;

            int index;

            if ((index = s.IndexOf("/accelerometer")) != -1) {
                string[] tokens = s.Substring(index).Split(' ');
                accelerometerX = float.Parse(tokens[1]);
                accelerometerY = float.Parse(tokens[2]);
                accelerometerZ = float.Parse(tokens[3]);
            }
            /*
            if ((index = s.IndexOf("/accelerometer/raw/x")) != -1) {
                accelerometerX = float.Parse(s.Substring(index + 20).Trim());
            }
            else if ((index = s.IndexOf("/accelerometer/raw/y")) != -1) {
                accelerometerY = float.Parse(s.Substring(index + 20).Trim());
            }
            else if ((index = s.IndexOf("/accelerometer/raw/z")) != -1) {
                accelerometerZ = float.Parse(s.Substring(index + 20).Trim());
            }
             */
        }

        Debug.DrawRay(Vector3.zero, new Vector3(accelerometerX, accelerometerY, accelerometerZ), Color.cyan);
    }
}
