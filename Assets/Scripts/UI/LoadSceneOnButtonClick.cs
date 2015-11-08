using UnityEngine;
using System.Collections;

public class LoadSceneOnButtonClick : MonoBehaviour {
    public string sceneName;

    public void onButtonClick() {
        Application.LoadLevel(sceneName);
    }
}
