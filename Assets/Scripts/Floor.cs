using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public GameManager gameManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TouchedFloor(Enemy enemy)
    {
        enemy.Defeated();
        gameManager.MissedObject();
    }
}
