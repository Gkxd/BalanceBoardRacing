using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIText : MonoBehaviour {

    public PlayerMovement player;
    public string variable;

    private string initialText;
    private Text text;

	void Start () {
        text = GetComponent<Text>();
        initialText = text.text;
	}
	
	void FixedUpdate () {
        text.text = initialText + player.GetType().GetField(variable).GetValue(player);	
        
	}
}
