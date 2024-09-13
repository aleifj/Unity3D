using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class UIManager : MonoBehaviour
{
    public Button startButton;
    public Button optionButton;
    public Button shopButton;

    private UnityAction action;
    void Start()
    {
        //UnityAction을 사용한 이벤트 연결 방식.
        action = () => OnButtonClick(startButton.name);
        startButton.onClick.AddListener(action);
        //무명메서드를 사용한 이벤트 연결 방식, 세미클론 이상한데 들어가네...
        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); });
        //람다식을 사용한 이벤트 연결 방식.464p 참고
        shopButton.onClick.AddListener(() => OnButtonClick(shopButton.name));
    }

    void Update()
    {
        
    }
    public void OnButtonClick(string msg)
    {
        Debug.Log($"Click Button : {msg}");
    }
}
