using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetection : MonoBehaviour
{
    public KeyCode keyToUse = KeyCode.Mouse0;
    public float maxGrabDistance = 10f;

    private MovingPlatform mGrabbedPlatform = null;

    void Start()
    {
    }

    private void _ungrabPlatform()
    {
        if (mGrabbedPlatform != null) {
            mGrabbedPlatform.FreezePlatform();
            mGrabbedPlatform = null;
        }
    }

    private void _updatemGrabbedPlatform()
    {
        if (mGrabbedPlatform == null)
            return;

        mGrabbedPlatform.UpdatePlatform(Camera.main.transform.position, Camera.main.transform.forward);
    }

    private void _tryFindPlatform()
    {
        RaycastHit hit;
        Transform playerOrigin = Camera.main.transform;

        if (Physics.Raycast(playerOrigin.position, playerOrigin.forward * maxGrabDistance, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;
            mGrabbedPlatform = objectHit.GetComponent<MovingPlatform>();
            if (mGrabbedPlatform != null)
            {
                mGrabbedPlatform.GrabPlatform(hit.distance);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyToUse))
        {
            if (mGrabbedPlatform != null)
            {
                _updatemGrabbedPlatform();
            }
            else
            {
                _tryFindPlatform();
            }
        }
        else
        {
            _ungrabPlatform();
        }
    }
}
