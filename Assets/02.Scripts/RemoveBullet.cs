using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;
    private void OnCollisionEnter(Collision collision)
    {//�浹�� ������ �� �߻��ϴ� �̺�Ʈ
        if(collision.collider.CompareTag("BULLET"))
        {/* �浹�� ���ӿ�����Ʈ�� �±װ� ��*/
            //ù ��° �浹 ������ ���� ����.
            ContactPoint cp = collision.GetContact(0);
            //�浹�� �Ѿ��� ���� ���͸� ���ʹϾ� Ÿ������ ��ȯ.
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            //����ũ ��ƼŬ ���� ����
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            //�浹 �� ����
            Destroy(collision.gameObject);
            Destroy(spark, 0.5f);
        }
    }
}
