using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();//몬스터가 출현할 위치를 저장할 List타입 변수.
    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;//SpawnPointGroup게임오브젝트의 Transform컴포넌트 추출.

        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);아래를 이렇게도 쓸 수 있음.
        foreach(Transform point in spawnPointGroup)
        {//SpawnPointGroup하위에 있는 모든 차일드 게임오브젝트의 Transform컴포넌트 추출.
            points.Add(point);
        }
    }

    void Update()
    {
        
    }
}
