using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Canvas exitMenu;
    public Button playButton;
    public Button exitButton;

    void Start() {
        exitMenu = exitMenu.GetComponent<Canvas>();
        playButton = playButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        exitMenu.enabled = false;
    }

    public void ExitPress() {
        exitMenu.enabled = true;
        playButton.enabled = false;
        exitButton.enabled = false;
    }

    public void NoPress() {
        exitMenu.enabled = false;
        playButton.enabled = true;
        exitButton.enabled = true;
    }

    public void StartLevel() {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        Application.Quit();
    }

}