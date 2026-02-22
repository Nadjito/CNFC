using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 4f;
    public Vector3 offset = new Vector3(4, 0, -10);
    public Vector2 cameraRotation;

    

    void Start()
    {
        transform.localEulerAngles=new Vector3(cameraRotation.x, cameraRotation.y, 0);

    }

    void FixedUpdate()//TO ADD - if game ended, stop follogwing
    {
        Vector3 vector = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, vector, smooth * Time.deltaTime);
    }
}