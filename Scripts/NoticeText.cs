using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeText : MonoBehaviour
{
    public Text noticeText;
    void Start()
    {

        noticeText = GameObject.Find("Notice").transform.Find("NoticeText").GetComponent<Text>();
        WriteMessage("안뇽");

        //다른 스크립트에서 알림을 쓰고 싶을땐
        // public NoticeText notice;
        //notice = GameObject.Find("Notice").GetComponent<NoticeText>();
        //notice.WriteMessage("원하는거")



    }

    public void WriteMessage(string message)
    {
        noticeText.text = message;
        noticeText.gameObject.SetActive(true);
        Invoke("DeleteMessage", 5f);
    }

 

    
    void DeleteMessage()
    {
        noticeText.gameObject.SetActive(false);
    }

}
