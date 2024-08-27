using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarrelCtrl : MonoBehaviour
{
    #region Public
    public GameObject expEffect;
    public Texture[] textures;//������ �ؽ��� �迭 ����.
    public float radius = 10.0f;//���߹ݰ�
    #endregion

    #region private
    MeshRenderer rendere;
    Transform tr;
    Rigidbody rb;
    #endregion

    int hitCount = 0;//�Ѿ� ���� Ƚ�� ������ų ����
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        //������ �ִ� MeshRenderer������Ʈ ����
        rendere = GetComponentInChildren<MeshRenderer>();

        int idx = Random.Range(0, textures.Length);//���� �߻�
        rendere.material.mainTexture = textures[idx];//�ؽ��� ����
    }
    /// <summary>
    /// �浹 �� �߻��ϴ� �ݹ��Լ�
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {//�Ѿ��� �巳�뿡 �浹��
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
        /*rb.mass = 1.0f;
        rb.AddForce(Vector3.up * 1500.0f);*///������ �� ���� �ڱ�ħ
        IndirectDamage(tr.position);
        Destroy(gameObject, 3.0f);
    }
    Collider[] colls = new Collider[10];//������� ������ ���� �迭�� �̸� ����.
    void IndirectDamage(Vector3 pos)
    {
        //Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);//�ֺ��� �ִ� �巳�� ��� ����(������ �÷����� �߻�)
                                                                      //��Ʈ����? 3�� ���̾��� �ǹ�
        Physics.OverlapSphereNonAlloc(pos, radius, colls, 1 << 3);//������ �÷��� �߻� ����

        foreach (var coll in colls)
        {
            if(coll != null)
            {
                rb = coll.GetComponent<Rigidbody>();
                rb.mass = 1.0f;
                rb.constraints = RigidbodyConstraints.None;
                rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f); 
            }
            
        }
    }
}
