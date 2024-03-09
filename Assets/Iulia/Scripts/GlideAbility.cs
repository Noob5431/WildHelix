using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideAbility : MonoBehaviour
{
    public bool isGrounded;
    float initialDrag;
    [SerializeField]
    float glideDrag;
    bool isGliding = false;
    Rigidbody current_rigidbody;
    public KeyCode keyToUse = KeyCode.None;

    // Start is called before the first frame update
    void Start()
    {
        //current_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToUse) && !isGrounded)
        {
            current_rigidbody = GetComponent<Rigidbody>();
            OnGlide();
        }
        if (Input.GetKeyUp(keyToUse) && !isGrounded)
        {
            current_rigidbody = GetComponent<Rigidbody>();
            CancelGlide();
        }
    }


    public void OnGlide()
    {
        initialDrag = current_rigidbody.drag;
        current_rigidbody.drag = glideDrag;
        isGliding = true;
    }

    public void CancelGlide()
    {
        current_rigidbody.drag = initialDrag;
        isGliding = false;
    }
}
