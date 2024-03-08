using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCollision : MonoBehaviour
{
    private bool mInitialized = false;
    int numberOfCollisions = 0;

    public bool IsColliding()
    {
        Debug.Log(numberOfCollisions);
        return numberOfCollisions > 0;
    }
    public void InitializeCollision(Mesh sourceMesh, Material sourceMaterial)
    {
        if (mInitialized) return;

        mInitialized = true;

        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;

        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        renderer.material = sourceMaterial;
        gameObject.AddComponent<MeshFilter>().mesh = sourceMesh;

        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        rigidbody.useGravity = false;
    }

    private bool _shouldIgnore(Collider col)
    {
        if (col.gameObject.name + "_collider" == gameObject.name)
            return true;
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_shouldIgnore(other))
            return;

        numberOfCollisions++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_shouldIgnore(other))
            return;

        numberOfCollisions--;
    }
}
