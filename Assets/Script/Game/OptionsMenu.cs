using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deadMenu;
    public GameObject winMenu;
    bool paused = false;
    
    public static OptionsMenu Instance;


    void Awake() {
        Instance = this;
    }
    
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

    public void DeadMenu() {
        deadMenu.SetActive(true);
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        paused =  true;
        Time.timeScale = 0;
    }

    public void Unpause() {
        pauseMenu.SetActive(false);
        paused =  false;
        Time.timeScale = 1;
    }

    public void Win() {
        winMenu.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        
    }

    public void QuitToMenu() {
        SceneManager.LoadScene("MainManu");
        Time.timeScale = 1;
    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("Bye");
    }
    
    public void LoadLevelByName(string levelName) {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1;
    }
}
