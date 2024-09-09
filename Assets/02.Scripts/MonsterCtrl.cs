using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//�׺���̼� ������ ����.


public class MonsterCtrl : MonoBehaviour
{
    #region MonsterStatus
    public enum State
    {//���� ���� ����
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }
    public State state = State.IDLE;//������ ���� ����.
    public float traceDist = 10.0f;//���� �����Ÿ�.
    public float attackDist = 2.0f;//���� �����Ÿ�.
    public bool isDie = false;//���� ��� ����.
    #endregion

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;//�׺���̼�?
    private Animator anim;

    //Animator parameter�� Hash�� ����,361p����.
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");  
    private GameObject bloodEffect;//����ȿ��prefab

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        //�������(PLAYER)�� Transform�Ҵ�
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        //�׺���̼��� �������� player�� ��ġ��.
        //agent.destination = playerTr.position;

        //bloodEffect Prefab�ҷ�����
        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");
        //������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ�.
        StartCoroutine(CheckmonsterState());
        //������ �ൿ�� üũ�ϴ� �ڷ�ƾ �Լ�.
        StartCoroutine(MonsterAction());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            //�浹�� �Ѿ��� ����/
            Destroy(collision.gameObject);
            anim.SetTrigger(hashHit);//�ǰݸ��׼� ����.

            Vector3 pos = collision.GetContact(0).point;//�Ѿ��� �浹 ����
            Quaternion rot = Quaternion.LookRotation(-collision.GetContact(0).normal);//�Ѿ��� �浹 ������ ��������
            ShowBloodEffect(pos, rot);//����ȿ������Ʈ �Լ� ȣ��
        }
    }
    void ShowBloodEffect(Vector3 pos, Quaternion rot)
    {//���� ȿ�� ����
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
                    agent.isStopped = true;//��������.
                    anim.SetBool(hashTrace, false);//Animator�� IsTrace������ false�� ����.
                    break;

                case State.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;

                case State.TRACE://CheckmonsterState()���� TRACE���� �޾�
                    agent.SetDestination(playerTr.position);//�÷��̾� ��ġ�� Destination�Ѵ�.
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);//Animator�� IsTrace������ true�� ����.
                    anim.SetBool(hashAttack, false);
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
            //0.3�� �� ����(���)�ϴ� ���� ������� �޽��� ������ �纸.
            yield return new WaitForSeconds(0.3f);
            //���Ϳ� �÷��̾� ������ �Ÿ� ����
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if(distance <= attackDist)
            {//���� �����Ÿ� ������ ���Դ��� Ȯ��.
                state = State.ATTACK;
            }
            else if(distance <= traceDist)
            {//���� �����Ÿ� ������ ���Դ��� Ȯ��.
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
        {//���� �����Ÿ� ǥ��
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        if(state == State.ATTACK)
        {//���� �����Ÿ� ǥ��
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    void Update()
    {
        
    }
}
