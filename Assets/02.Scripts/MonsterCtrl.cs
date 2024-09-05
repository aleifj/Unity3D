using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//네비게이션 쓰려고 넣음.


public class MonsterCtrl : MonoBehaviour
{
    #region MonsterStatus
    public enum State
    {//몬스터 상태 정보
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }
    public State state = State.IDLE;//몬스터의 현재 상태.
    public float traceDist = 10.0f;//추적 사정거리.
    public float attackDist = 2.0f;//공격 사정거리.
    public bool isDie = false;//몬스터 사망 여부.
    #endregion

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;//네비게이션?
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        //추적대상(PLAYER)의 Transform할당
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        //네비게이션의 목적지는 player의 위치값.
        //agent.destination = playerTr.position;

        //몬스터의 상태를 체크하는 코루틴 함수.
        StartCoroutine(CheckmonsterState());
        //몬스터의 행동를 체크하는 코루틴 함수.
        StartCoroutine(MonsterAction());
    }
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    agent.isStopped = true;//추적중지.
                    anim.SetBool("IsTrace", false);//Animator의 IsTrace변수를 false로 설정.
                    break;
                case State.ATTACK:
                    break;
                case State.TRACE://CheckmonsterState()에서 TRACE값을 받아
                    agent.SetDestination(playerTr.position);//플래이어 위치를 Destination한다.
                    agent.isStopped = false;
                    anim.SetBool("IsTrace", true);//Animator의 IsTrace변수를 true로 설정.
                    break;
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    IEnumerator CheckmonsterState()
    {
        while (!isDie)
        {
            //0.3초 간 중지(대기)하는 동안 제어권을 메시지 루프에 양보.
            yield return new WaitForSeconds(0.3f);
            //몬스터와 플레이어 사이의 거리 측정
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if(distance <= attackDist)
            {//공격 사정거리 범위로 들어왔는지 확인.
                state = State.ATTACK;
            }
            else if(distance <= traceDist)
            {//추적 사정거리 범위로 들어왔는지 확인.
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if(state == State.TRACE)
        {//추적 사정거리 표시
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        if(state == State.ATTACK)
        {//공격 사정거리 표시
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
