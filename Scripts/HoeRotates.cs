using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeRotates : MonoBehaviour
{
    public string currentMapName; //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.
    public inventory Inven;
    public Animator anim;
    public float speed = 1.0f;
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
        if (Inven.equipedItem != null)
        {
            if (Inven.equipedItem.Ename == "Hoe")
            {
                // Debug.Log("낫이 생겼습니다");
                hoe.SetActive(true);
                if (Input.GetMouseButton(0))//마우스 클릭중에는 낫이 돌아가는 애니메이션 재생.
                {
                    anim.SetBool("HoeOn", true);
                }
                else//마우스 클릭 안 하면 애니메이션 재생 안 함.
                {
                    anim.SetBool("HoeOn", false);
                }
            }
            else
            {
                // Debug.Log("낫이 없어졌습니다");
                hoe.SetActive(false);
            }
        }
        else
        {
            //  Debug.Log("선택된 아이템 없음");
            hoe.SetActive(false);
        }
    }
}

