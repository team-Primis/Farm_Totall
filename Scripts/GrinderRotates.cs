
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderRotates : MonoBehaviour
{
    public string currentMapName; //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.
    public inventory Inven;//인벤토리.
    public Animator anim;//낫 애니메이션.
    public float speed = 1.0f;//회전 속도용.
    public GameObject grinder; // 미해 ( 스크립트 위치 grinder->player 옮김, 게임오브젝트 넣어서 낫 제어로 바꿈 ( 안그러면 낫 작동 안해서..!)


    // Start is called before the first frame update
    void Start()
    {
        Inven= GameObject.Find("Inventory").GetComponent<inventory>();
        anim = grinder.GetComponent<Animator>();//Child로 넣어야 첨부터 Setactive false인 걸 가져오기 용이해서 그라인더를 Child로 넣고 컴포넌트 가져옴.
    }

    // Update is called once per frame
    void Update()
    {
        if (Inven.equipedItem != null)//장비한 아이템 칸이 비지 않았고
        {
            if (Inven.equipedItem.Ename == "sickle")//장비한 아이템의 이름이 "낫"이면
            {
               // Debug.Log("낫이 생겼습니다");
                grinder.SetActive(true);//낫 오브젝트를 눈에 보이게 하고
                if (Input.GetMouseButton(0))//마우스 왼클릭중에는 낫이 돌아가는 애니메이션 재생.
                {
                    anim.SetBool("GrinderOn", true);
                }
                else//마우스 클릭 안 하면 애니메이션 재생 안 함.
                {
                    anim.SetBool("GrinderOn", false);
                }
            }
            else//장비한 아이템의 이름이 낫이 아닌 경우에는 낫을 안 보이게 함.
            {
               // Debug.Log("낫이 없어졌습니다");
                grinder.SetActive(false);
            }
        }
        else//장비한 아이템 칸이 빈 경우에도 낫을 안 보이게 함.
        {
          //  Debug.Log("선택된 아이템 없음");
            grinder.SetActive(false);
        }
    }
}
