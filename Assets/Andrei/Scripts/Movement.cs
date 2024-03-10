using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float jumpForce, thrustForce;
    [SerializeField]
    public float initial_velocity, time_to_initial_velocity, final_velocity, time_to_final_velocity;
    [SerializeField]
    private float velocity;
    [SerializeField]
    Transform cam, gunTip;
    [SerializeField]
    LayerMask grappable;
    LineRenderer lr;
    [SerializeField]
    float maxSwingDistance;
    Vector3 swingPoint;
    SpringJoint joint;

    Rigidbody current_rigidbody;
    Vector3 moveVector;
    Vector3 globalMoveVector;

    Vector2 lookRotation;
    public bool isGrounded, isSwinging, canClimb;
    [SerializeField]
    float wallDetectionLenght;
    [SerializeField]
    float wallSphereRadius;
    [SerializeField]
    float maxWallLookAngle;
    float wallLookAngle;

    RaycastHit frontWallHit;
    [SerializeField]
    bool isClimbing = false;
    [SerializeField]
    float climbSpeed;
    float climbTimer;
    [SerializeField]
    float maxClimbTimer;
    [SerializeField]
    float climbJumpUpSpeed;
    [SerializeField]
    float climbJumpBackSpeed;
    RaycastHit wallRightHit, wallLeftHit;
    bool wallRight, wallLeft;
    Vector3 bonusForce = Vector3.zero;
    [SerializeField]
    float airDrag;
    [SerializeField]
    float runWallJumpUpForce, runWallJumpBackForce;
    public bool isRunning = false;
    public bool isWallRunning;
    [SerializeField]
    Vector3 wallRunningRotation;
    bool isRotated = false;
    bool wasWallRight = false;
    [SerializeField]
    float grappleRadius;
    [SerializeField]
    float lateralWallDetectionLenght;
    [SerializeField]
    float deactivateLateralMovementTime;
    float currentLatMovTimeRemain;
    [SerializeField]
    float bonusTimeToJump;
    float currentBonusTimeToJump;
    bool hasJumped = false;
    bool wasGrounded = true;

    float initialDrag;
    [SerializeField]
    float glideDrag;
    bool isGliding = false;
    [SerializeField]
    public bool canGlide = false;

    [SerializeField]
    float maxClimbAngle;
    float minAngleGlobal;
    Vector3 lowestAngleNormal;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lr = GetComponent<LineRenderer>();
        lookRotation = GetComponent<MouseLook>().lookRotation;
        current_rigidbody = GetComponent<Rigidbody>();
        velocity = 0;
        climbTimer = maxClimbTimer;
    }
    private void FixedUpdate()
    {
        OnRun();
        if (!isSwinging)
            Run();
        if (isSwinging)
            SwingThrust();
    }

    

    private void Update()
    {
        //bonus jump time
        currentBonusTimeToJump -= Time.deltaTime;

        if(!hasJumped && !isGrounded && wasGrounded)
        {
            currentBonusTimeToJump = bonusTimeToJump;
        }
        if(!wasGrounded && isGrounded)
        {
            wasGrounded = isGrounded;
            hasJumped = false;
        }
        if (wasGrounded && !isGrounded) wasGrounded = isGrounded;
        

        if (currentLatMovTimeRemain > 0) currentLatMovTimeRemain -= Time.deltaTime;

        WallCheck();
        CheckForWallRun();
        if (isGrounded)
        {
            bonusForce = Vector3.zero;
            currentLatMovTimeRemain = 0;
        }
            if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            StartSwing();
        }
        if (Input.GetMouseButtonUp(0) && isSwinging)
        {
            StopSwing();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            OnJump();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canGlide)
        {
            OnGlide();
        }
        if (Input.GetKeyUp(KeyCode.Space) && !isGrounded)
        {
            CancelGlide();
        }

        if (canClimb && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle && frontWallHit.collider.CompareTag("climbable"))
        {
            if (!isClimbing && climbTimer > 0) StartClimbing();
            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
        }
        else
        {
            if (isClimbing) StopClimbing();
        }
        if (isClimbing)
        {
            ClimbingMovement();
        }
        if (canClimb && Input.GetKeyDown(KeyCode.Space))
        {
            ClimbJump();
        }
        //wall run
        if ((wallLeft || wallRight) && Input.GetKey(KeyCode.W) && !isGrounded)
        {
            isWallRunning = true;
            //rotation along z
            if (!isRotated)
            {
                isRotated = true;
                if (wallLeft)
                {
                    wasWallRight = false;
                    transform.Rotate(-wallRunningRotation);
                }
                else if (wallRight)
                {
                    wasWallRight = true;
                    transform.Rotate(wallRunningRotation);
                }
            }
            if (bonusForce.magnitude < 0.1f)
                current_rigidbody.velocity = new Vector3(current_rigidbody.velocity.x, 0, current_rigidbody.velocity.z);
            if (Input.GetKeyDown(KeyCode.Space))
                WallRunJump();
        }
        else
        {
            if (isRotated)
            {
                isRotated = false;
                if (wasWallRight)
                {
                    transform.Rotate(-wallRunningRotation);
                }
                else if (!wasWallRight)
                {
                    transform.Rotate(wallRunningRotation);
                }
            }
            isWallRunning = false;
        }
        if (transform.rotation.eulerAngles.z < 0.1 && transform.rotation.eulerAngles.z > -0.1 && transform.rotation.eulerAngles.z!=0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,0);
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void CheckForWallRun()
    {
        wallRight = Physics.Raycast(transform.position, transform.right, out wallRightHit, lateralWallDetectionLenght) && wallRightHit.collider.CompareTag("wallRun");
        wallLeft = Physics.Raycast(transform.position, -transform.right, out wallLeftHit, lateralWallDetectionLenght) && wallLeftHit.collider.CompareTag("wallRun");
    }

    public void Run()
    {
        if (moveVector.magnitude < 0.1)
        {
            isRunning = false;
            velocity = 0;
        }
        //initial acceleration
        if (moveVector.magnitude > 0.01)
        {
            isRunning = true;
            if (velocity < initial_velocity)
            {
                float dx = (initial_velocity * Time.deltaTime) / time_to_initial_velocity; //rate of change needed in this frame
                velocity += dx;
            }
            //second acceleration
            else if (velocity < final_velocity)
            {
                float dx = (final_velocity * Time.deltaTime) / time_to_final_velocity;
                velocity += dx;
            }
            if (velocity > final_velocity) velocity = final_velocity;
        }

        //change of basis from local to global of moveVector
        globalMoveVector.x = transform.right.x * moveVector.x + transform.forward.x * moveVector.z;
        globalMoveVector.z = transform.right.z * moveVector.x + transform.forward.z * moveVector.z;
        globalMoveVector.Normalize();

        //apply velocity
        current_rigidbody.velocity = new Vector3(velocity * globalMoveVector.x, current_rigidbody.velocity.y, velocity * globalMoveVector.z) + bonusForce;

        //apply drag
        if(bonusForce.magnitude > 0)
            bonusForce = bonusForce - bonusForce.normalized * airDrag * Time.deltaTime;
        if (bonusForce.magnitude < 0)
            bonusForce = Vector3.zero;
    }

    void WallCheck()
    {
        if(isGrounded)
        {
            climbTimer = maxClimbTimer;        
        }
        canClimb = Physics.SphereCast(transform.position, wallSphereRadius, transform.forward, out frontWallHit, wallDetectionLenght, grappable);
        wallLookAngle = Vector3.Angle(transform.forward, -frontWallHit.normal);
    }    

    void SwingThrust()
    {
        if(moveVector.x>0.1)
            current_rigidbody.AddForce(transform.right * thrustForce * Time.deltaTime);
        if(moveVector.x<-0.1)
            current_rigidbody.AddForce(-transform.right * thrustForce * Time.deltaTime);
    }

    public void OnRun()
    {
        Vector2 input_trans = (!isGliding || isGrounded) 
            ? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))
            : new Vector2(0, Input.GetAxis("Vertical"));
        input_trans.Normalize();
        moveVector = new Vector3(input_trans.x, 0, input_trans.y);
        if (currentLatMovTimeRemain > 0.01) moveVector = new Vector3(moveVector.x, 0, moveVector.z);

        /*//stop move towards high angle slope
        if (minAngleGlobal > maxClimbAngle)
        {
            Debug.Log(lowestAngleNormal);
            Vector3 projectedNormal = Vector3.ProjectOnPlane(lowestAngleNormal, transform.up);
            projectedNormal.Normalize();
            moveVector = Vector3.ProjectOnPlane(moveVector, projectedNormal);
            moveVector.Normalize();
            Debug.Log(moveVector);
        }*/
        if(minAngleGlobal > maxClimbAngle)
        {
            moveVector = Vector3.zero;
        }

    }

    public void OnJump()
    {
        /*if(canClimb && wallLookAngle<maxWallLookAngle)
        {
            if (!isClimbing && climbTimer > 0) StartClimbing();
            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
        }*/
        if (isGrounded || currentBonusTimeToJump > 0.1)
        {
            hasJumped = true;
            currentBonusTimeToJump = 0;
            current_rigidbody.AddForce(jumpForce * transform.up, ForceMode.VelocityChange);
            GetComponentInChildren<AudioManager>().Jump();
        }
    }
  
    void StartSwing()
    {
        RaycastHit hit;
        if(Physics.SphereCast(cam.position,grappleRadius,cam.forward,out hit,maxSwingDistance,grappable) && hit.collider.gameObject.CompareTag("grapple"))
        {
            GetComponentInChildren<AudioManager>().Laser();
            isSwinging = true;
            swingPoint = hit.point;
            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            
        }
    }
    void StopSwing()
    {
        isSwinging = false;
        lr.positionCount = 0;
        Destroy(joint);
    }
    void DrawRope()
    {
        if (!isSwinging)
            return;
        if (lr.positionCount != 2)
            return;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }

    private void StartClimbing()
    {
        isClimbing = true;
    }
    private void ClimbingMovement()
    {
        current_rigidbody.velocity = new Vector3(current_rigidbody.velocity.x, climbSpeed, current_rigidbody.velocity.z);

    }
    private void StopClimbing()
    {
        isClimbing = false;

    }
    
    void ClimbJump()
    {
        current_rigidbody.velocity = new Vector3(current_rigidbody.velocity.x, 0, current_rigidbody.velocity.z);
        Vector3 forceToApply = transform.up * climbJumpUpSpeed + frontWallHit.normal * climbJumpBackSpeed;
        current_rigidbody.AddForce(new Vector3(0, forceToApply.y, 0), ForceMode.VelocityChange);
        forceToApply.y = 0;
        if (bonusForce.magnitude < forceToApply.magnitude)
            bonusForce += forceToApply;
        GetComponentInChildren<AudioManager>().Jump();
    }

    void WallRunJump()
    {
        current_rigidbody.velocity = new Vector3(current_rigidbody.velocity.x, 0, current_rigidbody.velocity.z);
        RaycastHit current_hit = wallRight ? wallRightHit : wallLeftHit;
        Vector3 forceToApply = transform.up * runWallJumpUpForce + current_hit.normal * runWallJumpBackForce;
        current_rigidbody.AddForce(new Vector3(0, forceToApply.y, 0), ForceMode.VelocityChange);
        forceToApply.y = 0;
        if (bonusForce.magnitude < forceToApply.magnitude)
            bonusForce += forceToApply;
        GetComponentInChildren<AudioManager>().Jump();

        currentLatMovTimeRemain = deactivateLateralMovementTime;
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

    void UpdateContacts(ContactPoint[] contacts)
    {
        float minAngle = 90;
        Vector3 minNormal = Vector3.zero;
        for (int i = 0; i < contacts.Length; i++)
        {
            float angle = Vector3.Angle(contacts[i].normal, transform.up);
            if (angle < minAngle)
            {
                minAngle = angle;
                minNormal = contacts[i].normal;
            }
        }
        if (contacts.Length == 0)
            minAngle = 0;
        Debug.Log(minAngle);
        minAngleGlobal = minAngle;
        lowestAngleNormal = minNormal;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.GetComponent<Terrain>())
            UpdateContacts(collision.contacts);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Terrain>())
            UpdateContacts(collision.contacts);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(gunTip.position, maxSwingDistance);
        Debug.DrawRay(gunTip.position, cam.forward * maxSwingDistance, Color.red);
        Debug.DrawRay(transform.position, transform.forward * wallDetectionLenght, Color.red);
        Debug.DrawRay(transform.position, transform.right * lateralWallDetectionLenght, Color.red);
        Debug.DrawRay(transform.position, -transform.right * lateralWallDetectionLenght, Color.red);
        Gizmos.DrawSphere(cam.forward * maxSwingDistance + transform.position, grappleRadius);
        Gizmos.DrawSphere(transform.forward * wallDetectionLenght + transform.position, wallSphereRadius);
        //Gizmos.DrawSphere(gunTip.position, wallDetectionLenght);
    }*/
}