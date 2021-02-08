using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text 클래스 사용을 위해서
using System; // String 클래스 사용을 위해서 (Text에서)

public class Chicken : MonoBehaviour
{
    Rigidbody2D rigid; // 속력 부여하기 위해서
    public GameObject Heart;
    public int nextMovex, nextMovey; // 다음 행동 결정할 변수
    public float randx, randy; // 미세 방향 조절
    public int happy = 0;
    bool checkx = false;
    bool checky = false;

    public float dis; // distance btw player & chicken
    public Transform PlayerTF; // position of player

    public GameManager GMScript; // 시간 사용, 창 유무
    public SpawnManager SMScript; // 알 소환하려고
    public float chickTimer; // 확인용...
    public bool checkEgg = false; // 중복 방지

    public inventory Inven; // 건초 및 씨앗

    Animator anim; // Animator 불러오기

    public int theCount; // 닭 저장 및 로드를 위해 추가한 부분

    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        SMScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        PlayerTF = GameObject.Find("Player").GetComponent<Transform>();

        Inven = GameObject.Find("Inventory").GetComponent<inventory>();

        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 4); // 생성 후 4초 후부터 움직이기 시작

        anim = GetComponent<Animator>(); // anim 변수 선언

        checkEgg = true; // 초기 설정

        theCount = GMScript.chickenCount;
    }

    // Update is called once per frame
    void Update()
    {    
        // 움직임 부여 방법 수정함 (velocity → translate)
        transform.Translate(new Vector2(nextMovex*randx,nextMovey*randy)*Time.deltaTime/2);

        // 움직임 경계 설정
        if(checkx==false) // x값 경계
        {
            if(transform.position.x < -35 || transform.position.x > 35) // 35 → 경계값만 수정
            {
                checkx=true;
                nextMovex = nextMovex*(-1); // 반대 방향으로
            }
        }
        if(checky==false) // y값 경계
        {
            if(transform.position.y < -35 || transform.position.y > 35) // 35 → 경계값만 수정
            {
                checky=true;
                nextMovey = nextMovey*(-1); // 반대 방향으로
            }
        }

        // 애니메이션 MoveX, MoveY
        anim.SetFloat("MoveX", nextMovex);
        anim.SetFloat("MoveY", nextMovey);

        // 하루마다 달걀 낳기
        chickTimer = GMScript.timer;
        if(chickTimer <= 1){    checkEgg = true;    } // 날 밝으면 초기화
        if(chickTimer >= 30) // 밤에
        {
            if(checkEgg == true)
            {
                LayEgg();
                checkEgg = false; // 한 번만
            }
        }

        // 현재 먹이 보유량 반영 (from inventory)
        // 문제가 씨앗이 여러 종류인데 어떻게 선택할 것이냐
        // 1) 씨앗 대신 닭 모이(씨앗모음) - 자판기에서 따로 팔아야 하는가 else?
        // 2) 또는 씨앗을 선택하면 그 다음에 고르도록 - 고생길...
        // 3) 닭 먹이를 장착한 후 스페이스바로 닭 누르기
        // 3번이 베스트지만 케어창 없애야 하고 마우스 클릭을 쓰다듬기로 바꾸는 등
        // 엄청난 공사가 필요함 그래서 진행 중...
        // care&choose창은 필요 없어짐!

        if(Heart.activeSelf == true){   ChickenStop();  } // 하트 있을 땐 해당 닭 정지

        if(GMScript.isMenuOpen == true){   ChickenStop();  } // 일시정지면 닭들 정지

        //장착 후 스페이스바 클릭 - 밥 주기
        if(Input.GetKeyDown(KeyCode.Space)) // 스페이스바 눌렀을 때 1번 판단
        {
            if(Inven.equipedItem.Ename == "hay") // 장착한 게 건초라면
            {
                FeedHay();
            }
            if(Inven.equipedItem.id < 5) // 현재 id 1~4가 씨앗임
            {
                FeedSeed();
            }
        }
    }


    // 쓰다듬기 - 마우스 (좌)클릭
    void OnMouseDown() // 닭을 클릭했을 때
    {
        dis = Vector2.Distance(PlayerTF.position,transform.position);
        if(dis < 1.5f) // Player가 가깝다면 쓰다듬기 가능
        {
            happy += 1; // 행복도 1 증가
            Heart.SetActive(true); // 하트 나타나고
            Debug.Log("Give Love (행복도: "+happy+"/4)");
            Invoke("HideHeart", 1); // 1초 후 사라짐
        }
    }

    void HideHeart() // 하트 사라지게
    {    Heart.SetActive(false);    }

    void FeedHay()
    {
        dis = Vector2.Distance(PlayerTF.position,transform.position);
        if(dis < 1.0f) // Player가 가깝다면 밥 주기 가능
        {
            Inven.RemoveItem(Inven.equipedItem.id); // 해당 건초 개수 하나 감소
            happy += 4; // 행복도 4 증가
            Heart.SetActive(true); // 하트 나타나고
            Debug.Log("Give Hay (행복도: "+happy+"/4)");
            Invoke("HideHeart", 1); // 1초 후 사라짐
        }
    }

    void FeedSeed()
    {
        dis = Vector2.Distance(PlayerTF.position,transform.position);
        if(dis < 1.0f) // Player가 가깝다면 밥 주기 가능
        {
            Inven.RemoveItem(Inven.equipedItem.id); // 해당 씨앗 개수 하나 감소
            happy += 2; // 행복도 2 증가
            Heart.SetActive(true); // 하트 나타나고
            Debug.Log("Give Seed (행복도: "+happy+"/4)");
            Invoke("HideHeart", 1); // 1초 후 사라짐
        }
    }

    void ChickenStop()
    {   nextMovex = 0; nextMovey = 0;   } // 움직임 멈춤

    void Think()
    {
        randx = UnityEngine.Random.Range(0.6f, 1.0f); // 미세 방향 조절
        randy = UnityEngine.Random.Range(0.6f, 1.0f); // 미세 방향 조절
        checkx=false; checky=false;
        nextMovex = UnityEngine.Random.Range(-1,2); // -1 이상 2 미만 (-1,0,1)
        nextMovey = UnityEngine.Random.Range(-1,2); // -1 후진, 0 정지, 1 전진
        float time = UnityEngine.Random.Range(4.0f, 7.0f); // 반복 시간 랜덤 부여
        Invoke("Think", time); // 재귀
    }

    // 달걀 낳는 조건 - 하루 후 4 이상 → 고급 / 2 이상 → 보통 / 나머지 → 없음
    void LayEgg()
    {
        if(happy>=4)
        {   SMScript.SpawnGEgg();   }
        else if(happy>=2)
        {   SMScript.SpawnNEgg();   }
        happy = 0;
    }

    // UI Buttons
    /*public void OnClickFoodButton() // 밥주기 버튼
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
        GMScript.isCareOpen = false;
    }
    public void OnClickBackButton() // 취소 버튼
    {
        Care.SetActive(false);
        GMScript.isCareOpen = false;
    }
    public void OnClickSeedButton() // 씨앗 주기 - 0개면 못 주고 꺼지면서 Log 출력...
    {
        Choose.SetActive(false);
        happy += 2; // 행복도 2 증가
        Heart.SetActive(true); // 하트 나타나고
        Invoke("HideHeart", 1); // 1초 후 사라짐
        GMScript.isCareOpen = false;
    }
    public void OnClickHayButton() // 건초 주기 - 0개면 못 주고 꺼지면서 Log 출력...
    {
        Choose.SetActive(false);
        happy += 4; // 행복도 4 증가
        Heart.SetActive(true); // 하트 나타나고
        Invoke("HideHeart", 1); // 1초 후 사라짐
        GMScript.isCareOpen = false;
    }*/
}
