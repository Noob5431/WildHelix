using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject Player;
    public KeyCode keyToRespawn = KeyCode.R;
    public TMPro.TMP_Text PressToReturnText;
    public float LowestPoint = 0f;

    private int mHighestCheckpoint = -1;
    private Vector3 mRespawnLocation;

    public static CheckpointManager mInstance;

    public void SetCheckpointReached(Checkpoint checkpoint)
    {
        if (checkpoint.priority >= mHighestCheckpoint)
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
        if (Input.GetKeyUp(keyToRespawn))
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

        if (PressToReturnText != null && Player != null)
        {
            if (Player.transform.position.y < LowestPoint)
            {
                PressToReturnText.gameObject.SetActive(true);
            }
            else
            {
                PressToReturnText.gameObject.SetActive(false);
            }
        }
    }
}
