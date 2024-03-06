using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool lockZ = true;

    private float mLockedY = 0f;
    private float mDistance = 0f;
    private bool mIsGrabbed = false;
    private bool mHittingObstascle = false;
    private Vector3 mObstacleLoc = Vector3.zero;

    private void _checkForObstacles(Vector3 grabberPosition, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(grabberPosition, direction, out hit, mDistance))
        {
            Debug.Log("Platform cannot be placed, hitting: " + hit.transform.gameObject.name + " at dist " + hit.distance + " max dist: " + mDistance);
            mHittingObstascle = true;
            mObstacleLoc = hit.point;
        }
        else
        {
            mHittingObstascle = false;
            mObstacleLoc = Vector3.zero;
        }
    }

    public void UpdatePlatform(Vector3 grabberPosition, Vector3 direction)
    {
        if (!mIsGrabbed)
            return;

        direction = Vector3.Normalize(direction);

        _checkForObstacles(grabberPosition, direction);

        Vector3 newPosition;
        if (mHittingObstascle)
        {
            newPosition = mObstacleLoc;
        }
        else
        {
            newPosition = grabberPosition + direction * mDistance;
        }
        if (lockZ)
        {
            newPosition.y = mLockedY;
        }
        transform.position = newPosition;
    }
    public void GrabPlatform(float distance)
    {
        if (!mIsGrabbed)
        {
            mDistance = distance;
            mIsGrabbed = true;
            mLockedY = transform.position.y;
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            Debug.Log("Grabbed platform");
        }
    }

    public void FreezePlatform()
    {
        if (mIsGrabbed)
        {
            mDistance = 0f;
            mIsGrabbed = false;
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
            Debug.Log("Freeze platform");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
