using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public Button mainButton;
    public ScoreText scoreScript;

    void Start() {
        mainButton = mainButton.GetComponent<Button>();
        scoreScript = scoreScript.GetComponent<ScoreText>();
        //mainButton.enabled = true;
    }

    public void MainMenuPress() {
        SceneManager.LoadScene(0);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return) && scoreScript.playerDead) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
