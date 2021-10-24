using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterDidi : MonoBehaviour
{
    
    public MonsterProfile monsterProfile;
    private NavMeshAgent _agent;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Goal;

    [SerializeField] private float DidiDistanceRun = 4.0f;
    [SerializeField] private float DidiDistanceGoal = 8.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        float distanceToGoal = Vector3.Distance(transform.position, Goal.transform.position);

        Debug.Log("Distance" + distanceToPlayer);

        if(distanceToGoal < DidiDistanceGoal){
            Vector3 dirToGoal = transform.position - Goal.transform.position;
            Vector3 newPos = transform.position - dirToGoal;
            _agent.SetDestination(newPos);
        }
        else if(distanceToPlayer < DidiDistanceRun){
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            _agent.SetDestination(newPos);
        }
    }
}
