using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;//������Ʈ�� ĳ�� ó���� ����
    private Animation anim;

    public float moveSpeed = 10.0f;
    public float turnSpeed = 80.0f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        tr = GetComponent<Transform>();//trancform ������Ʈ�� ������ ������ ����
        anim = GetComponent<Animation>();

        anim.Play("Idle");//�ִϸ��̼� ����.

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    // Update is called once per frame
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
}
