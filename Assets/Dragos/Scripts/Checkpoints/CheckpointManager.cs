using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject Player;

    private int mHighestCheckpoint = -1;
    private Vector3 mRespawnLocation;

    public static CheckpointManager mInstance;

    public void SetCheckpointReached(Checkpoint checkpoint)
    {
        if (checkpoint.priority > mHighestCheckpoint)
        {
            mHighestCheckpoint = checkpoint.priority;
            mRespawnLocation = checkpoint.transform.position;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (Player != null)
            {
                Player.transform.position = mRespawnLocation;
            }
            else
            {
                Debug.Log("PLAYER IS NULL!");
            }
        }
    }
}
