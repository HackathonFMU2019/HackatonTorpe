using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AspiradorBehaviour : MonoBehaviour
{

    [SerializeField] private Transform[] pointsToGo;
    private NavMeshAgent agent;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoNextPoint();
    }

  
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance <= 0.5)
        {
            GoNextPoint();
        }
    }

    void GoNextPoint()
    {
        var destPoint = Random.Range(0, pointsToGo.Length);
        if (pointsToGo == null)
        {
            return;
        }
        agent.SetDestination(pointsToGo[destPoint].position);
    }
}
