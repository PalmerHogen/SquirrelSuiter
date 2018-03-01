using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour {
    public float PrevHigh;
    public Text HighScore1;
    public Text HighScore2;
    public Text HighScore3;
    public Text HighScore4;
    public Text HighScore5;

    // Use this for initialization
    void Start () {
        PrevHigh = PlayerPrefs.GetFloat("HighScore1", 0);
        HighScore1.text = PrevHigh.ToString();
        PrevHigh = PlayerPrefs.GetFloat("HighScore2", 0);
        HighScore2.text = PrevHigh.ToString();
        PrevHigh = PlayerPrefs.GetFloat("HighScore3", 0);
        HighScore3.text = PrevHigh.ToString();
        PrevHigh = PlayerPrefs.GetFloat("HighScore4", 0);
        HighScore4.text = PrevHigh.ToString();
        PrevHigh = PlayerPrefs.GetFloat("HighScore5", 0);
        HighScore5.text = PrevHigh.ToString();
    }
	
	// Update is called once per frame
}
