using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    float groundCheckLenght=0.7f;

    private void FixedUpdate()
    {
        GetComponentInParent<Movement>().isGrounded = Physics.Raycast(transform.position,-transform.up,groundCheckLenght);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,-transform.up*groundCheckLenght);
    }
}
