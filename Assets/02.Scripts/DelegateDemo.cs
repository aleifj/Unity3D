using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateDemo : MonoBehaviour
{
    delegate float SumHandler(float a, float b);//��������Ʈ �⺻ ����.
    SumHandler sumHandler;//���� ��������Ʈ�� ���

    float Sum(float a, float b)//��������Ʈ�� ������ ���� �Լ�.
    {
        return a + b;
    }
    float Minus (float a, float b)//��������Ʈ�� ������ ���� �Լ�2.
    {
        return a - b;
    }
    void Start()
    {
        sumHandler = Sum;//������ ���� �Լ��� ��������Ʈ�� �Ҵ�.
        float sum = sumHandler(10.0f, 5.0f);//�Ҵ�� ��������Ʈ�� �Ķ���͸� �����ϰ�,�װ��� sum�̶� ���.
        Debug.Log($"Sum = {sum}");

        sumHandler = Minus;
        float minus = sumHandler(10.0f, 5.0f);
        Debug.Log($"Minus = {minus}");
        //��Ƽ�ɽ���
        sumHandler += Sum;
        sumHandler += Minus;
    }
}
