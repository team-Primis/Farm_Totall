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

    Animator anim; // Animator 불러오기

    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        SMScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        PlayerTF = GameObject.Find("Player").GetComponent<Transform>();

        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 4); // 생성 후 4초 후부터 움직이기 시작

        anim = GetComponent<Animator>(); // anim 변수 선언
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

        if(Input.GetKey(KeyCode.Space)) // 스페이스바 눌렀을 때
        {   HiChicken();   } // 창 나타낼지 판단

        if(Care.activeSelf == true || Choose.activeSelf == true) // 닭 케어 창이 열렸을 때
        {
            GMScript.isCareOpen = true;
            ChickenStop(); // 해당 닭만 정지
        }

        // 닭 관련 창 열렸으면 움직임 멈춤
        //if(GMScript.isBuyOpen == true || GMScript.isCareOpen == true)
        //{   ChickenStop();   }

        // 기타 UI 열렸으면 움직임 멈춤
        //if(GMScript.vendingImage.activeSelf == true || GMScript.container.activeSelf == true
        //        || GMScript.inventory.activeSelf == true)
        //{   ChickenStop();   }

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

        // 현재 보유량 반영 - 변수 받아와야 함, 0개면 못 주고 꺼지는 거지...
        //SeedText.text = String.Format("씨앗({0})", seedNum);
        //HayText.text = String.Format("건초({0})", hayNum);
    }

    void HiChicken() // Player가 가까워지면 선택창 나타남
    {
        dis = Vector2.Distance(PlayerTF.position,transform.position);
        if(dis < 1.0f)
        {
            CareText.text = String.Format("무얼 하실 건가요 ({0}/4)", happy);
            Care.SetActive(true); // 선택창 나타남 (ft. 행복도)
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

    void HideHeart()
    {    Heart.SetActive(false);    } // 하트 사라지게

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
        GMScript.isCareOpen = false;
    }
    public void OnClickBackButton() // 취소 버튼
    {
        Care.SetActive(false);
        GMScript.isCareOpen = false;
    }
    public void OnClickSeedButton() // 씨앗 주기
    {
        Choose.SetActive(false);
        happy += 2; // 행복도 2 증가
        Heart.SetActive(true); // 하트 나타나고
        Invoke("HideHeart", 1); // 1초 후 사라짐
        GMScript.isCareOpen = false;
    }
    public void OnClickHayButton() // 건초 주기
    {
        Choose.SetActive(false);
        happy += 4; // 행복도 4 증가
        Heart.SetActive(true); // 하트 나타나고
        Invoke("HideHeart", 1); // 1초 후 사라짐
        GMScript.isCareOpen = false;
    }
}
