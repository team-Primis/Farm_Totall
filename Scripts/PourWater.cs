using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourWater : MonoBehaviour
{
    //!!!!!!!게임이 일시정지 아닐 때만 동작하게 설정해두기!!!!!!!

    public GameObject waterPrefab;//물 주면 생성되는 프리펩.
    public GameObject thePlayer;
    public inventory Inven;
    // Start is called before the first frame update
    private void Awake()
    {
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnWater();
        
    }

    void SpawnWater()//식물 심는 함수.
    {

        Vector2 theplayerPosition = thePlayer.transform.position;//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 themousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기마다 이동하는 것처럼 보이기 위해 올림하여 마우스 위치 재설정.
        Vector2 distance = theplayerPosition - mousePosition;


        if (Input.GetMouseButtonDown(0))//플레이어의 위치를 기준으로 x는 타일 1.5칸, y는 타일 2칸 이하에서
        {
            if (Inven.equipedItem != null)
            {
                if (Inven.equipedItem.Ename == "waterSprinkle")
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
                    RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);

                    for (int i = 0; i < hit.Length; i++)
                    {
                        for (int j = 0; j < hit.Length; j++)
                        {
                            if (hit[i].transform.tag != "Water")
                            {
                                if (hit[i].transform.tag == "Plant" || hit[j].transform.tag == "Plant" )
                                {
                                    
                                    if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//마우스 왼클릭을 하는 중에는
                                    {

                                        GameObject Watered = Instantiate(waterPrefab);//식물 생성
                                        Watered.transform.position = themousePosition;//생성한 식물을 마우스 위치와 같은 곳에 배치함.

                                    }
                                }
                            }
                        }
                    }

                } 
            }




        }
    }
}

