using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text playerNameText;
    public InputField playerNameInput;
    public Button restartButton;

    HighScore highScore;

    List<HighScore> scores;

    public GameManager gameManager;
    public LevelManager levelManager;

    public GameObject highScoreLine;

    Vector3 originalHighScorePosition;

    List<GameObject> highScoreLines;

    public Text[] highScoreNameTexts;
    public Text[] highScoreScoreTexts;
    public Text[] highScoreDateTexts;
    

    private void Awake()
    {
        scores = new List<HighScore>();
        highScoreLines = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {
		playerNameInput.onEndEdit.AddListener(delegate { StorePlayerName(); });
        restartButton.onClick.AddListener(delegate { RestartGame(); });


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StorePlayerName()
    {
        string playerName = playerNameInput.text;
        highScore.playerName = playerName;
        scores.Add(highScore);
        ShowLeaderBoard();
    }

    public void SetPlayerScore(float score)
    {
        highScore = new HighScore();
        highScore.score = score;
    }

    public void DoEndGame()
    {
        playerNameText.enabled = true;
        playerNameInput.gameObject.SetActive(true);
        highScore.time = System.DateTime.Now;
        
        restartButton.gameObject.SetActive(false);
        foreach (GameObject highscoreline in highScoreLines)
        {
            Destroy(highscoreline);
        }

        for(int i=0; i<10; i++)
        {
            highScoreNameTexts[i].enabled = false;
            highScoreScoreTexts[i].enabled = false;
            highScoreDateTexts[i].enabled = false;
        }
    }

    void ShowLeaderBoard()
    {
        restartButton.gameObject.SetActive(true);
        playerNameText.enabled = false;
        playerNameInput.gameObject.SetActive(false);
        

        Vector3 currentHighScorePosition = originalHighScorePosition;
        scores.Sort();
        scores.Reverse();
        for(int i=0; i<10; i++)
        {
            HighScore currentScore;
            if(scores.Count > i)
            {
                currentScore = scores[i];
            } else
            {
                currentScore = null;
            }

           
            //highScoreLine.transform.parent = gameManager.gameOverCanvas.transform;
            if(currentScore != null)
            {
                highScoreNameTexts[i].text = currentScore.playerName;
                highScoreScoreTexts[i].text = FormatScoreText(currentScore.score);
                highScoreDateTexts[i].text = currentScore.time.ToString("yyyy/MM/dd HH:mm:ss");
            } else
            {
                highScoreNameTexts[i].text = "";
                highScoreScoreTexts[i].text = "";
                highScoreDateTexts[i].text = "";
            }

            highScoreNameTexts[i].enabled = true;
            highScoreScoreTexts[i].enabled = true;
            highScoreDateTexts[i].enabled = true;

            currentHighScorePosition = currentHighScorePosition - new Vector3(0, 23, 0);

        }
    }

    string FormatScoreText(float score)
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

    void RestartGame()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        gameManager.StartGame();
    }
}
