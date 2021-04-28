using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotWalker : MonoBehaviour
{
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f)));
    }
    private void Update()
    {
        agent.SetDestination(agent.destination + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-8f, 8f)));
    }
}
