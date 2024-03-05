using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    public float gravity;
    public float initial_gravity;

    private void Start()
    {
        initial_gravity = gravity;
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity -= new Vector3(0, gravity * Time.fixedDeltaTime, 0);
    }
}
