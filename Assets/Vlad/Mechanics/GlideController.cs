using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class GlideController : MonoBehaviour
{
    public float speed = 12.5f;
    public float drag = 6;
    private float fastSpeed = 13.8f;
    private float slowSpeed = 12.5f;
    private float slowDrag = 4;
    private float fastDrag = 6;
    private float maxAngle = 45.0f;
    private float rotationStep = 35.0f;
    private float shakeDegree = 5.0f;

    private Vector3 rotation;

    public Rigidbody rb;

    public float rotationPercentage;
    public float landingDelay = 3.0f;
    public CameraShake cameraShake;
    float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotation = transform.eulerAngles;
    }

    void Update()
    {
        // slide when landing
        if (!IsGrounded() && timer <= landingDelay)
        {
            updateMovement();
        } else
        {
            cameraShake.shaking = 0;
            timer = 0;
        }

        // check if player is landing
        if (IsGrounded())
        {
            timer += Time.deltaTime;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        float rayLength = 1.1f; // Adjust based on your character's size
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
        {
            return true;
        }
        return false;
    }

    private void updateMovement()
    {
        rotation.x += rotationStep * Input.GetAxis("Vertical") * Time.deltaTime;
        rotation.x = Mathf.Clamp(rotation.x, 0, maxAngle);

        rotation.y += rotationStep * Input.GetAxis("Horizontal") * Time.deltaTime;

        rotation.z = -shakeDegree * Input.GetAxis("Horizontal");
        rotation.z = Mathf.Clamp(rotation.z, -shakeDegree, shakeDegree);

        transform.rotation = Quaternion.Euler(rotation);

        rotationPercentage = rotation.x / maxAngle;
        float mod_drag = (rotationPercentage * (-2)) + fastDrag;
        float mod_speed = rotationPercentage * (fastSpeed - slowSpeed) + slowSpeed;

        rb.drag = mod_drag;
        Vector3 localV = transform.InverseTransformDirection(rb.velocity);
        localV.z = mod_speed;
        rb.velocity = transform.TransformDirection(localV);
    }
}
