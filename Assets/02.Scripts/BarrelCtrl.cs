using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    #region Public
    public GameObject expEffect;
    #endregion

    Transform tr;
    Rigidbody rb;

    int hitCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// �浹 �� �߻��ϴ� �ݹ��Լ�
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            if(++hitCount == 3)
            {//��ġ������
                ExpBarrel();
            }
        }
    }
    void ExpBarrel()
    {
        //���� ȿ�� ��ƼŬ ����
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        Destroy(exp, 5.0f);
        //rigidbody������Ʈ�� mass�� 1.0���� ������ ���Ը� ������ ��
        rb.mass = 1.0f;
        rb.AddForce(Vector3.up * 1500.0f);//������ �� ���� �ڱ�ħ
        Destroy(gameObject, 3.0f);
    }
}
