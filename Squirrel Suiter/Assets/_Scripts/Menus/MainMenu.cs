using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Canvas exitMenu;
    public Canvas levelSelect;
    public Canvas tutorial1;
    public Canvas tutorial2;

    public Button playButton;
    public Button exitButton;
    public Button tutorialButton;
    public Button tutorialExit1;
    public Button tutorialExit2;

    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button levelSelectExit;

    void Start() {
        exitMenu = exitMenu.GetComponent<Canvas>();
        levelSelect = levelSelect.GetComponent<Canvas>();
        tutorial1 = tutorial1.GetComponent<Canvas>();
        tutorial2 = tutorial2.GetComponent<Canvas>();

        playButton = playButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        tutorialButton = tutorialButton.GetComponent<Button>();
        tutorialExit1 = tutorialExit1.GetComponent<Button>();
        tutorialExit2 = tutorialExit2.GetComponent<Button>();

        tutorial1.enabled = false;
        tutorial2.enabled = false;
        exitMenu.enabled = false;
        levelSelect.enabled = false;

        level1 = level1.GetComponent<Button>();
        level2 = level2.GetComponent<Button>();
        level3 = level3.GetComponent<Button>();
        level4 = level4.GetComponent<Button>();
        level5 = level5.GetComponent<Button>();
        levelSelectExit = levelSelectExit.GetComponent<Button>();
    }

    public void ExitPress() {
        exitMenu.enabled = true;
        playButton.enabled = false;
        exitButton.enabled = false;
        tutorialButton.enabled = false;
    }

    public void NoPress() {
        exitMenu.enabled = false;
        playButton.enabled = true;
        exitButton.enabled = true;
        tutorialButton.enabled = true;
    }

    public void StartLevel() {
        levelSelect.enabled = true;
        playButton.enabled = false;
        exitButton.enabled = false;
        tutorialButton.enabled = false;
    }

    public void TutorialPress() {
        tutorial1.enabled = true;
        playButton.enabled = false;
        exitButton.enabled = false;
        tutorialButton.enabled = false;
    }

    public void TutorialNext() {
        tutorial1.enabled = false;
        tutorial2.enabled = true;
    }

    public void TutorialExit() {
        tutorial1.enabled = false;
        tutorial2.enabled = false;
        playButton.enabled = true;
        exitButton.enabled = true;
        tutorialButton.enabled = true;
    }

    public void level1Press() {
        SceneManager.LoadScene(1);
    }
    public void level2Press() {
        SceneManager.LoadScene(2);
    }
    public void level3Press() {
        SceneManager.LoadScene(3);
    }
    public void level4Press() {
        SceneManager.LoadScene(4);
    }
    public void level5Press() {
        SceneManager.LoadScene(5);
    }

    public void exitLevelSelect() {
        levelSelect.enabled = false;
        playButton.enabled = true;
        exitButton.enabled = true;
        tutorialButton.enabled = true;
    }

    public void ExitGame() {
        Application.Quit();
    }

}