using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OpeningScene : MonoBehaviour {

    public Vector3 position1;
    public Vector3 position2;
    public Vector3 position3;

    Vector3 currentPosition;
    Vector3 targetPosition;

    float startTime;
    float journeyLength;
    public float speed = 2f;
	// Use this for initialization
	void Start () {
        currentPosition = position1;
        targetPosition = position1;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("joystick 1 button 0") || Input.GetKeyDown("joystick 2 button 0"))
        {
            GoToNextScene();
        }

        if(targetPosition != currentPosition)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(currentPosition, targetPosition, fracJourney);

            if(Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                currentPosition = targetPosition;
            }
        }

        
	}

    void GoToNextScene()
    {
        if(currentPosition == position1)
        {
            targetPosition = position2;
            startTime = Time.time;
            journeyLength = Vector3.Distance(currentPosition, targetPosition);
        } else if(currentPosition == position2)
        {
            targetPosition = position3;
            startTime = Time.time;
            journeyLength = Vector3.Distance(currentPosition, targetPosition);
        } else if(currentPosition == position3)
        {
            SceneManager.LoadScene("Scene1");
        }
    }
}
