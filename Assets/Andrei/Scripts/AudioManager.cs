using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource land,running,jump,wind,laser,music;
    bool isGrounded = true, oldIsGrounded = true;
    [SerializeField]
    float windVolumeTransitionTime;
    float initialWindVolume, appliedWindVolume,windVolumeEffect,targetVolume;

    gamemanager manager;

    private void Start()
    {
        manager = GameObject.Find("gameManager").GetComponent<gamemanager>();
        wind.volume = manager.effectVolume * wind.volume;
        running.volume = manager.effectVolume * running.volume;
        jump.volume = manager.effectVolume * jump.volume;
        laser.volume = manager.effectVolume * laser.volume;
        land.volume = manager.effectVolume * land.volume;
        music.volume = manager.musicVolume * music.volume;

        initialWindVolume = wind.volume;
        wind.volume = 0;
        wind.Play();
        windVolumeEffect = initialWindVolume / windVolumeTransitionTime;
    }

    private void Update()
    {
        oldIsGrounded = isGrounded;
        isGrounded = GetComponentInParent<Movement>().isGrounded;
        if(isGrounded && !oldIsGrounded)
        {
            land.Play();
        }
        if ((GetComponentInParent<Movement>().isRunning && isGrounded) || GetComponentInParent<Movement>().isWallRunning)
        {
            if(!running.isPlaying)
                running.Play();
        }
        else running.Stop();
        if (gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude > GetComponentInParent<Movement>().final_velocity + 0.5f)
        {
                targetVolume = initialWindVolume;
        }
        else
        {
            targetVolume = 0;
        }
        if(appliedWindVolume < targetVolume)
        {
            appliedWindVolume += windVolumeEffect * Time.deltaTime;
        }
        else if(appliedWindVolume > targetVolume)
        {
            appliedWindVolume -= windVolumeEffect * Time.deltaTime;
        }
        wind.volume = appliedWindVolume;
    }

    public void Jump()
    {
        if(!jump.isPlaying)
        {
            jump.Play();
        }
    }
    public void Laser()
    {
        if(!laser.isPlaying)
        {
            laser.Play();
        }
    }    
}
