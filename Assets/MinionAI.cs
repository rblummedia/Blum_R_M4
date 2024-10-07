using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MinionAI : MonoBehaviour
{
    private enum MinionState { StationaryWaypoints, MovingWaypoint }

    private MinionState currentState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    public GameObject[] waypoints;
    public GameObject movingWaypoint;
    private int currWaypoint = -1;
    private bool FiveWaypointLoop = true;

    public float lookaheadTime = 1.0f;
    public float captureDistance = 1.0f;

    private void SetNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No stationary waypoints set for MinionAI.");
            return;
        }

        currWaypoint = (currWaypoint + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentState = MinionState.StationaryWaypoints;
        SetNextWaypoint();
    }

    private void Update()
    {
        switch (currentState)
        {
            case MinionState.StationaryWaypoints:
                FiveWaypointLoop = true;
                HandleStationaryWaypoints();
                break;

            case MinionState.MovingWaypoint:
                FiveWaypointLoop = false;
                HandleMovingWaypoint();
                break;
        }

        float movementSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        animator.SetFloat("vely", movementSpeed);
    }

    private void HandleStationaryWaypoints()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            SetNextWaypoint();
        }

        if (currWaypoint == waypoints.Length - 1 && currentState != MinionState.MovingWaypoint)
        {
            currentState = MinionState.MovingWaypoint;
        }
    }

    private void HandleMovingWaypoint()
    {
        Vector3 futurePosition = PredictMovingWaypointPosition();
        navMeshAgent.SetDestination(futurePosition);

        float distToMovingWaypoint = Vector3.Distance(transform.position, movingWaypoint.transform.position);
        if (distToMovingWaypoint <= captureDistance)
        {
            currentState = MinionState.StationaryWaypoints;
            SetNextWaypoint();
        }
    }

    public Vector3 PredictMovingWaypointPosition()
    {
        VelocityReporter velocityReporter = movingWaypoint.GetComponent<VelocityReporter>();

        Vector3 futurePosition = movingWaypoint.transform.position + (velocityReporter.velocity * lookaheadTime);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(futurePosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            futurePosition = hit.position;
        }

        return futurePosition;
    }

    public int CurrentWaypointIndex => currWaypoint;

    public Vector3 GetNextWaypointPosition()
    {
        if (waypoints.Length > 0)
        {
            return waypoints[(currWaypoint) % waypoints.Length].transform.position;
        }
        return Vector3.zero;
    }

    public bool GetFiveWaypointLoop() {
        return FiveWaypointLoop;
    }
}
