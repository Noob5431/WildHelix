using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class finisshmenu : MonoBehaviour
{

    [SerializeField] Text thisTime;
    [SerializeField] Text bestTime;
    gamemanager manager;
    float currtime;
    float time;
    public GameObject finishUI;
    public GameObject timer;
    public bool LevelOver = false;
    Pausemenu pause;
    Pausemenu strt;
    public bool isFinished=false;
    int currentScene;

    private void Start()
    {
        manager = GameObject.Find("gameManager").GetComponent<gamemanager>();
        if(SceneManager.GetActiveScene().buildIndex > 2)
            currentScene = SceneManager.GetActiveScene().buildIndex - 3;
        
    }
    void Update()
    {
        if (isFinished)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            LevelOver = true;
            Object.Destroy(timer);
            finishUI.SetActive(true);
            currtime = gameObject.GetComponent<Timer>().currentTime;
            if (currtime<manager.highScore[currentScene] || manager.highScore[currentScene] ==-1)
            {
                manager.highScore[currentScene] = currtime;
            }
            Time.timeScale = 0f;
        }
       /* else if(Input.GetKeyDown(KeyCode.F))
        {
            LevelOver = true;
            Object.Destroy(timer);
            finishUI.SetActive(true);
            currtime = gameObject.GetComponent<Timer>().currentTime;
            if (currtime < manager.highScore[currentScene] || manager.highScore[currentScene] == -1)
            {
                manager.highScore[currentScene] = currtime;
            }
        }*/
       
        thisTime.text = currtime.ToString("0.0");
        //gameObject.GetComponent<Timer>().currentTime.ToString("0.0");
        bestTime.text = manager.highScore[currentScene].ToString("0.0");
    }
    public void rs()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }
    public void Sl()
    {
        SceneManager.LoadScene(2);
    }



}
