using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    gamemanager manager;
    [SerializeField] Text[] levels;
    int currentScene;
    private void Start()
    {
        manager = GameObject.Find("gameManager").GetComponent<gamemanager>();
        for (int currentScene = 0; currentScene < 3; currentScene++)
        {
            if (manager.highScore[currentScene] == -1)
            {
                levels[currentScene].text = "None";
            }
            else
                levels[currentScene].text = manager.highScore[currentScene].ToString("0.0");
        }
    }
    public void Playgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
