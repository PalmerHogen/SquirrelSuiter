using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public Canvas pauseMenu;
    public CameraController camControl;
	public AudioClip musicLoop, pauseLoop;
    public Canvas gameOverCanvas;
    public ScoreText scoreScript;
    public Text gameOverScore;
    public Text gameOverAcorns;
    public Text newHighScore;

    private AudioSource Music; 
	private AudioSource[] atmosphere;
	private float musicTime;

	// Use this for initialization
	void Start () {
        pauseMenu.enabled = false;
		Music = GetComponent<AudioSource> ();
		atmosphere = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<AudioSource> ();
		Music.clip = musicLoop;
        gameOverCanvas.enabled = false;
        scoreScript = scoreScript.GetComponent<ScoreText>();
    }

    void Update() {
		if (!gameOverCanvas.enabled) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (pauseMenu.enabled) {
					pauseMenu.enabled = false;
					Time.timeScale = 1.0f;
					switchMusic ();
					int arraySize = atmosphere.Length;
					for (int a = 0; a < arraySize; a++) { 
						atmosphere [a].Play ();
					}

				} else {
					pauseMenu.enabled = true;
					Time.timeScale = 0;
					switchMusic ();
					int arraySize = atmosphere.Length;
					for (int a = 0; a < arraySize; a++) { 
						atmosphere [a].Stop ();
					}
				}
			}
		}
    }

    public void Die() {

		musicTime = Music.time;

		Music.Stop();

		Music.clip = pauseLoop;
		Music.time = musicTime;
		Music.Play();

        camControl.Shake();
        gameOverCanvas.enabled = true;
		//switchMusic();
        int acorns = scoreScript.AcornNum;
        int score = scoreScript.ScoreNum-1;
        gameOverAcorns.text = "Acorns: " + acorns.ToString();
        gameOverScore.text = "Score: " + score.ToString();

        if (scoreScript.PrevHigh > score) {
            print(scoreScript.PrevHigh);
            print(score);
            newHighScore.enabled = false;
        }
    }

	public void switchMusic(){
		if (Music.clip == pauseLoop) {
			musicTime = Music.time;
			Music.Stop();



			Music.clip = musicLoop;
			Music.time = musicTime;
			Music.Play();
		} else {
			musicTime = Music.time;

			Music.Stop();

			Music.clip = pauseLoop;
			Music.time = musicTime;
			Music.Play();
		}
	}
}
