using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;
    private void OnCollisionEnter(Collision collision)
    {//충돌이 시작할 때 발생하는 이벤트
        if(collision.collider.CompareTag("BULLET"))
        {/* 충돌한 게임오브젝트의 태그값 비교*/
            //첫 번째 충돌 지점의 정보 추출.
            ContactPoint cp = collision.GetContact(0);
            //충돌한 총알의 법선 벡터를 쿼터니언 타입으로 변환.
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            //스파크 파티클 동적 생성
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            //충돌 후 삭제
            Destroy(collision.gameObject);
            Destroy(spark, 0.5f);
        }
    }
}
