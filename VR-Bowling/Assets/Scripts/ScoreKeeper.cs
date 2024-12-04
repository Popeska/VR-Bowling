using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 


public class ScoreKeeper : MonoBehaviour
{
    private int[] scores = new int[21]; // Holds the score for up to 21 rolls (max rolls possible in a game)
    private int currentRoll = 1; // Keeps track of the current roll
    private int currentFrame = 1; // Keeps track of the current frame (1 to 10)
    private int totalScore = 0; // Total score of the game

    public TextMeshProUGUI[] frameScoreText;  // An array of TextMeshProUGUI to display each frame's score
    public TextMeshProUGUI totalScoreText;    // TextMeshProUGUI for displaying the total score

    
    // Method to record the score after a roll
    public void RecordRoll(int pins)
    {
        scores[currentRoll] = pins;
        currentRoll++;

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

        // Update the total score and frame scores
        for (int frame = 1; frame <= 10; frame++)
        {
            int frameScore = CalculateFrameScore(rollIndex);
            totalScore += frameScore;

            // Update the frame score UI (frameScoreText is an array of 10 TextMeshProUGUI objects)
            if (frameScoreText != null && frameScoreText.Length >= 10)
            {
                if (IsStrike(rollIndex))
                {
                    frameScoreText[frame - 1].text = "X";  // Display "Strike" for strikes
                }
                else if (IsSpare(rollIndex))
                {
                    frameScoreText[frame - 1].text = "/";   // Display "Spare" for spares
                }
                else
                {
                    frameScoreText[frame - 1].text = frameScore.ToString();  // Display normal score
                }
            }

            rollIndex += IsStrike(rollIndex) ? 1 : 2; // Strike takes 1 roll, spare and normal take 2 rolls
        }

        // Update the total score UI
        if (totalScoreText != null)
        {
            totalScoreText.text = "Total Score: " + totalScore.ToString();
        }
    }

    private int CalculateFrameScore(int rollIndex)
    {
        if (IsStrike(rollIndex))
        {
            return 10 + StrikeBonus(rollIndex);
        }
        else if (IsSpare(rollIndex))
        {
            return 10 + SpareBonus(rollIndex);
        }
        else
        {
            return scores[rollIndex] + scores[rollIndex + 1];
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

    public int GetTotalScore()
    {
        return totalScore;
    }

    public bool IsGameOver()
    {
        return currentFrame > 10;
    }

    // Reset the score-related variables when restarting the game
    public void ResetScore()
    {
        scores = new int[21]; // Reset the scores array
        currentRoll = 0; // Reset the current roll index
        currentFrame = 1; // Reset to the first frame
        totalScore = 0; // Reset the total score

        // Clear the frame score UI (optional)
        if (frameScoreText != null)
        {
            foreach (var frameText in frameScoreText)
            {
                frameText.text = ""; // Clear the text
            }
        }

        // Reset the total score UI (optional)
        if (totalScoreText != null)
        {
            totalScoreText.text = "Total Score: 0";  // Reset total score display
        }
    }
}

