using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    float groundCheckLenght=0.7f;

    private void FixedUpdate()
    {
        GlideAbility movementGlide = GetComponentInParent<GlideAbility>();
        if (movementGlide != null)
        {
            movementGlide.isGrounded = Physics.Raycast(transform.position, -transform.up, groundCheckLenght);
        }

        FireAbility fireAbility = GetComponentInParent<FireAbility>();
        //print(fireAbility);
        if (fireAbility != null)
        {
            RaycastHit hit;
            fireAbility.isOnPlatform = Physics.Raycast(transform.position, -transform.up, out hit, groundCheckLenght, LayerMask.GetMask("FirePlatform"));
            fireAbility.platformUnderPlayer = hit.collider;
            //print(hit.collider.name);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,-transform.up*groundCheckLenght);
    }
}
