using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public GameObject PauseMenu;
    bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (!paused) {
                Pause();
            }
            else {
                Unpause();
            }
        }    
    }

    public void Pause() {
        PauseMenu.SetActive(true);
        paused =  true;
        Time.timeScale = 0;
    }

    public void Unpause() {
        PauseMenu.SetActive(false);
        paused =  false;
        Time.timeScale = 1;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void QuitToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
