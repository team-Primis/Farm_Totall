using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourWater : MonoBehaviour
{
    //!!!!!!!게임이 일시정지 아닐 때만 동작하게 설정해두기!!!!!!!

    public GameObject thePlayer;//플레이어 선언.
    public inventory Inven;//인벤토리 선언.
    public Stemina stM;//스태미나 스크립트 가져옴.
    public AudioClip wateringSound;//물 소리.
    // Start is called before the first frame update
    private void Awake()
    {
        thePlayer = GameObject.Find("Player").gameObject;
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();

    }

    // Update is called once per frame
    void Update()
    {
        SpawnWater();

    }

    //물 뿌리는 함수.
    void SpawnWater()
    {

        Vector2 theplayerPosition = thePlayer.transform.position;//플레이어의 위치를 선언.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//게임플레이화면에서의 마우스 위치를 게임에디터에서의 Vector2 타입의 마우스 위치에 배정.
        
        Vector2 distance = theplayerPosition - mousePosition;//플레이어와 마우스 사이의 거리 선언.


        if (Input.GetMouseButtonDown(0))//마우스 왼클릭 시
        {
            if (Inven.equipedItem != null)//인벤의 장착한 아이템 칸이 빈칸이 아닐 때
            {
                if (Inven.equipedItem.Ename == "waterSprinkle")//장착한 아이템이 물뿌리개인 경우
                {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);//이거 아마 마우스 위치에서/카메라가 보고 있는 방향으로/길이 무한인 레이져 쏘는 거일걸.

                    if (hit.collider != null)//만약 발사한 레이저가 어디에 맞았는데
                    {
                        if (hit.collider.CompareTag("Plant"))//식물에 맞았으면
                        {
                            if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//플레이어의 위치를 기준으로 x 거리는 타일 1.5칸, y 거리는 타일 2칸 이하에서
                            {
                                GameObject notwateredplant = hit.collider.gameObject;//맞은 식물을 게임오브젝트로 선언해주고
                                PlantLoad pL = notwateredplant.GetComponent<PlantLoad>();//맞은 식물에 붙은 PlantLoad 스크립트를 가져와줘서

                                GameObject watered = notwateredplant.transform.Find("watered").gameObject;//맞은 식물에 Child로 넣어둔 물 오브젝트를 가져와서
                                SpriteRenderer wsr = watered.GetComponent<SpriteRenderer>();//물 오브젝트의 스프라이트 렌더러를 이용해서
                                pL.iswatered = true;//물을 줬음을 표시함.

                                wsr.enabled = true;//물을 줬음을 표시함.
                                SoundManager.instance.SFXPlay("Planting", wateringSound);//물 주는 소리 재생.
                                stM.UseHp(3f);//스태미나 소모.




                            }

                        }



                    }

                }
            }





        }




    }
}

