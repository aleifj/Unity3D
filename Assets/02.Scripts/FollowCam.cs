using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform targetTr;//따라가야할 대상을 연결 할 변수.
    private Transform camTr;//카메라 자신의 transform.

    [Range(2.0f, 20.0f)] public float dictance = 10.0f;//따라갈 대상으로부터 떨어질 거리
    [Range(0.0f, 10.0f)] public float height = 2.0f;//Y축으로 이동할 높이

    public float damping = 10.0f;//반응 속도
    public float targetOffset = 2.0f;//카메라LookAt의 Offset값
    private Vector3 velocity = Vector3.zero;//smoothDamp에서 사용할 변수.
    // Start is called before the first frame update
    void Start()
    {
        camTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {   
     //추적해야 할 대상의 뒤로 distance만큼 이동
     //높이를 height만큼 이동.
        Vector3 pos = targetTr.position + (-targetTr.forward * dictance) + (Vector3.up * height);
        //카메라 위치 = 타겟위치 + (타깃의 뒤쪽 방향 * 떨어질 거리) + (Y축 방향 * 높이);

        /*구면선형 보간함수를 사용해 부드럽게 위치를 변경
        camTr.position = Vector3.Slerp(camTr.position,                      시작위치
                                                  pos,                      목표위치            
                                                  Time.deltaTime * damping);시간 t*/

        //SmoothDamp를 이용한 위치 보간
        camTr.position = Vector3.SmoothDamp(camTr.position,//시작위치
                                            pos,           //목표위치
                                            ref velocity,  //현재속도
                                            damping);      //목표 위치까지 도달할 시간.

        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));//camera를 피벗 좌표를 향해 회전.
    }
}
