using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYGizmos : MonoBehaviour
{
    public Color color = Color.yellow;
    public float radius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;//기즈모 색상 설정
        Gizmos.DrawSphere(transform.position, radius);//구체 모양의 기즈모, 생성인자는 (생성위치,반지름)
    }
}
