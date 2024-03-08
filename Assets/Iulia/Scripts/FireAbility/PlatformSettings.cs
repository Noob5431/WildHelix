using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSettings : MonoBehaviour
{
    [SerializeField] Vector3 movePoint;
    [SerializeField] float speed;
    [SerializeField] float delayStart;
    [SerializeField] float delayEnd;

    public Vector3 GetMovePoint()
    {
        return movePoint;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetDelayStart()
    {
        return delayStart;
    }

    public float GetDelayEnd()
    {
        return delayEnd;
    }
}
