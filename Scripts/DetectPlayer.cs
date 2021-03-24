using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private GameManager GMScript;
    public bool isEntered = false;
    public GameObject panelToOpen;
    public string objectTag;
    public bool isOpened = false;

    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        objectTag = gameObject.tag;

        panelToOpen = GameObject.Find("Canvas2").transform.Find(objectTag+"Panel").gameObject;
    }

    void Update()
    {
        //콜라이더 안에 들어와서 스페이스바를 누르면 열렸다 닫혔다
        if (isEntered)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("touched " + objectTag);
                
                panelToOpen.SetActive(!panelToOpen.activeSelf);
                GMScript.isTimerStoped = panelToOpen.activeSelf;
                GMScript.isSleepOpen = panelToOpen.activeSelf;
                isOpened = panelToOpen.activeSelf;



            }
        }
    }

    //가까이 가면 isEntered 가 true가 됨 -> 콜라이더 안에 들어왔다고 간접적으로 알려줌
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isEntered = true;

        }

    }

    //해당 범위에서 나가면  isEntered = false -> 해당 범위에서 나가갔다는 뜻 + 타이머 흐르게, active 없애기
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isEntered = false;
            panelToOpen.SetActive(false);
            GMScript.isTimerStoped = false;
            isOpened = false;
        }
    }

}
