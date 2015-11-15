using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class FinishTrigger : MonoBehaviour {
    private bool finished = false;

    [Header("Reference Settings")]
    public BoxCollider finishCollider;
    public ScoreManager scoreManager;

    [Header("Gameplay Settings")]
    public float raceTime;
    public int lapNumber;
    public int maxLaps;

    //public Text timer;

    void Start()
    {
        finishCollider.enabled = false; //Initially finish line is deactivated
    }

    void OnTriggerExit()
    {
        this.scoreManager.onFinishLap ();
        if (lapNumber < maxLaps)
        {
            lapNumber++;

            finishCollider.enabled = false; //Passing through finish line collider deactivates it
        }
        else
        {
            /* For Debugging Purposes */
            Debug.Log("YOU WIN!");      
            finished = true;
            this.scoreManager.onFinishTrack();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (!finished) {

            //timer.text = raceTime.ToString("f2");

            raceTime = Time.time;

            //raceTime = raceTime + 1 * Time.deltaTime;
        }
    }
}
