using UnityEngine;
using System.Collections;

public class BalanceBoardTest : MonoBehaviour {
    
	void Update () {
        //Debug.Log(Input.GetAxisRaw("Horizontal") + " " + Input.GetAxisRaw("Vertical"));

        Debug.Log(Input.inputString);
	}
}
