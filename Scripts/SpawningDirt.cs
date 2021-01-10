using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningDirt : MonoBehaviour
{
    public GameObject DirtPrefab;//파낸 검은 흙 오브젝트 필드 설정.
    public Transform target;//플레이어의 현재 위치를 가져오기 위한 필드.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//게임플레이화면에서의 마우스 위치를 VEctor2 타입의 마우스 위치에 배정.
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기만큼 움직이는 것처럼 보이기 위해 반올림하여 마우스 위치 재배정.
        
        if (Mathf.Abs(target.position.x) <= 1.5f || Mathf.Abs(target.position.y) <= 2f)//현재 플레이어 위치를 기준으로 주변 x로는 타일 1.5칸, y로는 2칸 이하에서
        { if (Input.GetMouseButton(0))//마우스 왼클릭을 하면
            {
                GameObject DarkDirt = Instantiate(DirtPrefab);//땅이 파짐
                DarkDirt.transform.position = mousePosition;//파진 땅 위치는 클릭한 마우스 위치.
            }
            }
    }
}
