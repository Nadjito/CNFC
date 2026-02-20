using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PenguinController : MonoBehaviour
{
    public float forwardSpeed = 3f;
    public float downForce = 10f;
    public float upForce = 10f;               
    public float buoyancyStrength = 8f;       
    public float buoyancyDamping = 2f;       
    public float maxVerticalSpeed = 8f;
    public float maxUp = 3.5f;
    public float maxDown = -3.5f;

    private Rigidbody rb;
    private bool isPressing;
    private bool hadPressed;
    private float restY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        restY = transform.position.y; 
        rb.linearVelocity = new Vector3(forwardSpeed, 0f, 0f);
    }

    void Update()
    {
        isPressing = Mouse.current != null && Mouse.current.leftButton.isPressed;
    }

    void FixedUpdate()
    {
        Vector3 vel = rb.linearVelocity;
        vel.x = forwardSpeed;
        rb.linearVelocity = vel;

        if (isPressing)
        {
            rb.AddForce(Vector3.down * downForce, ForceMode.Acceleration);
            hadPressed = true;
        }
        else
        {
            if (hadPressed)
            {
                float maxDepth = restY - maxDown; 
                float depth = Mathf.Clamp(restY - rb.position.y, 0f, maxDepth);
                float depthRatio = (maxDepth > 0f) ? (depth / maxDepth) : 0f;
                float impulse = upForce * Mathf.Lerp(0.6f, 1.6f, depthRatio);
                rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
                hadPressed = false;
            }
            float displacement = restY - rb.position.y;
            float buoyancy = displacement * buoyancyStrength;
            float damping = -rb.linearVelocity.y * buoyancyDamping;
            rb.AddForce(Vector3.up * (buoyancy + damping), ForceMode.Acceleration);
        }
        
        vel = rb.linearVelocity;
        vel.x = forwardSpeed;
        vel.y = Mathf.Clamp(vel.y, -maxVerticalSpeed, maxVerticalSpeed);
        rb.linearVelocity = vel;
        
        Vector3 pos = rb.position;
        pos.y = Mathf.Clamp(pos.y, maxDown, maxUp);
        rb.MovePosition(pos);
    }
}