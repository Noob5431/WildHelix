using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSettings : MonoBehaviour
{
    [SerializeField] Vector3 movePoint;
    [SerializeField] float speed;
    [SerializeField] float delay;

    public Vector3 GetMovePoint()
    {
        return movePoint;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetDelay()
    {
        return delay;
    }
}
