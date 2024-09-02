using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//반드시 필요한 컴포넌트를 명시해 해당 컴포넌트가 삭제되는 것을 방지하는 어트리뷰트
[RequireComponent(typeof(AudioSource))]//이거 있으면 인스펙터에서 삭제 불가
public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;

    public AudioClip fireSfx;//오디오 음원

    private new AudioSource audio;
    private MeshRenderer muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //FirePos 하위에 MuzzleFlash의 Material컴포넌트를 가져옴.
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        //enabled:활성화, 처음 시작할 때 비활성화.
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
        //Bullet프리팹을 동적?으로 생성(생성할 객체,위치,회전)중요!
        Instantiate(bullet, firePos.position, firePos.rotation);
        //총소리 발생, 1.0f초
        audio.PlayOneShot(fireSfx, 1.0f);
        //총구 화염 효과 코루틴?함수 호출
        StartCoroutine(ShowMuzzleFlash());
    }
    //총구 화염 효과 코루틴?함수 호출
    IEnumerator ShowMuzzleFlash()
    {
        //오프셋 좌표값을 랜덤 함수로 생성
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        //첵스처의 오프셋 값 설정
        muzzleFlash.material.mainTextureOffset = offset;

        //muzzleFlash의 회전 변경
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        //muzzleFlash의 크기 조절
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        //MuzzleFlash활성화
        muzzleFlash.enabled = true;
        //0.2초 동안 대기(정지)하는 동안 메시지 루프로 제어권을 양보
        yield return new WaitForSeconds(0.2f);
        //muzzleFlash비활성화
        muzzleFlash.enabled = false;
    }
}
