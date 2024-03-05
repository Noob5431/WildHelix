using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public GameObject startText;

     public  bool GameIsPaused = false;
     public  bool GameHasStarted = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (GameHasStarted == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startText.SetActive(false);
                Time.timeScale = 1f;
                GameHasStarted = true;
            }
        }



        if (GameHasStarted == true && gameObject.GetComponent<finisshmenu>().LevelOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (GameIsPaused)
                {
                    Resume();

                }
                else
                {
                    Pause();
                }
            }
        }
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale =1f;
        GameIsPaused =false; 
    }
    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale =0f;
        GameIsPaused =true; 
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
        GameIsPaused =false; 
        GameHasStarted = false;
        Time.timeScale =1f;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameIsPaused =false; 
        GameHasStarted = false;
        Time.timeScale =1f;
    }
}
