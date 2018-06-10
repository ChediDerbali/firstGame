using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsBehindCamera : MonoBehaviour {

    private Bounds bounds;
    // Use this for initialization
    void Start()
    {
        bounds = GetComponent<Renderer>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBehindCamera(gameObject, bounds))
            Destroy(gameObject);
    }


    public static bool IsBehindCamera(GameObject obj, Bounds objectBounds)
    {
        var cb = GetOrthographicBounds(Camera.main);
        return (cb.min.x > (obj.transform.position.x + objectBounds.size.x / 2));
    }   /// <summary>
        /// Gets orthographic camera bounds
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
    public static Bounds GetOrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}
