using UnityEngine;

public class DestinationTracker : MonoBehaviour
{
    public GameObject minion;
    public GameObject tracker;
    private GameObject trackerInstance;

    private void Start()
    {
        trackerInstance = Instantiate(tracker);
    }

    private void Update()
    {
        if (minion != null)
        {
            MinionAI minionAI = minion.GetComponent<MinionAI>();
            if (minionAI != null)
            {
                Vector3 nextWaypointPosition;
                if (minionAI.GetFiveWaypointLoop()) {
                    nextWaypointPosition = minionAI.GetNextWaypointPosition();
                } else {
                    nextWaypointPosition = minionAI.PredictMovingWaypointPosition();
                }
                
                trackerInstance.transform.position = nextWaypointPosition;
            }
        }
    }

    private void OnDestroy()
    {
        if (trackerInstance != null)
        {
            Destroy(trackerInstance);
        }
    }
}
