using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]private Transform tr;//������Ʈ�� ĳ�� ó���� ����
    public float moveSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();//trancform ������Ʈ�� ������ ������ ����
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Debug.Log("h =" + h);//���ڿ��� ���� �� float���� ���ڿ��� �ٲ?,�Ͻ��� ����ȯ.
        Debug.Log("v =" + v);

        /*1. transform ������Ʈ�� ��ġ�� ����.
        transform.position += new Vector3(0, 0, 1);

        2. ����ȭ ���͸� ����� �ڵ�. �� �̳��� new�Ⱦ���?
        transform.position += Vector3.forward * 1 * Time.deltaTime;

        3. transform������Ʈ�� ĳ�� ó��.å 135p*/
        //tr.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime);

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);//�����¿� �̵� ���� ���Ͱ��
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        //Translate(�̵����� * �̵��Ӵ� * time.deltatime)147p����
    }
}
