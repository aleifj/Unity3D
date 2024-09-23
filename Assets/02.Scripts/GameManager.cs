using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();//몬스터가 출현할 위치를 저장할 List타입 변수.
    public GameObject Monster;
    public float createTime = 3.0f;
    private bool isGameOver;//게임 종료 여부 변수.
    public bool IsGameOver
    {//게임 종료 여부를 저장할 프로퍼티.
        get { return isGameOver; }
        set
        {
            isGameOver = value;//set에 값을 넣을때 꼭 value해줘야댐.
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }
    public static GameManager instance = null;
    private void Awake()
    {
        if(instance == null)//instance가 할당되지 않았을 경우.
        {
            instance = this;
        }
        else if(instance != this)//instance에 할당된 클래스의 인스턴스가 다른경우 새로 생성된 클래스를 의미.
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);//다른 scene으로 넘어가도라도 삭제하지 않고 유지함.510p참고.
    }

    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;//SpawnPointGroup게임오브젝트의 Transform컴포넌트 추출.플레이하면 스폰포인트 자동으로 들어감.

        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);아래를 이렇게도 쓸 수 있음.
        foreach(Transform point in spawnPointGroup)
        {//SpawnPointGroup하위에 있는 모든 차일드 게임오브젝트의 Transform컴포넌트 추출.
            points.Add(point);
        }
        InvokeRepeating("CreateMonster",2.0f,createTime);//일정 간격으로 함수 호출.
    }

    void CreateMonster()
    {
        int idx=Random.Range(0,points.Count);//몬스터의 불규칙한 생성 위치.
        Instantiate(Monster,points[idx].position,points[idx].rotation);//몬스터 프리팹 생성.
    }
    void Update()
    {
        
    }
}
