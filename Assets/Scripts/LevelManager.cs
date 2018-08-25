using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private List<Level> levels;
    

    private Level currentLevel;

    public int levelNumber = 0;

    float timeSinceLastSpawn = 0f;

    public Spawner enemySpawner;


    public Player[] players;

    bool isStopped;

    // Use this for initialization
    void Awake () {

        levels = new List<Level>();

        Level level1 = new Level();

        level1.numEnemiesToSpawn = 30;
        level1.enemySpeed = 8f;
        level1.rateOfSpawn = 1f;
        level1.playerSpeed = 20f;
        level1.enemyScore = 10;

        levels.Add(level1);



        Level level2 = new Level();
        level2.numEnemiesToSpawn = 30;
        level2.enemySpeed = 12f;
        level2.rateOfSpawn = 0.8f;
        level2.playerSpeed = 20f;
        level2.enemyScore = 15;
        levels.Add(level2);


        Level level3 = new Level();
        level3.numEnemiesToSpawn = 50;
        level3.enemySpeed = 14f;
        level3.rateOfSpawn = 0.5f;
        level3.playerSpeed = 25f;
        level3.enemyScore = 20;
        levels.Add(level3);


        Level level4 = new Level();
        level4.numEnemiesToSpawn = 70;
        level4.enemySpeed = 16f;
        level4.rateOfSpawn = 0.45f;
        level4.playerSpeed = 25f;
        level4.enemyScore = 25;
        levels.Add(level4);

        Level level5 = new Level();
        level5.numEnemiesToSpawn = 70;
        level5.enemySpeed = 18f;
        level5.rateOfSpawn = 0.45f;
        level5.playerSpeed = 30f;
        level5.enemyScore = 30;
        levels.Add(level5);



        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isStopped)
        {
            if (currentLevel.enemiesLeft > 0 && currentLevel.enemiesSpawned < currentLevel.numEnemiesToSpawn)
            {
                if (timeSinceLastSpawn < currentLevel.rateOfSpawn)
                {
                    timeSinceLastSpawn += Time.deltaTime;
                }
                else
                {
                    enemySpawner.SpawnEnemy(currentLevel);
                    currentLevel.enemiesSpawned++;
                    timeSinceLastSpawn = 0f;
                }
            }
            else
            {
                levelNumber++;
                if (levelNumber < levels.Count)
                {
                    Begin(levels[levelNumber]);
                }
            }
        }
        
	}

    public void Begin(Level level)
    {
        this.currentLevel = level;
        currentLevel.enemiesLeft = currentLevel.numEnemiesToSpawn;
        currentLevel.enemiesSpawned = 0;
        GetComponent<GameManager>().enemyScore = currentLevel.enemyScore;

        foreach (Player player in players)
        {
            player.movementSpeed = level.playerSpeed;
        }

        isStopped = false;
    }

    public void Stop()
    {
        isStopped = true;
    }

    public void StartGame()
    {
        levelNumber = 0;
        Begin(levels[0]);
    }
}
