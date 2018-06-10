using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{



    public string BackgroundTag = "Background";
    public bool CorrectBackgroundPositions = false;
    public BackgroundScrollDirection Direction;


    #region Requires Intermediate CSharp Skills
    public static bool BackgroundsSorted = false;
    public static object bgLock = new object();

    [HideInInspector]
    public int BackgroundIndex = 0;
    [HideInInspector]
    public int BackgroundsCount = 0;
    [HideInInspector]
    public List<GameObject> Backgrounds;
    private void Awake()
    {
        lock (bgLock)
        {
            if (!BackgroundsSorted)
            {

                IEnumerable<GameObject> backgrounds = null;

                if (Direction == BackgroundScrollDirection.Right)
                    backgrounds = GameObject.FindGameObjectsWithTag(BackgroundTag).Where(x => x.GetComponent<BackgroundScroller>() != null).OrderBy(x => x.transform.position.x);
                else if (Direction == BackgroundScrollDirection.Left)
                    backgrounds = GameObject.FindGameObjectsWithTag(BackgroundTag).Where(x => x.GetComponent<BackgroundScroller>() != null).OrderByDescending(x => x.transform.position.x);
                else if (Direction == BackgroundScrollDirection.Up)
                    backgrounds = GameObject.FindGameObjectsWithTag(BackgroundTag).Where(x => x.GetComponent<BackgroundScroller>() != null).OrderBy(x => x.transform.position.y);
                else
                    backgrounds = GameObject.FindGameObjectsWithTag(BackgroundTag).Where(x => x.GetComponent<BackgroundScroller>() != null).OrderByDescending(x => x.transform.position.y);




                int index = 0;
                foreach (var bg in backgrounds)
                {
                    bg.GetComponent<BackgroundScroller>().BackgroundsCount = backgrounds.Count();
                    bg.GetComponent<BackgroundScroller>().BackgroundIndex = index++;
                }
                Backgrounds = backgrounds.ToList();
                if (CorrectBackgroundPositions)
                    FixBackgroundPositions(Backgrounds);
            }
        }
    }
    void FixBackgroundPositions(List<GameObject> backgrounds)
    {

        for (int i = 1; i < backgrounds.Count(); i++)
        {
            var lastBackgroundBounds = backgrounds[i - 1].GetComponent<Renderer>().bounds;
            var currentBackgroundBounds = backgrounds[i].GetComponent<Renderer>().bounds;
            // move after last
            if (Direction == BackgroundScrollDirection.Right)
                backgrounds[i].transform.position = new Vector3(lastBackgroundBounds.max.x + currentBackgroundBounds.size.x / 2, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            else if (Direction == BackgroundScrollDirection.Up)
                backgrounds[i].transform.position = new Vector3(backgrounds[i].transform.position.x, lastBackgroundBounds.max.y + currentBackgroundBounds.size.y / 2, backgrounds[i].transform.position.z);
            else if (Direction == BackgroundScrollDirection.Left)
                backgrounds[i].transform.position = new Vector3(lastBackgroundBounds.min.x - currentBackgroundBounds.size.x / 2, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            else
                backgrounds[i].transform.position = new Vector3(backgrounds[i].transform.position.x, lastBackgroundBounds.min.y - currentBackgroundBounds.size.y / 2, backgrounds[i].transform.position.z);


        }

    }
    #endregion


    /// <summary>
    /// Scrolls current background & fix positions
    /// </summary>
    void ScrollCurrentAndFixPositions()
    {
        // gets the last background index 
        int lastIndex = (BackgroundIndex - 1) % BackgroundsCount;
        // if the last index was negative means that the background in question is in the maximum boundary of the interval (we should use modulo operator)
        if (lastIndex < 0)
            lastIndex = BackgroundsCount + (lastIndex % BackgroundsCount);

        // gets the background associated with the lastIndex
        var lastBg = Backgrounds.Where(x => x.GetComponent<BackgroundScroller>().BackgroundIndex == lastIndex).FirstOrDefault();
        // gets its bounds
        var lastBackgroundBounds = lastBg.GetComponent<Renderer>().bounds;
        // gets the current background bounds
        var currentBackgroundBounds = GetComponent<Renderer>().bounds;

        //  We scroll the backgrounds according to the movement direction
        if (Direction == BackgroundScrollDirection.Right)
            transform.position = new Vector3(lastBackgroundBounds.max.x + currentBackgroundBounds.size.x / 2, transform.position.y, transform.position.z);
        else if (Direction == BackgroundScrollDirection.Left)
            transform.position = new Vector3(lastBackgroundBounds.min.x - currentBackgroundBounds.size.x / 2, transform.position.y, transform.position.z);
        else if (Direction == BackgroundScrollDirection.Up)
            transform.position = new Vector3(transform.position.x, lastBackgroundBounds.max.y + currentBackgroundBounds.size.y / 2, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, lastBackgroundBounds.min.y - currentBackgroundBounds.size.y / 2, transform.position.z);


    }


    // Update is called once per frame
    void Update()
    {
        // if the background is hidden, scroll and fix
        if (IsHidden())
            ScrollCurrentAndFixPositions();
    }


    // Checks if the background is hidden
    bool IsHidden()
    {
        // check if the background is hidden based on the movement direction
        if (Direction == BackgroundScrollDirection.Right)
            return IsLeftOfCamera(gameObject);
        else if (Direction == BackgroundScrollDirection.Left)
            return IsRightOfCamera(gameObject);
        else if (Direction == BackgroundScrollDirection.Up)
            return IsUnderCamera(gameObject);
        else
            return IsOnTopOfCamera(gameObject);
    }




    #region Camera Utils

    /// <summary>
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
    public static bool IsLeftOfCamera(GameObject obj)
    {
        var cb = GetOrthographicBounds(Camera.main);
        return (cb.min.x > (obj.transform.position.x + obj.GetComponent<Renderer>().bounds.size.x / 2));
    }
    public static bool IsRightOfCamera(GameObject obj)
    {
        var cb = GetOrthographicBounds(Camera.main);

        return (cb.max.x < (obj.transform.position.x - obj.GetComponent<Renderer>().bounds.size.x / 2));
    }
    public static bool IsOnTopOfCamera(GameObject obj)
    {
        var cb = GetOrthographicBounds(Camera.main);

        return (cb.max.y < (obj.transform.position.y - obj.GetComponent<Renderer>().bounds.size.y / 2));
    }
    public static bool IsUnderCamera(GameObject obj)
    {
        var cb = GetOrthographicBounds(Camera.main);

        return (cb.min.y > (obj.transform.position.y + obj.GetComponent<Renderer>().bounds.size.y / 2));
    }
    #endregion
}
public enum BackgroundScrollDirection
{
    Right,
    Left,
    Up,
    Down
}