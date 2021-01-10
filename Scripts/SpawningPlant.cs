using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPlant : MonoBehaviour//식물 spawning하는 클래스
{
    public GameObject[] PlantPrefabs;//배열로 구현하여 인덱스가 0일 때 꽃, 1일 때 호박을 심도록 구현.
    public Transform target;//플레이어의 현재 위치 추적하기 위한 필드.
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        SpawnPlant(PlantPrefabs[0]);//심을 식물 종류를 어떻게 결정할 수 있을지 몰라서 우선 꽃만 키울 수 있게 해둠. 
    }

    void SpawnPlant(GameObject PlantPrefabs)//식물 심는 함수.
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기마다 이동하는 것처럼 보이기 위해 올림하여 마우스 위치 재설정.
        if (Mathf.Abs(target.position.x) <= 1.5f || Mathf.Abs(target.position.y) <= 2f)//플레이어의 위치를 기준으로 x는 타일 1.5칸, y는 타일 2칸 이하에서
        {
            if (Input.GetMouseButton(0))//마우스 왼클릭을 하는 중에는
            {
                
                GameObject PlantedPlant = Instantiate(PlantPrefabs);//식물 생성
                PlantedPlant.transform.position = mousePosition;//생성한 식물을 마우스 위치와 같은 곳에 배치함.
               
            }
            }
        }
    }

}
