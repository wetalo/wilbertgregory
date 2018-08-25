using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEffect : MonoBehaviour {

    public Rigidbody rb;

	void FixedUpdate () {
        rb.AddForce(0, 1, 1);
    }
}
