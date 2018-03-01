using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Button mainButton;
    public Button restartButton;

    void Start() {
        mainButton = mainButton.GetComponent<Button>();
        restartButton = restartButton.GetComponent<Button>();
        mainButton.enabled = true;
        restartButton.enabled = true;
    }

    public void ReturnPress() {
        SceneManager.LoadScene(0);
    }

    public void RestartPress() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}