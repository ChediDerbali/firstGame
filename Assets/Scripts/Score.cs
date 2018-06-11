using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    float time;

	// Use this for initialization
	void Start () {
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text>().text = Time.time.ToString("00.00");
        PlayerPrefs.SetFloat("time", Time.time);
    }
}
