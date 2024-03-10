using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck_fara_glide : MonoBehaviour
{
    [SerializeField]
    float groundCheckLenght = 0.5f;
    
    private void FixedUpdate()
    {
        Movement movement = GetComponentInParent<Movement>();
       movement.isGrounded = Physics.Raycast(transform.position, -transform.up, groundCheckLenght);
       

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, -transform.up * groundCheckLenght);
    }
}
