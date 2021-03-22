using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeRotates : MonoBehaviour
{
    public string currentMapName; //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.
    public inventory Inven;//인벤토리.
    public Animator anim;//애니메이터.
    
    public GameObject hoe; // 미해 ( 스크립트 위치 grinder->player 옮김, 게임오브젝트 넣어서 낫 제어로 바꿈 ( 안그러면 낫 작동 안해서..!)


    // Start is called before the first frame update
    void Start()
    {
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        anim = hoe.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Inven.equipedItem != null)//장착한 아이템이 빈칸이 아니고
        {
            if (Inven.equipedItem.Ename == "Hoe")//호미이면
            {
             
                hoe.SetActive(true);//호미를 보이게 하고
                if (Input.GetMouseButton(0))//마우스 클릭중에는 호미가 돌아가는 애니메이션 재생.
                {
                    anim.SetBool("HoeOn", true);
                }
                else//마우스 클릭 안 하면 애니메이션 재생 안 함.
                {
                    anim.SetBool("HoeOn", false);
                }
            }
            else//장착한 아이템이 호미가 아닌 경우 호미를 끔.
            {
                
                hoe.SetActive(false);
            }
        }
        else//장착한 아이템이 없을 경우 호미를 끔.
        {
            //  Debug.Log("선택된 아이템 없음");
            hoe.SetActive(false);
        }
    }
}

