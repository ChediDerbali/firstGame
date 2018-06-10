using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    float offset;

	// Use this for initialization
	void Start () {
        offset = target.position.x;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        transform.position= new Vector3(target.position.x-offset, transform.position.y,transform.position.z);
        
    }
}
