using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterCtrl : MonoBehaviour
{
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();

        //추적대상(PLAYER)의 Transform할당
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        //네비게이션의 목적지는 player의 위치값.
        agent.destination = playerTr.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
