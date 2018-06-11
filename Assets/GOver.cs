using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GOver : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
        float time = PlayerPrefs.GetFloat("time");
        int coins = PlayerPrefs.GetInt("coin");
        float total = time * coins;
        GetComponent<Text>().text = "Game Over\n" + total.ToString("00.00");
        if (PlayerPrefs.GetFloat("bestScore") < total)
            PlayerPrefs.SetFloat("bestScore",total);
        GetComponent<Text>().text = GetComponent<Text>().text + "\nBest Score:\n" + PlayerPrefs.GetFloat("bestScore").ToString("00.00");



    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("parcour");
    }
}
