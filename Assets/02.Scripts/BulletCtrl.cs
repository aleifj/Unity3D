using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float damage = 20.0f;
    public float force = 1500.0f;//총알의 힘.

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //총알의 전진 방향으로 힘을 가한다.
        rb.AddForce(transform.forward * force);
    }
    void Update()
    {
        
    }
}
