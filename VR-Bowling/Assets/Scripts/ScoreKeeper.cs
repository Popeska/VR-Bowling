using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro

public class ScoreKeeper : MonoBehaviour
{
    private int[] scores = new int[21]; // Holds the score for up to 21 rolls (max rolls possible in a game)
    private int currentRoll = 0; // Keeps track of the current roll
    private int currentFrame = 1; // Keeps track of the current frame (1 to 10)
    private int totalScore = 0; // Total score of the game

    // Method to record the score after a roll
    public void RecordRoll(int pins)
    {
        scores[currentRoll] = pins;
        currentRoll++;

        // Handle the frame logic
        if (currentFrame <= 10)
        {
            if (IsStrike(currentRoll - 1))
            {
                currentFrame++; // Move to the next frame immediately after a strike
            }
            else if (currentRoll % 2 == 0 || currentFrame == 10) // If it's the second throw of the frame
            {
                currentFrame++; // Move to the next frame after the second throw
            }
        }

        UpdateScore();
    }

    private void UpdateScore()
    {
        totalScore = 0;
        int rollIndex = 0;

        // Calculate score per frame
        for (int frame = 1; frame <= 10; frame++)
        {
            if (IsStrike(rollIndex))
            {
                totalScore += 10 + StrikeBonus(rollIndex);
                rollIndex++;
            }
            else if (IsSpare(rollIndex))
            {
                totalScore += 10 + SpareBonus(rollIndex);
                rollIndex += 2;
            }
            else
            {
                totalScore += scores[rollIndex] + scores[rollIndex + 1];
                rollIndex += 2;
            }
        }
    }

    private bool IsStrike(int rollIndex)
    {
        return scores[rollIndex] == 10;
    }

    private bool IsSpare(int rollIndex)
    {
        return scores[rollIndex] + scores[rollIndex + 1] == 10;
    }

    private int StrikeBonus(int rollIndex)
    {
        return scores[rollIndex + 1] + scores[rollIndex + 2];
    }

    private int SpareBonus(int rollIndex)
    {
        return scores[rollIndex + 2];
    }

    // Get the total score calculated from all rolls
    public int GetTotalScore()
    {
        return totalScore;
    }
     public bool IsGameOver()
    {
        return currentFrame > 10;
    }

    // For UI updates
    public void UpdateScoreText(TextMeshProUGUI scoreText)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
        }
    }

    // Reset the score-related variables when restarting the game
    public void ResetScore()
    {
        scores = new int[21]; // Reset the scores array
        currentRoll = 0; // Reset the current roll index
        currentFrame = 1; // Reset to the first frame
        totalScore = 0; // Reset the total score
    }
}
