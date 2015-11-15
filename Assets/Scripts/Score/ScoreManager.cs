using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    private bool playing;

    [Header("Reference Settings")]
    public Score score; // Model
    public ScoreDisplay scoreDisplay; // View

    [Header("Gameplay Settings")]
    public int boostPadScore;
    public int lapMillisecondsForBonus;
    public int lapFinishScore;

    private int lapStartTime;

    void Start () {
        this.onStartTrack ();
    }

    private void updateDisplayedScore () {
        this.scoreDisplay.UpdateDisplayedScore (this.score.get ());
    }

    /**
     * Called on the start of a new track race; initializes the score.
     */
    public void onStartTrack() {
        this.score.setScore (0);
        this.updateDisplayedScore ();
        this.onStartLap ();
        this.playing = true;
    }

    private void onStartLap() {
        this.lapStartTime = (int)(Time.time * 1000);
    }

    /**
     * Called by the FinishTrigger when any lap is finished.
     */
    public void onFinishLap () {
        int lapMilliseconds = ((int)(Time.time * 1000)) - this.lapStartTime;
        int lapTimeBonus = Mathf.Max (0, this.lapMillisecondsForBonus - lapMilliseconds);
        int lapFinishPoints = lapTimeBonus + this.lapFinishScore;
        this.score.addPoints (lapFinishPoints);
        this.updateDisplayedScore ();
        this.onStartLap ();
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
        this.updateDisplayedScore ();
    }
}
