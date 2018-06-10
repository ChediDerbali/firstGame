using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBehavior : MonoBehaviour {

    public string targetTag = "Player";

    public float force;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force) ;

        }
    }
}
