using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]private Transform tr;//컴포넌트를 캐시 처리할 변수
    public float moveSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();//trancform 컴포넌트를 추출해 변수에 대입
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Debug.Log("h =" + h);//문자열에 넣을 때 float값이 문자열로 바뀌나?,암시적 형변환.
        Debug.Log("v =" + v);

        /*1. transform 컴포넌트의 위치를 변경.
        transform.position += new Vector3(0, 0, 1);

        2. 정규화 벡터를 사용한 코드. 왜 이놈은 new안쓰냐?
        transform.position += Vector3.forward * 1 * Time.deltaTime;

        3. transform컴포넌트의 캐시 처리.책 135p*/
        //tr.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime);

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);//전후좌우 이동 방향 벡터계산
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        //Translate(이동방향 * 이동속다 * time.deltatime)147p참조
    }
}
