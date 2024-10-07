using UnityEngine;

public class BallCollector : MonoBehaviour
{
    public bool hasBall = false;  // Initially false, the character hasn't collected the ball yet.

    // This method will be called when the ball is collected.
    public void ReceiveBall()
    {
        hasBall = true;  // Mark that the character now has the ball.
    }
}
