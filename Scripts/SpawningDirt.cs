﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawningDirt : MonoBehaviour
{
    public GameObject DirtPrefab;//파낸 검은 흙 오브젝트 필드 설정.
    public GameObject thePlayer;
    public inventory Inven;
    public Stemina stM;
    // Start is called before the first frame update
    private void Awake()
    {

        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        thePlayer = GameObject.Find("Player").gameObject;
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();

    }

    // Update is called once per frame


 void Update()
    {
        SpawnDirt();
    }
   public void SpawnDirt()
    {
        Vector2 theplayerPosition = thePlayer.transform.position;//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 themousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기마다 이동하는 것처럼 보이기 위해 올림하여 마우스 위치 재설정.
        Vector2 distance = theplayerPosition - mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            if (Inven.equipedItem != null)//(Inven.equipedItem.Ename != "empty")로 하니까 눌레퍼런스 그거 뜨길래 다시 바꿈.
            {
                if (Inven.equipedItem.Ename == "sickle")
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                    
                    if (hit.collider == null || hit.collider.CompareTag("Player")) 
                    {
                        
                        if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//마우스 왼클릭을 하는 중에는
                            {

                            GameObject DarkDirt = Instantiate(DirtPrefab);//식물 생성
                            DarkDirt.transform.position = themousePosition;//생성한 식물을 마우스 위치와 같은 곳에 배치함.
                            stM.UseHp(7f);
                            }
                        
                    }

                    if(hit.collider!=null)
                    {
                        if(hit.collider.CompareTag("DarkDirt"))
                        {
                            GameObject responsedDarkDirt = hit.collider.gameObject;
                            Destroy(responsedDarkDirt);
                            stM.UseHp(5f);
                        }
                    }
                        
                    



                }
            }

        }
    }
}