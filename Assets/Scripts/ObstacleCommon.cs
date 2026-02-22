using UnityEngine;

public class ObstacleCommon : MonoBehaviour
{
    private new Collider collider;
    [HideInInspector]public bool triggerAbility;
    public Transform player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider>();
        triggerAbility = false;
        player = null;
    }

    void OnEnable()
    {
        if (collider != null)
        {
            collider.enabled = true;

        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        Debug.Log("Collision detected between " + collision.gameObject.name+" and "+gameObject.name);
        collider.enabled = false;
        player=collision.transform;

        triggerAbility = true;

    }
}
