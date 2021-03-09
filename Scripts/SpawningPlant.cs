using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPlant : MonoBehaviour//식물 spawning하는 클래스
    //!!!!!!!게임이 일시정지 아닐 때만 동작하게 설정해두기!!!!!!!
{
    public GameObject[] PlantPrefabs;//배열로 구현하여 인덱스가 0일 때 꽃, 1일 때 호박을 심도록 구현.
    public GameObject thePlayer;
    public inventory Inven;
    public Stemina stM;
    // Start is called before the first frame update
    void Start()
    {
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        //By미해 ( 이거 없으면 건물 안으로 들어왔다 나가면 null pointer 에러)
        thePlayer = GameObject.Find("Player").gameObject;
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Inven.equipedItem.Ename != "empty")
        {
            if (Inven.equipedItem.Ename == "blueFlowerSeed")
            { SpawnPlant(PlantPrefabs[0]); }
            if (Inven.equipedItem.Ename == "pumpkinSeed")
            { SpawnPlant(PlantPrefabs[1]); }
        }
    }

    void SpawnPlant(GameObject PlantPrefabs)//식물 심는 함수.
    {

        Vector2 theplayerPosition = thePlayer.transform.position;//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 themousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기마다 이동하는 것처럼 보이기 위해 올림하여 마우스 위치 재설정.
        Vector2 distance = theplayerPosition - mousePosition;
        
        
        if (Input.GetMouseButtonDown(0))//플레이어의 위치를 기준으로 x는 타일 1.5칸, y는 타일 2칸 이하에서
        {
            //Debug.Log(distance);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.transform != null)
            {
                if (hit.collider.CompareTag("DarkDirt"))
                {
                    if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//마우스 왼클릭을 하는 중에는
                    {

                        GameObject PlantedPlant = Instantiate(PlantPrefabs);//식물 생성
                        PlantedPlant.transform.position = themousePosition;//생성한 식물을 마우스 위치와 같은 곳에 배치함.}
                        Inven.UseItem(Inven.equipedItem.id);
                        stM.UseHp(4f);
                    }



                }
            }
                            
                   



        }
    }
}