using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�ݵ�� �ʿ��� ������Ʈ�� ����� �ش� ������Ʈ�� �����Ǵ� ���� �����ϴ� ��Ʈ����Ʈ
[RequireComponent(typeof(AudioSource))]//�̰� ������ �ν����Ϳ��� ���� �Ұ�
public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;

    public AudioClip fireSfx;//����� ����

    private new AudioSource audio;
    private MeshRenderer muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //FirePos ������ MuzzleFlash�� Material������Ʈ�� ������.
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        //enabled:Ȱ��ȭ, ó�� ������ �� ��Ȱ��ȭ.
        muzzleFlash.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    void Fire()
    {
        //Bullet�������� ����?���� ����(������ ��ü,��ġ,ȸ��)�߿�!
        Instantiate(bullet, firePos.position, firePos.rotation);
        //�ѼҸ� �߻�, 1.0f��
        audio.PlayOneShot(fireSfx, 1.0f);
        //�ѱ� ȭ�� ȿ�� �ڷ�ƾ?�Լ� ȣ��
        StartCoroutine(ShowMuzzleFlash());
    }
    //�ѱ� ȭ�� ȿ�� �ڷ�ƾ?�Լ� ȣ��
    IEnumerator ShowMuzzleFlash()
    {
        //MuzzleFlashȰ��ȭ
        muzzleFlash.enabled = true;
        //0.2�� ���� ���(����)�ϴ� ���� �޽��� ������ ������� �纸
        yield return new WaitForSeconds(0.2f);
        //muzzleFlash��Ȱ��ȭ
        muzzleFlash.enabled = false;
    }
}
