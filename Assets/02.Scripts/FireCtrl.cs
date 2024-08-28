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
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
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
    }
}
