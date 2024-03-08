using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Analytics;

public class MovingPlatform : MonoBehaviour
{
    public bool lockZ = true;

    private float mLockedY = 0f;
    private float mDistance = 0f;
    private bool mIsGrabbed = false;
    private bool mHittingObstascle = false;
    private Vector3 mObstacleLoc = Vector3.zero;
    private Material mErrorMaterial;
    private Material mNormalMaterial;
    private Vector3 mHitOffset;
    private MovingPlatformCollision mMovingPlatformCollision;

    private void _checkForObstacles(Vector3 grabberPosition, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(grabberPosition, direction, out hit, mDistance, int.MaxValue, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log("Platform cannot be placed, hitting: " + hit.transform.gameObject.name + " at dist " + hit.distance + " max dist: " + mDistance);
            mHittingObstascle = true;
            mObstacleLoc = hit.point;
        }
        else
        {
            if (mMovingPlatformCollision != null && mMovingPlatformCollision.IsColliding())
            {
                mHittingObstascle = true;
            }
            else
            {
                mHittingObstascle = false;
            }

            mObstacleLoc = Vector3.zero;
        }


    }

    private void _updateMaterialType()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer == null)
            return;

        if (mHittingObstascle)
        {
            renderer.material = mErrorMaterial;
        }
        else
        {
            renderer.material = mNormalMaterial;
        }
    }

    public void UpdatePlatform(Vector3 grabberPosition, Vector3 direction)
    {
        if (!mIsGrabbed)
            return;

        direction = Vector3.Normalize(direction);

        _checkForObstacles(grabberPosition, direction);
        _updateMaterialType();

        Vector3 newPosition;
        if (mHittingObstascle && mObstacleLoc != Vector3.zero)
        {
            newPosition = mObstacleLoc + mHitOffset;
        }
        else
        {
            newPosition = grabberPosition + mHitOffset + direction * mDistance;
        }

        if (lockZ)
        {
            newPosition.y = mLockedY;
        }
        transform.position = newPosition;
    }
    public void GrabPlatform(float distance, Vector3 hitLocation)
    {
        if (!mIsGrabbed)
        {
            mDistance = distance;
            mIsGrabbed = true;
            mLockedY = transform.position.y;
            mHitOffset = transform.position - hitLocation;
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
            if (mHittingObstascle)
            {
                mHittingObstascle = false;
                _updateMaterialType();
            }

            Debug.Log("Freeze platform");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mErrorMaterial = Resources.Load("Materials/ErrorRed", typeof(Material)) as Material;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null) {
            mNormalMaterial = renderer.material;
        }
        GameObject colliderObject = new GameObject(gameObject.name + "_collider");
        colliderObject.transform.parent = transform;
        mMovingPlatformCollision = colliderObject.AddComponent<MovingPlatformCollision>();
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter != null && renderer != null)
        {
            mMovingPlatformCollision.InitializeCollision(meshFilter.mesh, renderer.material);
        }
        else
        {
            Debug.Log("NU AM PUTUT INSTANTIA PLATFORMA!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
