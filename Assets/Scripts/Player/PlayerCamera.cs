using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    public GameObject playerBody;

	void Update () {
        transform.position = Vector3.Lerp(transform.position, playerBody.transform.position, 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerBody.transform.rotation, 0.07f);
	}
}
