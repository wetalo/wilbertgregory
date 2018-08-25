using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public int numEnemiesToSpawn;
    public float enemySpeed;
    public float rateOfSpawn;


    public int enemiesLeft;
    public int enemiesSpawned = 0;

    public float playerSpeed;

    public int enemyScore;



	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void EnemyDefeated()
    {
        enemiesLeft--;
    }

    public void EnemySpawned()
    {
        enemiesSpawned++;
    }
}
