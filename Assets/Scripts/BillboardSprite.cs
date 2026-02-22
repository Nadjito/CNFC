using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    [SerializeField] private Transform playerAndShadow;
    [SerializeField] private Transform playerReflection;

    [SerializeField] private Transform water;

    [SerializeField]private float scaleFactor;

    private float distanceFromWaterLevel;

    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void FixedUpdate()
    {
        distanceFromWaterLevel = Mathf.Abs(transform.position.y - water.position.y);
        //Debug.Log("Distance from water level: " + distanceFromWaterLevel);

        float newScale = 1 / distanceFromWaterLevel;
        if(newScale>1)
            newScale=1;
        //Debug.Log("New scale: " + newScale);

        if (transform.position.y< water.position.y)
        {
            
            playerReflection.localScale = new Vector3(1, 1, 1);
           playerAndShadow.localScale = new Vector3(newScale, newScale,1);

        }
        else
        {
            playerAndShadow.localScale=new Vector3(1,1,1);
           playerReflection.localScale = new Vector3(newScale, newScale, 1);
        }
    }
}


