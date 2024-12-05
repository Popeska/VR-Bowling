using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 


public class ScoreKeeper : MonoBehaviour
{
    private int[] scores = new int[21]; // Holds the score for up to 21 rolls (max rolls possible in a game)
    private Frame[] frames = new Frame[10];
    private int currentRoll = 1; // Keeps track of the current roll
    private int currentFrame = 0; // Keeps track of the current frame (1 to 10)
    private int totalScore = 0; // Total score of the game
    private int totalRolls = 0;

    public TextMeshProUGUI[] frameScoreText;  // An array of TextMeshProUGUI to display each frame's score
    public TextMeshProUGUI totalScoreText;    // TextMeshProUGUI for displaying the total score

    void Start()
    {
        // Initialize all frames
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = new Frame();
        }
    }

    // Method to record the score after a roll
    public void RecordRoll(int pins)
    {
        totalRolls++;
        if (currentFrame >= 10)
        {
            Debug.LogWarning("All frames are complete!");
            return;
        }


        if (currentRoll == 1)
        {
            frames[currentFrame].FirstRoll = pins;
            if (frames[currentFrame].IsStrike())
            {
                // Strike ends the frame immediately
                Debug.Log($"Frame {currentFrame + 1}: Strike!");
                advanceFrame();
            }
            else
            {
                currentRoll = 2; // Move to the second roll
            }
        }
        else if (currentRoll == 2)
        {
            frames[currentFrame].SecondRoll = pins;
            Debug.Log($"Frame {currentFrame + 1}: First Roll: {frames[currentFrame].FirstRoll}, Second Roll: {frames[currentFrame].SecondRoll}");
            advanceFrame();
        }

        UpdateScore();
    }

    private void advanceFrame()
    {
        frames[currentFrame].Total = calculateFrameScore(currentFrame);

        if(currentFrame > 0 && frames[currentFrame].IsStrike()){
            frames[currentFrame - 1].Total += frames[currentFrame].Total;
        }

        currentFrame++;
        currentRoll = 1; // Reset roll for the next frame
    }

    private int calculateFrameScore(int frameIndex)
    {
        Frame frame = frames[frameIndex];
        int score = frame.FirstRoll + frame.SecondRoll;

        // Handle strike and spare scoring for frames 1-9
        if (frameIndex < 9)
        {
            if (frame.IsStrike())
            {
                Frame nextFrame = frames[frameIndex + 1];
                score += nextFrame.FirstRoll;

                // Add second roll of the next frame unless it's also a strike, then add the first roll of the frame after
                if (nextFrame.IsStrike() && frameIndex + 1 < 9)
                {
                    score += frames[frameIndex + 2].FirstRoll;
                }
                else
                {
                    score += nextFrame.SecondRoll;
                }
            }
            else if (frame.IsSpare())
            {
                score += frames[frameIndex + 1].FirstRoll;
            }
        }

        return score;
    }


    private void UpdateScore()
    {
        totalScore = 0;
        int rollIndex = 0;


        // Update the total score and frame scores
        for (int frame = 0; frame < 10; frame++)
        {
            // Update the frame score UI (frameScoreText is an array of 10 TextMeshProUGUI objects)
            if (frameScoreText != null && frameScoreText.Length == 10)
            {
                if (frames[frame].IsStrike())
                {
                    frameScoreText[frame].text = "X";  // Display "Strike" for strikes
                }
                else if (frames[frame].IsSpare())
                {
                    frameScoreText[frame].text = "/";   // Display "Spare" for spares
                }
                else
                {
                    frameScoreText[frame].text = frames[frame].Total.ToString();  // Display normal score
                }
            }
            totalScore += frames[frame].Total;
        }

        // Update the total score UI
        if (totalScoreText != null)
        {
            totalScoreText.text = "Total Score: " + totalScore.ToString();
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
        int totalScore = 0;
        for (int i = 0; i <= currentFrame && i < 10; i++)
        {
            totalScore += frames[i].Total;
        }
        return totalScore;
    }

    public bool IsGameOver()
    {
        return currentFrame > 10;
    }

    public int getCurrentFrame(){
        return currentFrame;
    }

    public int getCurrentRoll(){
        return currentRoll;
    }
}

