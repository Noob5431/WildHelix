using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int priority = 0;
    public float checkpointScale = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if(priority == 0)
        {
            CheckpointManager.mInstance.SetCheckpointReached(this);
        }
        Collider collider = GetComponent<Collider>();
        if(collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>();
            gameObject.transform.localScale = Vector3.one * checkpointScale;
        }
        collider.isTrigger = true;
    }

    private bool _isPlayer(GameObject objectToCheck)
    {
        if (objectToCheck == null) return false;
        if (objectToCheck.GetComponent<Movement>() != null)
            return true;

        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!_isPlayer(other.gameObject))
            return;

        CheckpointManager.mInstance.SetCheckpointReached(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 2f, 1f));
        Collider collider = GetComponent<Collider>();
        Gizmos.color = Color.green;
        if (collider == null)
        {
            Gizmos.DrawWireCube(transform.position, Vector3.one * checkpointScale);
        }
    }
}
