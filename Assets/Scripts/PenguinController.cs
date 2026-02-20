using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PenguinController : MonoBehaviour
{
    public float forwardSpeed = 3f;
    public float downForce = 10f;
    public float upForce = 20f;
    public float buoyancyStrength = 8f;
    public float buoyancyDamping = 2f;
    public float maxVerticalSpeed = 8f;
    public float maxUp = 3.5f;
    public float maxDown = -3.5f;
    public float minDiveForJump = 0.25f;
    public float waterSurfaceTolerance = 0.05f;
    public float perfectJumpMultiplier = 1.1f;
    public float perfectJumpWindow = 0.1f;

    private Rigidbody rb;
    private bool isPressing;
    private bool prevPress;
    private bool hadPressed;
    private bool canJump;
    private bool wasInWater;
    private float pressStartTime;
    private float restY;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        restY = transform.position.y;
        rb.linearVelocity = new Vector3(forwardSpeed, 0f, 0f);
    }

    void Update()
    {
        bool currentPress = Mouse.current != null
            ? Mouse.current.leftButton.isPressed
            : Input.GetMouseButton(0);

        if (currentPress && !prevPress)
        {
            float prof = restY - transform.position.y;
            bool inWater = prof >= -waterSurfaceTolerance;
            if (inWater)
            {
                hadPressed = true;
                pressStartTime = Time.time;
            }
        }

        isPressing = currentPress;
        prevPress = currentPress;
    }

    void FixedUpdate()
    {
        float prof = restY - rb.position.y;
        bool inWater = prof >= -waterSurfaceTolerance;
        
        if (inWater && !wasInWater)
        {
            bool perfectTiming = isPressing || (hadPressed && Time.time - pressStartTime <= perfectJumpWindow);
            if (perfectTiming)
            {
                float impulse = upForce * perfectJumpMultiplier;
                rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
                hadPressed = false;
                canJump = false;
            }
        }

        wasInWater = inWater;

        if (inWater && prof > minDiveForJump)
            canJump = true;

        if (isPressing && inWater)
        {
            hadPressed = false;
            canJump = false;
            rb.AddForce(Vector3.down * downForce, ForceMode.Acceleration);
        }
        else
        {
            if (hadPressed && canJump)
            {
                float maxDepth = restY - maxDown;
                float depth = Mathf.Clamp(prof, 0f, maxDepth);
                float depthRatio = maxDepth > 0f ? depth / maxDepth : 0f;
                float impulse = upForce * Mathf.Lerp(0.6f, 1.6f, depthRatio);
                rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
                hadPressed = false;
                canJump = false;
            }

            if (inWater)
            {
                float buoyancy = prof * buoyancyStrength;
                float damping = rb.linearVelocity.y > 0f ? -rb.linearVelocity.y * buoyancyDamping : 0f;
                rb.AddForce(Vector3.up * (buoyancy + damping), ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(Vector3.down * 5f, ForceMode.Acceleration);
            }
        }

        Vector3 vel = rb.linearVelocity;
        vel.x = forwardSpeed;
        vel.y = Mathf.Clamp(vel.y, -maxVerticalSpeed, maxVerticalSpeed);
        rb.linearVelocity = vel;

        Vector3 pos = rb.position;
        float clampedY = Mathf.Clamp(pos.y, maxDown, maxUp);
        if (!Mathf.Approximately(pos.y, clampedY))
        {
            pos.y = clampedY;
            rb.position = pos;
            vel = rb.linearVelocity;
            vel.y = 0f;
            rb.linearVelocity = vel;
        }
    }
}