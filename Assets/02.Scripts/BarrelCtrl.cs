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
    /// 충돌 시 발생하는 콜백함수
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            if(++hitCount == 3)
            {//전치연산자
                ExpBarrel();
            }
        }
    }
    void ExpBarrel()
    {
        //폭발 효과 파티클 생성
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        Destroy(exp, 5.0f);
        //rigidbody컴포넌트의 mass를 1.0으로 수정해 무게를 가볍게 함
        rb.mass = 1.0f;
        rb.AddForce(Vector3.up * 1500.0f);//폭팔할 때 위로 솟구침
        Destroy(gameObject, 3.0f);
    }
}
