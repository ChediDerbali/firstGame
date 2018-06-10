using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickControle : MonoBehaviour {

    public float speed = 1;
    public float force = 30000;
    public LayerMask ground;
    public int jumps = 2;
    private int j;

    // Use this for initialization
    void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
        
        bool onGround = GetComponent<Collider2D>().IsTouchingLayers(ground);
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

        if (onGround)
            j = jumps;
        if (Input.GetKeyDown(KeyCode.UpArrow) && j>0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
            j --;
        }
        if (Input.GetKey(KeyCode.DownArrow) && !onGround)
        {
            GetComponent<Rigidbody2D>().gravityScale = 20;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 7;
        }







    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Collect"))
        {
            Destroy(other.gameObject);
        }
    }



}

