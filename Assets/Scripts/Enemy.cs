using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum EnemyType
    {
        cubeEnemy,
        sphereEnemy,
        pyramidEnemy
    }

    public EnemyType enemyType;

    string sphericalPlayer = "Player2";
    string cubicPlayer = "Player1";

    string samePlayerType;
    string otherPlayerType;

    public float speed;

    Rigidbody rigidbody;

    public Level parentLevel;

	// Use this for initialization
	void Start () {
        if (enemyType == EnemyType.cubeEnemy)
        {
            samePlayerType = cubicPlayer;
            otherPlayerType = sphericalPlayer;
        }
        else if (enemyType == EnemyType.sphereEnemy)
        {
            samePlayerType = sphericalPlayer;
            otherPlayerType = cubicPlayer;
        }
        else if (enemyType == EnemyType.pyramidEnemy)
        {

        }

        rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(transform.TransformDirection(Vector3.down) * speed, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSpeed(float speed)
    {
        this.speed = speed;
        if(rigidbody!=null)
        rigidbody.AddForce((Vector3.down) * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(other.name == samePlayerType)
            {
                other.SendMessage("TouchedSameType",this);
            } else if(other.name == otherPlayerType)
            {
                other.SendMessage("TouchedOtherType", this);
            }
            
        } else if (other.tag == "Floor")
        {
            other.SendMessage("TouchedFloor",this);
        }
    }

    public void Defeated()
    {
        parentLevel.EnemyDefeated();
        Destroy(gameObject);
    }
}
