using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateDemo : MonoBehaviour
{
    delegate float SumHandler(float a, float b);//델리게이트 기본 형식.
    SumHandler sumHandler;//위의 델리게이트의 명명

    float Sum(float a, float b)//델리게이트와 형식을 맟춘 함수.
    {
        return a + b;
    }
    float Minus (float a, float b)//델리게이트와 형식을 맟춘 함수2.
    {
        return a - b;
    }
    void Start()
    {
        sumHandler = Sum;//형식을 맟춘 함수를 델리게이트에 할당.
        float sum = sumHandler(10.0f, 5.0f);//할당된 델리게이트에 파라미터를 지정하고,그것을 sum이라 명명.
        Debug.Log($"Sum = {sum}");

        sumHandler = Minus;
        float minus = sumHandler(10.0f, 5.0f);
        Debug.Log($"Minus = {minus}");
        
        //멀티케스팅
        sumHandler += Sum;
        sumHandler += Minus;

        //람다식으로 길이를 줄여보자.
        sumHandler = (float a, float b) => (a + b);
        float sum2 = sumHandler(10.0f, 5.0f);
        Debug.Log($"Sum2 = {sum2}");

        //무명 메서드로 길이를 줄여보자/
        sumHandler = delegate (float a, float b) { return a + b; };//세미클론 주의
        float sum3 = sumHandler(2.0f, 3.0f);
        Debug.Log($"Sum3 = {sum3}");

        sumHandler = (float a, float b) => (a - b);//람다식 빼기버젼.
    }
}
