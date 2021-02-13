using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderRotates : MonoBehaviour
{
    public string currentMapName; //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.
    public inventory Inven;
    public Animator anim;
    public float speed = 1.0f;

    public GameObject grinder;

    // Start is called before the first frame update
    void Start()
    {

        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        anim = GetComponent<Animator>();


    }

    //이 코드 자체가 낫한테 붙어있기 때문에, 낫이 enable이 false가 되면 아예 작동을 안하는 것.
    //따라서 가장 처음에 플레이어가 아무것도 들고 있지 안하서 setactive false가 되고, 따라서 Update문이 실행이 안됨.
    void Update()
    {
        if (Inven.equipedItem != null)
        {
            if (Inven.equipedItem.Ename == "sickle")
            {
                //Debug.Log("낫이 생겼습니다");
                grinder.gameObject.SetActive(true);

                if (Input.GetMouseButton(0))//마우스 클릭중에는 낫이 돌아가는 애니메이션 재생.
                {
                    anim.SetBool("GrinderOn", true);
                }
                else//마우스 클릭 안 하면 애니메이션 재생 안 함.
                {
                    anim.SetBool("GrinderOn", false);
                }


            }
            else
            {
                // Debug.Log("낫이 없어졌습니다");
                grinder.gameObject.SetActive(false);
            }
        }
        else
        {
            //Debug.Log("선택된 아이템 없음");
            grinder.gameObject.SetActive(false);
        }
    }
}

/*
 * 
 * 
 public GameObject grinder;

  void Update()
    {
        if (Inven.equipedItem != null)
        {
            if (Inven.equipedItem.Ename == "sickle")
            {
                grinder.gameObject.SetActive(true);
            }
            else
            { 
                grinder.gameObject.SetActive(false);
            }
        }
        else
        {
            grinder.gameObject.SetActive(false);
        }
    }
 * */
