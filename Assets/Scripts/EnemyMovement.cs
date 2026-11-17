using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private playerController Pc;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Pc != null)
        {
            agent.SetDestination(Pc.transform.position);
        }
    }
}
