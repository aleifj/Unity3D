using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {//�浹�� ������ �� �߻��ϴ� �̺�Ʈ
        if(collision.collider.CompareTag("BULLET"))
        {//�浹�� ���ӿ�����Ʈ�� �±װ� ��
            Destroy(collision.gameObject);
        }
    }
}
