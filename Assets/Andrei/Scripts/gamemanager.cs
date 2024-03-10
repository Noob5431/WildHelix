using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public float musicVolume, effectVolume, mouseSensitivity;
    Slider musicSlider, effectSlider, mouseSensitivitySlider;
    bool firstTime = true;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            AudioKeeper keeper = null;
            if (GameObject.Find("AudioSlideKeeper"))
                keeper = GameObject.Find("AudioSlideKeeper").GetComponent<AudioKeeper>();
            if (keeper)
            {
                musicSlider = keeper.music;
                effectSlider = keeper.effects;
                mouseSensitivitySlider = keeper.mouse;
            }
            if (musicSlider)
            {
                musicVolume = musicSlider.value;
            }
            if (effectSlider)
            {
                effectVolume = effectSlider.value;
            }
            if (mouseSensitivitySlider)
            {
                mouseSensitivity = mouseSensitivitySlider.value;
            }
        }
    }

    void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && firstTime == false)
        {
            AudioKeeper keeper = GameObject.Find("AudioSlideKeeper").GetComponent<AudioKeeper>();
            if (keeper)
            {
                musicSlider = keeper.music;
                effectSlider = keeper.effects;
                mouseSensitivitySlider = keeper.mouse;
            }
            if (musicSlider)
            {
                musicSlider.value = musicVolume;
            }
            if (effectSlider)
            {
                effectSlider.value = effectVolume;
            }
            if (mouseSensitivitySlider)
            {
                mouseSensitivitySlider.value = mouseSensitivity;
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            firstTime = false;
        }
    }

}
