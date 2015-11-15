using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
    [Header("Gameplay Settings")]
    public float timeToUpdateScore;

    private Text scoreText;
    private int displayedScore; // what is shown right now
    private int targetScore; // what the score is interpolating toward
    private int startScore; // what the score is interpolating from
    private float finishUpdateTime; // time the score should be updated by

    private void displayScore(int score) {
        this.scoreText.text = score.ToString ();
        this.displayedScore = score;
    }

    void Start() {
        this.scoreText = this.GetComponent<Text> ();
        this.displayScore (0);
        this.targetScore = 0;
    }

    void Update() {
        if (this.displayedScore != this.targetScore) {
            if (Time.time >= this.finishUpdateTime) {
                this.displayScore(this.targetScore);
            } else {
                float percentTimeLeft = (this.finishUpdateTime - Time.time) / this.timeToUpdateScore;
                int newScore = (int) Mathf.Lerp (this.targetScore, this.startScore, percentTimeLeft);
                this.displayScore(newScore);
            }
        }
    }

    /**
     * Called by the ScoreManager to update the displayed score. The display will interpolate up to
     * the provided score, which is why this class exists separately.
     */
    public void UpdateDisplayedScore(int score) {
        this.targetScore = score;
        this.startScore = this.displayedScore;
        this.finishUpdateTime = Time.time + this.timeToUpdateScore;
    }
}
