using UnityEngine;

public class ObstacleCommon : MonoBehaviour
{
    private new Collider collider;
    [HideInInspector]public bool triggerAbility;
    public Transform player;

    void OnEnable()
    {
        if (collider != null)
        {
            collider.enabled = true;

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = gameObject.GetComponent<Collider>();
        triggerAbility = false;
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collider.enabled = false;
        player=collision.transform;

        triggerAbility = true;

    }
}
