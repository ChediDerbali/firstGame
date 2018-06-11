using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    float offsetX;
    float offsetY=2;
    Vector3 desiredPosition;

    // Use this for initialization
    void Start () {
        offsetX = target.position.x;
	}
	


    private void LateUpdate()
    {
        if (target.position.y <= 2)
            desiredPosition = new Vector3(target.position.x - offsetX, 0, transform.position.z);
        else
            desiredPosition = new Vector3(target.position.x - offsetX, target.position.y, transform.position.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.125f);
        transform.position = smoothedPosition;

    }
}
