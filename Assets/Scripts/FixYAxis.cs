using UnityEngine;

public class FixYAxis : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 pos = transform.position;
        pos.y = Mathf.Max(target.position.y, 0f);
        transform.position = pos;
    }
}