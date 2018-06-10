using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehavior : MonoBehaviour {

    public string tag ="Player";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == tag)
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10 + Vector2.left;
            ;
    }
}
