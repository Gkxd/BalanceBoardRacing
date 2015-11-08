using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    private int score;

    /**
     * <returns>Returns the current score.</returns>
     */
    public int get () {
        return this.score;
    }

    /**
     * <param name="score">New value for the score.</param>
     */
    public void setScore (int score) {
        this.score = score;
    }

    /**
     * Decrement the score by the given value. Note that the score will never go below 0.
     *
     * <param name="points">Points to subtract from the score.</param>
     */
    public void subtractPoints (int points) {
        // Score can't go below zero
        this.score = Mathf.Max (0, this.score - points);
    }

    /**
     * Add the given points to the total score.
     *
     * <param name="points">Points to add to the score.</param>
     */
    public void addPoints (int points) {
        this.score += points;
    }
}
