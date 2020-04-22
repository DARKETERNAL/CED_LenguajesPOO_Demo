using UnityEngine;

public class NavmeshAIController : NavMeshAgentController
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float distanceToChase = 150F;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if ((playerTransform.position - transform.position).magnitude <= distanceToChase)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }
}