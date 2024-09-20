using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarrelCtrl : MonoBehaviour
{
    #region Public
    public GameObject expEffect;
    public Texture[] textures;//무작위 텍스쳐 배열 적용.
    public float radius = 10.0f;//폭발반경
    #endregion

    #region private
    MeshRenderer rendere;
    Transform tr;
    Rigidbody rb;
    #endregion

    int hitCount = 0;//총알 맞은 횟수 누적시킬 변수
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        //하위에 있는 MeshRenderer컴포넌트 추출
        rendere = GetComponentInChildren<MeshRenderer>();

        int idx = Random.Range(0, textures.Length);//난수 발생
        rendere.material.mainTexture = textures[idx];//텍스터 지정
    }
    /// <summary>
    /// 충돌 시 발생하는 콜백함수
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {//총알이 드럼통에 충돌시
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
        /*rb.mass = 1.0f;
        rb.AddForce(Vector3.up * 1500.0f);*///폭팔할 때 위로 솟구침
        IndirectDamage(tr.position);
        Destroy(gameObject, 3.0f);
    }
    Collider[] colls = new Collider[10];//결과값을 저장할 정적 배열을 미리 선언.
    void IndirectDamage(Vector3 pos)
    {
        //Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);//주변에 있는 드럼통 모두 추출(가비지 컬렉션이 발생)
                                                                      //비트연산? 3번 레이어라는 의미
        Physics.OverlapSphereNonAlloc(pos, radius, colls, 1 << 3);//가비지 컬렉션 발생 안함

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
