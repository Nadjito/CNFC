using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PenguinController : MonoBehaviour
{
    public float forwardSpeed = 3f;
    public float speedBoostPerfect = 2f;
    public float speedBoostDiveMax = 3f;
    public float speedDecay = 0.5f;
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
    private float lastPressTime = -999f;
    private float restY;
    public float currentForwardSpeed;
    private float maxDepthReached;
    private bool perfectPress;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        restY = transform.position.y;
        currentForwardSpeed = forwardSpeed;
        rb.linearVelocity = new Vector3(currentForwardSpeed, 0f, 0f);
        pressStartTime = -999f;
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogWarning("Animator component not found on PenguinController object.");
    }

    void Update()
    {
        bool currentPress = Mouse.current != null
            ? Mouse.current.leftButton.isPressed
            : Input.GetMouseButton(0);

        bool justPressed = Mouse.current != null
            ? Mouse.current.leftButton.wasPressedThisFrame
            : Input.GetMouseButtonDown(0);

        if (justPressed)
            lastPressTime = Time.time;

        perfectPress = (Time.time - lastPressTime) <= 0.4f;

        if (justPressed && !prevPress)
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

        if (currentForwardSpeed > forwardSpeed)
            currentForwardSpeed = Mathf.Max(currentForwardSpeed - speedDecay * Time.fixedDeltaTime, forwardSpeed);

        if (currentForwardSpeed < forwardSpeed)
            currentForwardSpeed = Mathf.Max(currentForwardSpeed - speedDecay * Time.fixedDeltaTime, forwardSpeed);

        if (inWater && !wasInWater)
        {
            bool perfectTiming = perfectPress || (hadPressed && pressStartTime > 0f && Time.time - pressStartTime <= perfectJumpWindow);
            if (perfectTiming)
            {
                float impulse = upForce * perfectJumpMultiplier;
                rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
                currentForwardSpeed = Mathf.Min(currentForwardSpeed + speedBoostPerfect, forwardSpeed * 3f);
                animator.SetTrigger("Parry");
                Debug.Log("Parry");
                hadPressed = false;
                canJump = false;
                maxDepthReached = 0f;
            }
        }

        wasInWater = inWater;

        if (inWater)
            maxDepthReached = Mathf.Max(maxDepthReached, prof);
        //animator.SetBool("JumpingIdle", false);

        if (inWater && prof > minDiveForJump)
            canJump = true;

        if (isPressing && inWater)
        {
            rb.AddForce(Vector3.down * downForce, ForceMode.Acceleration);

            float maxDepth = restY - maxDown;
            float depthRatio = maxDepth > 0f ? Mathf.Clamp01(prof / maxDepth) : 0f;
            float targetSpeed = Mathf.Lerp(forwardSpeed, forwardSpeed + speedBoostDiveMax, depthRatio);

            if (targetSpeed > currentForwardSpeed)
                currentForwardSpeed = targetSpeed;

            if (rb.linearVelocity.y > 0f)
            {
                hadPressed = false;
                canJump = false;
            }
        }
        else
        {
            if (hadPressed && canJump && inWater)
            {
                float maxDepth = restY - maxDown;
                float depth = Mathf.Clamp(maxDepthReached, 0f, maxDepth);
                float depthRatio = maxDepth > 0f ? depth / maxDepth : 0f;
                float impulse = upForce * Mathf.Lerp(0.6f, 1.6f, depthRatio);
                rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
                currentForwardSpeed = Mathf.Min(currentForwardSpeed + speedBoostDiveMax * depthRatio, forwardSpeed * 3f);
                hadPressed = false;
                canJump = false;
                maxDepthReached = 0f;
            }

            if (inWater)
            {
                float buoyancy = prof * buoyancyStrength;
                float damping = rb.linearVelocity.y > 0f ? -rb.linearVelocity.y * buoyancyDamping : 0f;
                rb.AddForce(Vector3.up * (buoyancy + damping), ForceMode.Acceleration);
                //Animator swim
            }
            else
            {
                rb.AddForce(Vector3.down * 5f, ForceMode.Acceleration);
                //Animator fall
                animator.SetBool("JumpingIdle", true);
            }
        }

        Vector3 vel = rb.linearVelocity;
        vel.x = currentForwardSpeed;
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

    public void SetSpeed(float value)
    {
        currentForwardSpeed = value;
    }

    public float GetSpeed()
    {
        return currentForwardSpeed;
    }
    public void SetCurrentForwardSpeed(float newSpeed)
    {
        currentForwardSpeed = newSpeed;
    }
}