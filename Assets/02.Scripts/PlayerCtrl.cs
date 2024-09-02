using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;//컴포넌트를 캐시 처리할 변수
    private Animation anim;

    public float moveSpeed = 10.0f;
    public float turnSpeed = 80.0f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        tr = GetComponent<Transform>();//trancform 컴포넌트를 추출해 변수에 대입
        anim = GetComponent<Animation>();

        anim.Play("Idle");//애니메이션 실행.

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");//카메라의 회전을 마우스의 X, Y 입력 값을 받아오는 것


        /*1. transform 컴포넌트의 위치를 변경.
         transform.position += new Vector3(0, 0, 1);

       2. 정규화 벡터를 사용한 코드. 왜 이놈은 new안쓰냐?
        transform.position += Vector3.forward * 1 * Time.deltaTime;

         3. transform컴포넌트의 캐시 처리.책 135p
       tr.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime);*/

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);//전후좌우 이동 방향 벡터계산
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        //Translate(이동방향 * 이동속도 * time.deltatime)147p참조
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        PlayerAnim(h, v);//캐릭터 애니메이션 설정.
    }
    void PlayerAnim(float h, float v)
    {//키보드 입력값을 기준으로 동작 할 애니메이션 수행
        if (v >= 0.1f)
        {//전진
            anim.CrossFade("RunF", 0.25f);
        }
        else if (v <= -0.1f)
        {//후진
            anim.CrossFade("RunB", 0.25f);
        }
        else if (h >= 0.1f)
        {//우
            anim.CrossFade("RunR", 0.25f);
        }
        else if (h <= -0.1f)
        {//좌
            anim.CrossFade("RunL", 0.25f);
        }
        else 
        {//정지
            anim.CrossFade("Idle", 0.25f);
        }
    }
}
