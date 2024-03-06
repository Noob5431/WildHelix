using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    float lookSpeed;
    [SerializeField]
    GameObject playerCamera;
    gamemanager manager;

    Vector2 mouseDelta;
    public Vector2 lookRotation;

    private void Start()
    {
        manager = GameObject.Find("gameManager").GetComponent<gamemanager>();
        lookSpeed = manager.mouseSensitivity * lookSpeed;
        lookRotation = transform.localEulerAngles;
    }

    private void Update()
    {
        if (Time.timeScale > 0.01)
        {
            mouseDelta = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
            lookRotation += mouseDelta * lookSpeed * Time.deltaTime;
            lookRotation.x = Mathf.Clamp(lookRotation.x, -90, 90);

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, lookRotation.y, transform.localEulerAngles.z);
            playerCamera.transform.localEulerAngles = new Vector3(lookRotation.x, playerCamera.transform.localEulerAngles.y,
                                                                    playerCamera.transform.localEulerAngles.z);
        }
    }

    
}
