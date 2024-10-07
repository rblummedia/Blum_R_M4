using UnityEngine;

public class CollectableBall : MonoBehaviour
{
    // This method is triggered when another collider enters this object's trigger collider
    void OnTriggerEnter(Collider c)
    {
        // Check if the object has a Rigidbody component attached
        if (c.attachedRigidbody != null)
        {
            // Try to get the BallCollector component from the object with the Rigidbody
            BallCollector bc = c.attachedRigidbody.gameObject.GetComponent<BallCollector>();

            // If the object has a BallCollector component, proceed
            if (bc != null)
            {
                // Call the ReceiveBall method on the BallCollector component
                bc.ReceiveBall();

                // Trigger an event that passes the position of the object collecting the ball
                EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);

                // Destroy the collectable ball GameObject after it's been collected
                Destroy(this.gameObject);
            }
        }
    }
}
