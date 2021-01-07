using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text 클래스 사용을 위해서
using System; // String 클래스 사용을 위해서 (Text에서)

public class Chicken : MonoBehaviour
{
    Rigidbody2D rigid; // 속력 부여하기 위해서
    public GameObject Care;
    public GameObject Choose;
    public GameObject Heart;
    public Text CareText;
    public int nextMovex, nextMovey; // 다음 행동 결정할 변수
    public int happy = 0;
    bool checkx = false;
    bool checky = false;

    Animator anim; // Animator 불러오기

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 4); // 생성 후 4초 후부터 움직이기 시작

        anim = GetComponent<Animator>(); // anim 변수 선언
    }

    // Update is called once per frame
    void Update()
    {    
        // 속도 정의
        rigid.velocity = new Vector2(nextMovex,nextMovey);

        // 움직임 경계 설정
        if(checkx==false) // x값 경계
        {
            if(transform.position.x < -8 || transform.position.x > 8) // 8 → 경계값만 수정
            {
                checkx=true;
                nextMovex = nextMovex*(-1); // 반대 방향으로
            }
        }
        if(checky==false) // y값 경계
        {
            if(transform.position.y < -4 || transform.position.y > 4) // 4 → 경계값만 수정
            {
                checky=true;
                nextMovey = nextMovey*(-1); // 반대 방향으로
            }
        }

        // 말 걸면 움직임 멈춤 (해당 닭만)
        if(Care.activeSelf == true || Choose.activeSelf == true)
        {   ChickenStop();   }

        // 애니메이션 MoveX, MoveY
        anim.SetFloat("MoveX", nextMovex);
        anim.SetFloat("MoveY", nextMovey);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name == "Player") // Player가 닿으면
        {    
            CareText.text = String.Format("무얼 하실 건가요 ({0}/10)", happy);
            Care.SetActive(true); // 선택창 나타남 (ft. 행복도)
        }
    } // 닭을 trigger로 설정

    void ChickenStop()
    {   nextMovex = 0; nextMovey = 0;   } // 움직임 멈춤

    void Think()
    {
        checkx=false; checky=false;
        nextMovex = UnityEngine.Random.Range(-1,2); // -1 이상 2 미만
        nextMovey = UnityEngine.Random.Range(-1,2); // -1 후진, 0 정지, 1 전진
        float time = UnityEngine.Random.Range(1.0f, 4.0f); // 반복 시간 랜덤 부여
        Invoke("Think", time); // 재귀
    }

    void HideHeart()
    {    Heart.SetActive(false);    } // 하트 사라지게

    // UI Buttons
    public void OnClickFoodButton() // 밥주기 버튼
    {
        Choose.SetActive(true);
        Care.SetActive(false);
    }
    public void OnClickGoodButton() // 쓰다듬기
    {
        Care.SetActive(false);
        happy += 1; // 행복도 1 증가
        Heart.SetActive(true); // 하트 나타나고
        Invoke("HideHeart", 1); // 1초 후 사라짐
    }
    public void OnClickBackButton() // 취소 버튼
    {
        Care.SetActive(false);
    }
    public void OnClickSeedButton() // 씨앗 주기
    {
        Choose.SetActive(false);
        happy += 2; // 행복도 2 증가
        Heart.SetActive(true); // 하트 나타나고
        Invoke("HideHeart", 1); // 1초 후 사라짐
    }
    public void OnClickHayButton() // 건초 주기
    {
        Choose.SetActive(false);
        happy += 4; // 행복도 4 증가
        Heart.SetActive(true); // 하트 나타나고
        Invoke("HideHeart", 1); // 1초 후 사라짐
    }

    // 행복도가 3일 후 "10 이상 → 고급 알", "5 이상 → 보통 알", "5 미만 → 알 없음"
    // 수정) 하루 후 4 이상 → 고급 / 2 이상 → 보통 / 나머지 → 없음
    // "시간"
}
