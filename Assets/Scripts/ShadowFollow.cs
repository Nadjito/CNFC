using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = target.position.x;
        transform.position = pos;
    }
}
