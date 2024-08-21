using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYGizmos : MonoBehaviour
{
    public Color color = Color.yellow;
    public float radius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;//����� ���� ����
        Gizmos.DrawSphere(transform.position, radius);//��ü ����� �����, �������ڴ� (������ġ,������)
    }
}
