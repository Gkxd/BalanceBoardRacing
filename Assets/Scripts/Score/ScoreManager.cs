using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    private bool playing;

    [Header("Reference Settings")]
    public Score score;

    [Header("Gameplay Settings")]
    public int startScore;
    public int pointsLostPerMillisecond;
    public int boostPadScore;

    void Start () {
        this.onStartTrack ();
    }

    public void onStartTrack() {
        this.score.setScore (this.startScore);
        this.playing = true;
    }

    void Update () {
        Debug.Log ("Score: " + this.score.get ());
        if (playing) {
            int millisecondsPassed = (int)(Time.deltaTime * 1000);
            int pointsLost = millisecondsPassed * pointsLostPerMillisecond;
            this.score.subtractPoints (pointsLost);
        }
    }

    /**
     * Called by the FinishTrigger when the last lap is finished.
     */
    public void onFinishTrack () {
        this.playing = false;
    }

    /**
     * Called by a boost pad when a player enters one.
     */
    public void onBoostPad() {
        this.score.addPoints (boostPadScore);
    }
}
