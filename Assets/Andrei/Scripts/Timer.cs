using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float currentTime = 1f;
    float startingTime = 0f;
    [SerializeField] Text countdownText;

    void start()
    {
        Time.timeScale = 0;
        currentTime = startingTime;
    }
    void Update()
    {
        if (gameObject.GetComponent<finisshmenu>().LevelOver == false)
        {
            currentTime += Time.deltaTime;
            countdownText.text = currentTime.ToString("0.0");
        }
    }
}
