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
    private GameObject bloodEffect;//혈흔효과prefab

    //Animator parameter의 Hash값 추출,361p참조.
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");

    private int hp = 100;

    private void OnEnable()
    {//스크립트가 활성화 될 때 마다 호출되는 함수.이벤트 연결 할거임.
        PlayerCtrl.OnPlayerDie += this.OnPlayerDie;//이벤트 발생 시 수행할 함수 연결.this생략 가능.
    }
    private void OnDisable()
    {//스크립트가 비활성화 될 때 마다 호출되는 함수.이벤트 연결 끊을거임.
        PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;//연결된 함수 해제.this생략 가능.
    }

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        //추적대상(PLAYER)의 Transform할당
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        /*네비게이션의 목적지는 player의 위치값.
          agent.destination = playerTr.position;*/

        //bloodEffect Prefab불러오기
        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");
        //몬스터의 상태를 체크하는 코루틴 함수.
        StartCoroutine(CheckmonsterState());
        //몬스터의 행동를 체크하는 코루틴 함수.
        StartCoroutine(MonsterAction());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            //충돌한 총알을 삭제/
            Destroy(collision.gameObject);
            anim.SetTrigger(hashHit);//피격리액션 실행.

            Vector3 pos = collision.GetContact(0).point;//총알의 충돌 지점
            Quaternion rot = Quaternion.LookRotation(-collision.GetContact(0).normal);//총알의 충돌 지점의 법선벡터
            ShowBloodEffect(pos, rot);//혈흔효과이펙트 함수 호출

            hp -= 10;
            if (hp <= 0)
            {
                state = State.DIE;
            }
        }
    }
    void ShowBloodEffect(Vector3 pos, Quaternion rot)
    {//혈흔 효과 생성
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot, monsterTr);
        Destroy(blood, 1.0f);
    }
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    agent.isStopped = true;//추적중지.
                    anim.SetBool(hashTrace, false);//Animator의 IsTrace변수를 false로 설정.
                    break;

                case State.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;

                case State.TRACE://CheckmonsterState()에서 TRACE값을 받아
                    agent.SetDestination(playerTr.position);//플래이어 위치를 Destination한다.
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);//Animator의 IsTrace변수를 true로 설정.
                    anim.SetBool(hashAttack, false);
                    break;

                case State.DIE:
                    isDie = true;
                    agent.isStopped = true;
                    anim.SetTrigger(hashDie);
                    GetComponent<CapsuleCollider>().enabled = false;//몬스터의 Collider Component 비활성화.
                    SphereCollider[] sc = this.GetComponentsInChildren<SphereCollider>();//몬스터 공격수단 비활성화.
                    foreach(var item in sc)
                    {
                        item.enabled = false;
                    }
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

            if (state == State.DIE) yield break;
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
    private void OnPlayerDie()
    {
        StopAllCoroutines();//몬스터의 상태를 체크하는 코루틴 함수를 모두 정지.

        agent.isStopped = true;//몬스터가 위치추적 정지.
        anim.SetFloat(hashSpeed, Random.Range(0.8f, 1.2f));//애니메이션 재생속도 조절.
        anim.SetTrigger(hashPlayerDie);//티배깅 애니메이션 실행.
    }
}
