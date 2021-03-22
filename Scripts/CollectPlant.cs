using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPlant : MonoBehaviour
{

    public Transform thePlayer;//플레이어 위치 가져옴.
    public inventory Inven;//인벤토리 가져옴.
    public Stemina stM;//스태미나 스크립트 가져옴.
    public SpawningPlant sP;//스포닝플랜트 스크립트 가져옴.
   
    public AudioClip collectingPlant;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player").GetComponent<Transform>();
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
        sP = GameObject.Find("SpawningPlant").GetComponent<SpawningPlant>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Harvestit();
    }

    //식물 수확하는ㄴ함수.
    public void Harvestit()
    {
      

        Vector2 theplayerPosition = thePlayer.position;//플레이어의 위치를 선언.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//게임플레이화면에서의 마우스 위치를 게임에디터의 Vector2 타입의 마우스 위치에 배정.


        Vector2 distance = theplayerPosition - mousePosition;//플레이어와 마우스 사이의 거리 선언.

        if (Input.GetMouseButtonDown(0))//마우스 왼클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);//이거 아마 마우스 위치에서/카메라가 보고 있는 방향으로/길이 무한인 레이져 쏘는 거일걸.

            if (hit.collider != null)//레이져에 물체가 맞았는데
            {

                if (hit.collider.CompareTag("Plant"))//그 물체가 식물이면
                {

                    if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//플레이어의 위치를 기준으로 x 거리는 타일 1.5칸, y 거리는 타일 2칸 이하에서
                    {
                        GameObject notwateredplant = hit.collider.gameObject;//맞은 물체를 게임오브젝트로 선언해주고
                        PlantLoad pL = notwateredplant.GetComponent<PlantLoad>();//그 게임오브젝트의 PlantLoad 스크립트를 가져와서
                        if(pL.i >= pL.didItBloomed && pL.i < pL.toMuchWilted)//그 스크립트의 i 상태를 통해 얘가 수확 가능한 상태인 걸 확인하고
                        {
                            if (hit.collider.gameObject.name == "BlueFlower(Clone)")//만약 맞은 식물이 파란꽃이면
                            {
                                Inven.putInventory(41, 1);//인벤토리에 파란꽃을 넣어줌.
                            }
                            else if (hit.collider.gameObject.name == "Pumpkin(Clone)")//만약 맞은 식물이 호박이면
                            {
                                Inven.putInventory(40, 1);//인벤토리에 호박을 넣어줌.
                            }

                            Destroy(notwateredplant);//레이져에 맞은 물체는 없애줌.
                            sP.createdPlant.Remove(notwateredplant);//스포닝플랜트에서 관리하는 생성된 식물 리스트에서 레이져에 맞은 물체를 없애줌.
                            sP.PlantXp.Remove(notwateredplant.transform.position.x);//레이져에 맞은 물체의 x 좌표 정보도 없애줌.
                            sP.PlantYp.Remove(notwateredplant.transform.position.y);//레이져에 맞은 물체의 y 좌표 정보도 없애줌.
                            SoundManager.instance.SFXPlay("Planting", collectingPlant);//사운드매니져의 음악 재생 함수를 이용하여 수확 시 소리를 재생해줌.
                            stM.UseHp(3f);//스태미나 소모함.
                        }

                               
                       

                    }
                }
            }

        }



    }
}
