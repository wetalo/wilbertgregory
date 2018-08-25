using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float minZ;
    public float maxZ;

    public GameObject[] enemyTypes;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEnemy(Level currentLevel)
    {
        float yPos = transform.position.y;
        float xPos = transform.position.x;
        float zPos = Random.Range(minZ, maxZ);

        GameObject enemyType = enemyTypes[Random.Range((int)0, (int)enemyTypes.Length)];
        GameObject enemy = Instantiate(enemyType, new Vector3(xPos,yPos,zPos), enemyType.transform.rotation);

        enemy.GetComponent<Enemy>().SetSpeed(currentLevel.enemySpeed);
        enemy.GetComponent<Enemy>().parentLevel = currentLevel;
        
    }
}
