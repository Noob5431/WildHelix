using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    [SerializeField]
    float FOV_min, FOV_max, speed_min, speed_max, increase_speed, decrease_time;
    float decrease_speed;
    [SerializeField]
    float FOV_current,speed_current,appliedFov=0;

    private void Start()
    {
        speed_min = GetComponent<Movement>().initial_velocity;
        speed_max = GetComponent<Movement>().final_velocity;
        decrease_speed = (FOV_max - FOV_min) / decrease_time;
    }

    private void Update()
    {
        /*speed_current = GetComponent<Rigidbody>().velocity.magnitude;
        FOV_current = (speed_current - speed_min) / (speed_max - speed_min) * (FOV_max - FOV_min) + FOV_min;
        FOV_current = Mathf.Clamp(FOV_current,FOV_min, FOV_max);
        if(appliedFov < FOV_current)
        {
            appliedFov += increase_speed * Time.deltaTime;
        }
        else 
        {
            appliedFov -= decrease_speed * Time.deltaTime;
        }
        appliedFov = Mathf.Clamp(appliedFov, FOV_min, FOV_max);
        GetComponentInChildren<Camera>().fieldOfView = appliedFov;*/
    }
}
