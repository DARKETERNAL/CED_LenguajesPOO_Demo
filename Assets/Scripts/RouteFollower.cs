using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class RouteFollower : MonoBehaviour
{
    [SerializeField]
    private Transform[] routePoints;

    [SerializeField]
    private bool loopRoute = true;

    [Header("Follow NavMesh route")]
    [SerializeField]
    private bool followNavMeshRoute = true;

    [Header("Follow Transform route")]
    [SerializeField]
    private float transformMoveSpeed = 1.0F;

    [SerializeField]
    private float distanceThreshold;

    private int currentPointIndex = 0;
    private NavMeshAgent navMeshAgent;

    private void FollowNavMeshRoute()
    {
        navMeshAgent.SetDestination(routePoints[currentPointIndex].position);
    }

    private void FollowTranslateRoute()
    {
        float moveStep = transformMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, routePoints[currentPointIndex].position, moveStep);
    }

    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        FollowRoute();
    }

    private void Update()
    {
        if (followNavMeshRoute)
        {
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0F)
                    {
                        OnRouteCompleted();
                    }
                }
            }
        }
        else
        {
            FollowTranslateRoute();
            float remainingDistance = DistanceToCurrentPoint();
            print(string.Format("Remaining distance: {0}", remainingDistance));
            if (remainingDistance < distanceThreshold)
            {
                OnRouteCompleted();
            }
        }
    }

    private void OnRouteCompleted()
    {
        currentPointIndex += 1;

        if (currentPointIndex >= routePoints.Length && loopRoute)
        {
            currentPointIndex = 0;
        }

        FollowRoute();
    }

    private void FollowRoute()
    {
        if (currentPointIndex < routePoints.Length)
        {
            if (followNavMeshRoute)
            {
                FollowNavMeshRoute();
            }
        }
        else
        {
            enabled = false;
        }
    }

    private float DistanceToCurrentPoint()
    {
        Vector3 currentPosition = transform.position;
        Vector3 currentDestination = routePoints[currentPointIndex].position;

        return Vector3.Distance(
            new Vector3(currentPosition.x, 0F, currentPosition.z),
            new Vector3(currentDestination.x, 0F, currentDestination.z));
    }
}