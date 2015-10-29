using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinishTrigger : MonoBehaviour {

    private bool finished = false;

    public float raceTime;
    public int lapNumber;
    public int maxLaps;

    //public Text timer;

    void OnTriggerEnter()
    {
        if (lapNumber < maxLaps)
        {
            lapNumber++;
        }
        else
        {
            Debug.Log("YOU WIN!");
            finished = true;
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
