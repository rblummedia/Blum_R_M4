using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBean : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = true;
    
    // Random jump interval
    public float minJumpInterval = 1f;
    public float maxJumpInterval = 3f;
    
    // Random jump force
    public float minJumpForce = 5f;
    public float maxJumpForce = 10f;
    
    // Random torque (for rotating the bean)
    public float minTorque = 5f;
    public float maxTorque = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Start the random jumping behavior
        StartCoroutine(JumpRoutine());
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            // Wait for a random interval before the next jump
            float waitTime = Random.Range(minJumpInterval, maxJumpInterval);
            yield return new WaitForSeconds(waitTime);

            // Only jump if grounded
            if (isGrounded)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        // Apply random jump force in the upward direction
        float jumpForce = Random.Range(minJumpForce, maxJumpForce);
        Vector3 jumpDirection = new Vector3(
            Random.Range(-1f, 1f),   // Random horizontal jump angle
            1f,                      // Upward force
            Random.Range(-1f, 1f)    // Random horizontal jump angle
        ).normalized;

        rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);

        // Apply random torque for rotation
        float torqueForce = Random.Range(minTorque, maxTorque);
        Vector3 torqueDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );
        rb.AddTorque(torqueDirection * torqueForce, ForceMode.Impulse);
        
        isGrounded = false; // Assume it’s airborne after the jump
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bean has landed on the ground
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
}
