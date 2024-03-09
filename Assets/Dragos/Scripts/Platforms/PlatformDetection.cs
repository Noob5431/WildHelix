using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetection : MonoBehaviour
{
    public KeyCode keyToUse = KeyCode.Mouse0;
    public float maxGrabDistance = 10f;
    public float scrollSensivity = 3f;

    private MovingPlatform mGrabbedPlatform = null;
    private ScrollController mScrollController;

    void Start()
    {
        mScrollController = new ScrollController();
        mScrollController.sensitivity = scrollSensivity;
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

        mScrollController.Update(mGrabbedPlatform);
        mGrabbedPlatform.UpdatePlatform(Camera.main.transform.position, Camera.main.transform.forward);
    }

    private void _tryFindPlatform()
    {
        RaycastHit hit;
        Transform playerOrigin = Camera.main.transform;

        if (Physics.Raycast(playerOrigin.position, playerOrigin.forward, out hit, maxGrabDistance, int.MaxValue, QueryTriggerInteraction.Ignore))
        {
            GameObject objectHit = hit.transform.gameObject;
            mGrabbedPlatform = objectHit.GetComponent<MovingPlatform>();
            if (mGrabbedPlatform != null)
            {
                mGrabbedPlatform.GrabPlatform(hit.distance, hit.point);
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
