using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;//������Ʈ�� ĳ�� ó���� ����
    private Animation anim;

    public float moveSpeed = 10.0f;
    public float turnSpeed = 80.0f;

    private readonly float initHP = 100.0f;//�ʱ�HP ��
    public float currHP;//����HP ��
    private Image hpBar;//HPBar���� �� ����.

    public delegate void PlayerDieHandler();//��������Ʈ ����.
    public static event PlayerDieHandler OnPlayerDie;//�̺�Ʈ ����.

    IEnumerator Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();//HpBar����,? ������. 487p����.
        currHP = initHP;//HP�ʱ�ȭ
        
        tr = GetComponent<Transform>();//trancform ������Ʈ�� ������ ������ ����
        anim = GetComponent<Animation>();

        anim.Play("Idle");//�ִϸ��̼� ����.

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");//ī�޶��� ȸ���� ���콺�� X, Y �Է� ���� �޾ƿ��� ��

        /*1. transform ������Ʈ�� ��ġ�� ����.
         transform.position += new Vector3(0, 0, 1);
       2. ����ȭ ���͸� ����� �ڵ�. �� �̳��� new�Ⱦ���?
        transform.position += Vector3.forward * 1 * Time.deltaTime;
         3. transform������Ʈ�� ĳ�� ó��.å 135p
       tr.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime);*/

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);//�����¿� �̵� ���� ���Ͱ��
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        //Translate(�̵����� * �̵��ӵ� * time.deltatime)147p����
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        PlayerAnim(h, v);//ĳ���� �ִϸ��̼� ����.
    }
    void PlayerAnim(float h, float v)
    {//Ű���� �Է°��� �������� ���� �� �ִϸ��̼� ����
        if (v >= 0.1f)
        {//����
            anim.CrossFade("RunF", 0.25f);
        }
        else if (v <= -0.1f)
        {//����
            anim.CrossFade("RunB", 0.25f);
        }
        else if (h >= 0.1f)
        {//��
            anim.CrossFade("RunR", 0.25f);
        }
        else if (h <= -0.1f)
        {//��
            anim.CrossFade("RunL", 0.25f);
        }
        else 
        {//����
            anim.CrossFade("Idle", 0.25f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(currHP >= 0.0f && other.CompareTag("PUNCH"))
        {//�浹�� collider�� ������ PUNCH��� 
            currHP -= 10.0f;//����HP 10����
            DisplayHealth();
            Debug.Log($"Player HP = {currHP / initHP}");
            if(currHP <= 0.0f)
            {
                PlayerDie();
            }
        }
    }
    void PlayerDie()
    {
        Debug.Log("�� ����");

        /*GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");//MONSTER�±׸� ���� ��� ���ӿ�����Ʈ�� �迭�� ã��.
        foreach(GameObject monster in monsters)
        {//��� ������ OnPlayerDie�Լ��� ���������� ȣ��.
            monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }*/
        OnPlayerDie();//���ΰ� ��� �̺�Ʈ ȣ��(�߻�).
    }
    void DisplayHealth()
    {
        hpBar.fillAmount = currHP / initHP;
    }
}
