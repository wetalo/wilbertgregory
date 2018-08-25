using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int numObjectsMissed;
    public int numObjectsGotten;
    public int numMistakes;

    public DebugText gameManagerText;

    public float health = 100f;

    public float missedObjectDamage = 4f;
    public float gotObjectHeal = 1f;
    public float mistakeDamage = 15f;

    public float maxHealth = 100f;

    public Slider healthBar;
    
    public Text scoreText;
    public Text multiplierText;

    public float score;

    int scoreMultiplier = 1;
    int numGottenInARow = 0;
    public int enemyScore;

    public int scoreMultiplierAmount1 = 10;
    public int scoreMultiplierAmount2 = 20;
    public int scoreMultiplierAmount3 = 35;

    bool recordScore = true;

    public Canvas gameCanvas;
    public Canvas gameOverCanvas;

    public GameOver gameOverManager;

    public Player[] players;

    public bool isInGameOverState = false;

    // Use this for initialization
    void Start () {
        StartGame();
    }
	
	// Update is called once per frame
	void Update () {
        string text = "Objects Missed: " + numObjectsMissed + "\n"
                        + "Objects Gotten: " + numObjectsGotten + "\n"
                        + "Mistakes made: " + numMistakes;
        if(gameManagerText)
        gameManagerText.ChangeText(text);
        UpdateHealthBar();


    }

    public void MissedObject()
    {
        numObjectsMissed++;
        
        health -= missedObjectDamage;
        if (health < 0)
        {
            health = 0;
            recordScore = false;
            if (!isInGameOverState)
            {
                GameOver();
            }
        }

        numGottenInARow = 0;

    }

    public void GotObject()
    {
        numObjectsGotten++;
        health += gotObjectHeal;
        if (health > 100)
        {
            health = 100;
        }

        numGottenInARow++;
        CalculateScore();
    }

    public void MistakeMade()
    {
        numMistakes++;
        health -= mistakeDamage;
        if (health < 0)
        {
            health = 0;
            recordScore = false;
            if (!isInGameOverState)
            {
                GameOver();
            }
        }
        numGottenInARow = 0;
    }

    public void UpdateHealthBar()
    {
        healthBar.value = health / maxHealth;
    }

    void CalculateScore()
    {
        if (recordScore)
        {
            scoreMultiplier = 1;
            if (numGottenInARow > scoreMultiplierAmount1)
            {
                scoreMultiplier = 2;
            }
            if (numGottenInARow > scoreMultiplierAmount2)
            {
                scoreMultiplier = 3;
            }
            if (numGottenInARow > scoreMultiplierAmount3)
            {
                scoreMultiplier = 4;
            }

            score += enemyScore * scoreMultiplier;
            UpdateScoreText();
        }

    }

    void UpdateScoreText()
    {
        multiplierText.text = "x" + scoreMultiplier;
        scoreText.text = "Score: " + FormatScoreText();
    }

    string FormatScoreText()
    {
        string scoreString = "" + score;

        if (scoreString.Length > 3)
        {
            scoreString = scoreString.Insert(scoreString.Length - 3, ",");
        }

        int maxLength = 7;
        int scoreLength = scoreString.Length;

        for (int i = scoreLength; i < maxLength; i++)
        {
            scoreString = " " + scoreString;
        }

        return scoreString;
    }

    void GameOver()
    {
        gameCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        isInGameOverState = true;

        gameOverManager.SetPlayerScore(score);
        gameOverManager.DoEndGame();
        GetComponent<LevelManager>().Stop();
    }

    public void StartGame()
    {
        GetComponent<LevelManager>().StartGame();
        gameCanvas.enabled = true;
        gameOverCanvas.enabled = false;
        isInGameOverState = false;

        recordScore = true;

        health = maxHealth;
        score = 0;
        UpdateScoreText();

        numObjectsGotten = 0;
        numObjectsMissed = 0;
        numMistakes = 0;

        foreach(Player player in players)
        {
            player.mistakesMade = 0;
            player.objectsGotten = 0;
            player.UpdateText();
        }

    }
}
