using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text 클래스 사용을 위해서
using System; // String 클래스 사용을 위해서 (Text에서)
using UnityEngine.SceneManagement; // 씬 관련 (달걀 낳기 때문에)

public class Chicken : MonoBehaviour
{
    // 하트있을때, 닭판매창, 일시정지, 로딩화면 = 닭 정지
    // 닭구매창, 닭판매창, 일시정지, 로딩화면, 자는화면 = 닭 쓰다듬기 불가능
    // 가까이 가서 아무것도 안 들고 "좌클릭 = 쓰다듬기", "Z = 닭판매창ON"
    // 가까이 가서 건초 또는 씨앗 들고 "스페이스바 = 밥주기"
    // 밥주기 또는 쓰다듬기 직후: 하트 생성
    // 아침 7시(timer=420) : 행복도 확인하여 달걀 생성 → 집 내부의 경우는 SPAWNMANAGER 참고

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
    public bool checkEgg = false; // 중복 방지

    public inventory Inven; // 건초 및 씨앗

    Animator anim; // Animator 불러오기

    public GameObject WillSell;
    private PlayerControll thePlayerCtr; // for money

    public NoticeText notice; // 안내 메세지

    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        SMScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        PlayerTF = GameObject.Find("Player").GetComponent<Transform>();

        Inven = GameObject.Find("Inventory").GetComponent<inventory>();

        Invoke("Think", 4); // 생성 후 4초 후부터 움직이기 시작

        anim = GetComponent<Animator>(); // anim 변수 선언

        checkEgg = true; // 초기 설정

        thePlayerCtr = GameObject.Find("Player").GetComponent<PlayerControll>();

        notice = GameObject.Find("Notice").GetComponent<NoticeText>(); // 안내 메세지
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
        if(SceneManager.GetActiveScene().name == "OutSide")
        {
            if(GMScript.timer >= 420 && GMScript.timer <= 424) // 날 밝으면 (7시 기준 = 420)
            {
                if(checkEgg == false) // 한 번만
                {
                    checkEgg = true; // true
                }
            }
            else if(GMScript.timer > 424 && GMScript.timer <= 428) // 그 다음에
            {
                if(checkEgg == true) // 한 번만
                {
                    LayEgg(); // 알 낳고
                    checkEgg = false; // false
                }
            }
        }
        // INSIDE -> SPAWNMANAGER


        // 현재 먹이 보유량 반영 (from inventory)
        // care&choose 창 삭제함

        // 하트있을때, 일시정지, 닭판매창, 로딩화면 = 닭 정지 (0314 업데이트)
        if(Heart.activeSelf || GMScript.isMenuOpen || GMScript.isWillSellOpen || GMScript.isLoadingOpen)
        {
            ChickenStop();
        }

        // 장착 후 스페이스바 클릭 - 밥 주기
        if(Input.GetKeyDown(KeyCode.Space)) // 스페이스바 눌렀을 때 1번 판단
        {
            // 장착한 게 건초라면
            // (미해 : Inven.equippedItem != null 추가. (안그러면 아이템을 들고 있지 않을 때 오류))
            if(Inven.equipedItem.Ename == "hay")
            {
                FeedHay();
            }

            // 장착한 게 씨앗이라면 - 현재 id 1~9가 씨앗임
            // (미해 : Inven.equippedItem!= null, Inven.equipedItem.id != 0 추가.
            // (아무것도 안들떄 초기 id가 0이라서 계속 먹임))
            // (0304) 초기 id가 1000으로 변경됨, null 없어짐
            if(Inven.equipedItem.id != 1000 && Inven.equipedItem.id < 10) // 
            {
                FeedSeed();
            }
        }

        // 닭 팔기 - 1
        // Z(닭판매창), Yes버튼, X버튼
        // 시간 정지, 닭 정지, 플레이어 정지
        if(Input.GetKeyDown(KeyCode.Z)) // Z 눌렀을 때
        {
            dis = Vector2.Distance(PlayerTF.position,transform.position);
            if(dis < 1.0f && Inven.equipedItem.id == 1000) // 가까이서 아무것도 안 들고 있으면
            {
                WillSell.SetActive(true);
                WillSell.transform.position = Camera.main.WorldToScreenPoint(
                        new Vector2(this.transform.position.x, this.transform.position.y + 1.5f));
                GMScript.isWillSellOpen = true;
                GMScript.isTimerStoped = true; // 시간 정지
            }
        }
    }

    // 닭 팔기 - 2
    // 직접 버튼에 함수 추가 필수!
    public void WSYes()
    {
        thePlayerCtr.playerMoneyChange(200, true); // 돈 200원 증가
        GMScript.isWillSellOpen = false;
        GMScript.isTimerStoped = false; // 시간 흐르게
        SMScript.chickenCount -= 1; // 개수 반영
        notice.WriteMessage("닭 판매 성공!");
        int chnum = SMScript.chickenList.Count; // 여기부턴 닭 리스트에서 제거하는 과정
        for (int i = 0; i < SMScript.chickenList.Count; i++)
        {
            if(SMScript.chickenList[i] == this.gameObject)
            chnum = i;
        }
        if(chnum < SMScript.chickenList.Count) // index 찾았으면
        {
            SMScript.chickenList.RemoveAt(chnum);
        }
        Destroy(this.gameObject); // 파괴
    }
    public void WSNo()
    {
        WillSell.SetActive(false);
        GMScript.isWillSellOpen = false;
        GMScript.isTimerStoped = false; // 시간 흐르게
    }

    
    // 쓰다듬기 - 마우스 (좌)클릭
    void OnMouseDown() // 닭을 클릭했을 때
    {
        dis = Vector2.Distance(PlayerTF.position,transform.position);
        // 일시정지창, 로딩화면창, 닭구매창, 닭판매창, 자는화면창 없을 때 (0314 업데이트)
        if(GMScript.isMenuOpen == false && GMScript.isLoadingOpen == false
            && GMScript.isWillSellOpen == false && GMScript.isBuyOpen == false && GMScript.isSleepOpen == false)
        {
            // Player가 가깝고 + 아무것도 안 들고 있다면 = 쓰다듬기 가능
            if(dis < 1.5f && Inven.equipedItem.id == 1000)
            {
                happy += 1; // 행복도 1 증가
                Heart.SetActive(true); // 하트 나타나고
                Debug.Log("Give Love (행복도: "+happy+"/4)");
                Invoke("HideHeart", 1); // 1초 후 사라짐
            }
        }
    }
    

    void HideHeart() // 하트 사라지게
    {    Heart.SetActive(false);    }

    void FeedHay()
    {
        dis = Vector2.Distance(PlayerTF.position,transform.position);
        if(dis < 1.0f) // Player가 가깝다면 밥 주기 가능
        {
            // removeitem -> useitem

            //(0316 미해 : useItem통일)
            //Inven.UseItem(Inven.equipedItem.id); // 해당 건초 개수 하나 감소 (원래 코드)
            Inven.UseItem(); //(바꾼 코드)

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
            //(0316 미해 : useItem통일)
            //Inven.UseItem(Inven.equipedItem.id); // 해당 씨앗 개수 하나 감소(원래코드)
            Inven.UseItem(); //바꾼코드

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
        if(happy >= 4)
        {   SMScript.SpawnGEgg();   }
        else if(happy >= 2)
        {   SMScript.SpawnNEgg();   }
        happy = 0;
    }
}
