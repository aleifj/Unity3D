using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform targetTr;//���󰡾��� ����� ���� �� ����.
    private Transform camTr;//ī�޶� �ڽ��� transform.

    [Range(2.0f, 20.0f)] public float dictance = 10.0f;//���� ������κ��� ������ �Ÿ�
    [Range(0.0f, 10.0f)] public float height = 2.0f;//Y������ �̵��� ����

    public float damping = 10.0f;//���� �ӵ�
    public float targetOffset = 2.0f;//ī�޶�LookAt�� Offset��
    private Vector3 velocity = Vector3.zero;//smoothDamp���� ����� ����.
    // Start is called before the first frame update
    void Start()
    {
        camTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {   
     //�����ؾ� �� ����� �ڷ� distance��ŭ �̵�
     //���̸� height��ŭ �̵�.
        Vector3 pos = targetTr.position + (-targetTr.forward * dictance) + (Vector3.up * height);
        //ī�޶� ��ġ = Ÿ����ġ + (Ÿ���� ���� ���� * ������ �Ÿ�) + (Y�� ���� * ����);

        /*���鼱�� �����Լ��� ����� �ε巴�� ��ġ�� ����
        camTr.position = Vector3.Slerp(camTr.position,                      ������ġ
                                                  pos,                      ��ǥ��ġ            
                                                  Time.deltaTime * damping);�ð� t*/

        //SmoothDamp�� �̿��� ��ġ ����
        camTr.position = Vector3.SmoothDamp(camTr.position,//������ġ
                                            pos,           //��ǥ��ġ
                                            ref velocity,  //����ӵ�
                                            damping);      //��ǥ ��ġ���� ������ �ð�.

        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));//camera�� �ǹ� ��ǥ�� ���� ȸ��.
    }
}
