using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {//충돌이 시작할 때 발생하는 이벤트
        if(collision.collider.CompareTag("BULLET"))
        {//충돌한 게임오브젝트의 태그값 비교
            Destroy(collision.gameObject);
        }
    }
}
