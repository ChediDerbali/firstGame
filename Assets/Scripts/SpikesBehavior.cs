using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpikesBehavior : MonoBehaviour {

    public string tag ="Player";
    public Text t,c;
    float time;
    int count;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetFloat("time", time);
            SceneManager.LoadScene("gameOver");
        }
    }
}
