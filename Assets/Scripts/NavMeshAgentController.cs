using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentController : MonoBehaviour
{
    [SerializeField]
    private LayerMask walkableLayer;

    protected NavMeshAgent agent;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // detectar posición del mouse
            // tirar raycast hacia la pantalla
            // si encuentro suelo, mover al punto.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, walkableLayer))
            {                
                agent.SetDestination(hit.point);
            }

        }
    }
}