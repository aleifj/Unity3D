using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] points;//몬스터가 출현할 위치를 저장할 배열.
    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;//SpawnPointGroup게임오브젝트의 Transform컴포넌트 추출.
        points = spawnPointGroup?.GetComponentsInChildren<Transform>();//SpawnPointGroup하위에 있는 모든 차일드 게임오브젝트의 Transform컴포넌트 추출.
    }

    void Update()
    {
        
    }
}
