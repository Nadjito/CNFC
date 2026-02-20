using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 4f;
    public Vector3 offset = new Vector3(4, 0, -10);
    public float cameraRotation;

    void Start()
    {
        transform.rotation = Quaternion.AngleAxis(cameraRotation, Vector3.right);
    }

    void FixedUpdate()
    {
        Vector3 vector = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, vector, smooth * Time.deltaTime);
    }
}