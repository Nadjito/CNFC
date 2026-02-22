using System.Collections;
using UnityEngine;

public class ObstacleCommon : MonoBehaviour
{
    private new Collider collider;
    [HideInInspector] public bool triggerAbility;
    [HideInInspector] public Transform player;
    private Animator animator;
    private bool prepareToDeactivate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider>();
        animator = gameObject.GetComponent<Animator>();
        triggerAbility = false;
        player = null;
        Checks();
        prepareToDeactivate = false;
    }

    void Update()
    {
        if (prepareToDeactivate)
        {
                StartCoroutine(DeactivateThis());

        }
    }

    void Checks()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on "+gameObject.name+" object.");
        }
        if (collider == null)
        {
            Debug.LogWarning("Collider component not found on "+gameObject.name+" object. Please add a BoxCollider component to the obstacle prefab.");
        }

    }

    void OnEnable()
    {
        if (collider != null)   
            collider.enabled = true;
        prepareToDeactivate = false;

    }

    private void OnDisable()
    {
        triggerAbility = false;
        //animator.SetBool("TriggerAbility", false);
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collider.enabled = false;
            player = collision.transform;

            triggerAbility = true;
            //animator.SetBool("TriggerAbility", true);
            prepareToDeactivate = true;
        }
        else
        {
            Debug.LogWarning("Obstacle collided with an object that is not the player. Please check the tags and colliders of the objects in the scene.");
        }
    }

    IEnumerator DeactivateThis()
    {
               yield return new WaitForSeconds(transform.parent.GetComponent<ObstacleSpawner>().obstacleLifetime);
        transform.parent.GetComponent<ObstacleSpawner>().ReturnObstacleToPool(gameObject);
    }
}
