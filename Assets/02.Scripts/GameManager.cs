using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();//몬스터가 출현할 위치를 저장할 List타입 변수.

    public List<GameObject> monsterPool = new List<GameObject>();//몬스터를 미리 생성해 저장할 리스트 자료형.
    public int maxMonsters = 10;//오브젝트 풀에 생성할 몬스터의 최대 갯수.
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
        CreateMonsterPool();//몬스터 오브젝트 풀 생성.

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
        //Instantiate(Monster,points[idx].position,points[idx].rotation);몬스터 프리팹 생성.

        GameObject _monster = GetMonsterInPool();//오브젝트 풀에서 몬스터 추출.
        _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);//추출한 몬스터의 위치와 회전을 설정.
        _monster?.SetActive(true);//추출한 몬스터 활성화.
    }
    private void CreateMonsterPool()
    {
        for (int i = 0; i < maxMonsters; i++)
        {
            var _monster = Instantiate<GameObject>(Monster);//몬스터 생성.
            _monster.name = $"Monster_{i:00}";//몬스터가 만들어질 때 이름 지정.
            _monster.SetActive(false);//몬스터 비 활성화.

            monsterPool.Add(_monster);//생성한 몬스터를 오브젝트 풀에 추가.
        }
    }
    public GameObject GetMonsterInPool()//오브젝트 풀에서 사용 가능한 몬스터를 추출해 반환하는 함수.
    {
        foreach(var _monster in monsterPool)//오브젝트 풀의 처음부텉 끝까지 순회.
        {
            if(_monster.activeSelf == false)//비활성화 여부로 사용 가능한 몬스터를 판단
            {
                return _monster;
            }
        }
        return null;
    }
}
