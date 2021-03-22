using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 이동
using UnityEngine.UI; // for UI text
using System.IO; // Input Output
using System.Runtime.Serialization.Formatters.Binary; // binary 파일로 변환

public class MenuControl : MonoBehaviour
{
    // 서브메뉴(=일시정지) 화면 + Load + Save (2/3 수정 - SaveNLoad 스크립트로)
    // GameManager에 붙임!

    public GameObject menuWindow;
    public GameObject whereSave;

    public GameManager GMScript; // for isMenuOpen
    public SaveNLoad theSaveNLoad;

    public Text AboutF1;
    public Text AboutF2;

    // center function
    public GameObject BuyChicken;
    public SpawnManager SMScript;
    private PlayerControll thePlayerCtr; // for money

    // 안내 메세지
    public NoticeText notice;

    // for 일시정지창 ON/OFF 조건
    GameObject SL;
    GameObject CP;
    GameObject VP;
    GameObject SP;

    void Start()
    {
        GameObject canvas2 = GameObject.Find("Canvas2");
        menuWindow = canvas2.transform.Find("MenuWindow").gameObject;
        whereSave = canvas2.transform.Find("MenuWindow").transform.Find("WhereSave").gameObject;

        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        theSaveNLoad = GameObject.Find("GameManager").GetComponent<SaveNLoad>();

        AboutF1 = canvas2.transform.Find("MenuWindow").transform.Find("WhereSave").
                transform.Find("SaveFile1").transform.Find("AboutF1").GetComponent<Text>();
        AboutF2 = canvas2.transform.Find("MenuWindow").transform.Find("WhereSave").
                transform.Find("SaveFile2").transform.Find("AboutF2").GetComponent<Text>();

        // center function
        BuyChicken = canvas2.transform.Find("BuyChicken").gameObject;
        SMScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        thePlayerCtr = GameObject.Find("Player").GetComponent<PlayerControll>();

        // 안내 메세지
        notice = GameObject.Find("Notice").GetComponent<NoticeText>();

        // for 일시정지창 ON/OFF 조건
        SL = GameObject.Find("Canvassleep").transform.Find("SleepLoading").gameObject;
        CP = GameObject.Find("Canvas2").transform.Find("containerPanel").gameObject;
        VP = GameObject.Find("Canvas2").transform.Find("vendingPanel").gameObject;
        SP = GameObject.Find("Canvas2").transform.Find("sellingPanel").gameObject;
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel")) // esc 버튼
        {
            // 로딩화면창, 닭구매창, 닭판매창, 기절창(잠선택창),
            // 자는화면창, 보관상자, 자판기, 판매상자 꺼져있을 때 (0320 업데이트)
            if(!GMScript.isLoadingOpen && !GMScript.isWillSellOpen && !GMScript.isBuyOpen && !GMScript.isSleepOpen 
                    && !SL.activeSelf && !CP.activeSelf && !VP.activeSelf && !SP.activeSelf)
            {
                if(menuWindow.activeSelf) // 켜져있을 때
                {
                    if(!whereSave.activeSelf) // 저장하기 아닌 상태
                    {
                        menuWindow.SetActive(false); // 메뉴 off
                        GMScript.isTimerStoped = false; // 시간 흐르기 시작
                    }
                }
                else // 꺼져있을 때
                {
                    menuWindow.SetActive(true); // 메뉴 on
                    GMScript.isTimerStoped = true; // 시간 정지 시작
                }
            }
        }

        // 메뉴 on → 시간(menucontrol), 닭(Chicken), 플레이어(playermove) 정지
        if(menuWindow.activeSelf)
        {
            GMScript.isMenuOpen = true;
        }
        else
        {
            GMScript.isMenuOpen = false;
        }
    }

    public void GoConti() // 게속하기
    {
        menuWindow.SetActive(false); // 메뉴 off
        GMScript.isTimerStoped = false; // 시간 흐르기 시작
    }

    public void GoSave() // 저장하기
    {
        whereSave.SetActive(true); // 저장할 파일 선택할 UI
        // 저장 완료 후 whereSave를 다시 false로 바꾼 후 menuWindow false로 바꾸기!

        // Choice1BT
        FileInfo fileInfo1 = new FileInfo(Application.dataPath + "/SaveFile1.txt");
        if (fileInfo1.Exists) // 파일이 존재하면
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file1 = File.Open(Application.dataPath + "/SaveFile1.txt", FileMode.Open);
            if(file1.Length > 0) // 내용이 있을 때
            {
                SNLData data1;
                data1 = (SNLData)bf.Deserialize(file1); // 직렬화된 것을 Data 형식으로 바꿈
                int moneyNow = 2000 - data1.usedMoney; // 기본값 2000
                AboutF1.GetComponent<Text>().text = "FILE 1 : DAY " + data1.day +
                                                        " / " + moneyNow + "원" +
                                                        " / " + "닭 " + data1.chickenCount + "마리";
            }
            else
            {
                AboutF1.GetComponent<Text>().text = "FILE 1 : EMPTY";
            }
            file1.Close(); // 파일 닫기
        }
        else
        {
            AboutF1.GetComponent<Text>().text = "FILE 1 : NO FILE";
        }

        // Choice2BT
        FileInfo fileInfo2 = new FileInfo(Application.dataPath + "/SaveFile2.txt");
        if (fileInfo2.Exists) // 파일이 존재하면
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file2 = File.Open(Application.dataPath + "/SaveFile2.txt", FileMode.Open);
            if(file2.Length > 0) // 내용이 있을 때
            {
                SNLData data2;
                data2 = (SNLData)bf.Deserialize(file2); // 직렬화된 것을 Data 형식으로 바꿈
                int moneyNow = 2000 - data2.usedMoney; // 기본값 2000
                AboutF2.GetComponent<Text>().text = "FILE 2 : DAY " + data2.day +
                                                        " / " + moneyNow + "원" +
                                                        " / " + "닭 " + data2.chickenCount + "마리";
            }
            else
            {
                AboutF2.GetComponent<Text>().text = "FILE 2 : EMPTY";
            }
            file2.Close(); // 파일 닫기
        }
        else
        {
            AboutF2.GetComponent<Text>().text = "FILE 2 : NO FILE";
        }
    }

    public void GoTitle() // 타이틀로
    {
        SceneManager.LoadScene("Title");
    }

    // WhereSave UI의 버튼들
    public void GoFile1()
    {
        theSaveNLoad.CallSaveF1(); // File 1 저장
        Debug.Log("File 1 저장 완료");
        notice.WriteMessage("1번 위치에 저장 완료!");
        whereSave.SetActive(false);
        menuWindow.SetActive(false); // 메뉴 off
        GMScript.isTimerStoped = false; // 시간 흐르기 시작
    }
    public void GoFile2()
    {
        theSaveNLoad.CallSaveF2(); // 저장
        Debug.Log("File 2 저장 완료");
        notice.WriteMessage("2번 위치에 저장 완료!");
        whereSave.SetActive(false);
        menuWindow.SetActive(false); // 메뉴 off
        GMScript.isTimerStoped = false; // 시간 흐르기 시작
    }
    public void XSave()
    {
        Debug.Log("저장 취소");
        whereSave.SetActive(false);
    }

    /*public void GameExit() // 종료하기 버튼 - 없앰
    {
#if UNITY_EDITOR // 타이틀매니저 스크립트 참고
        UnityEditor.EditorApplication.isPlaying = false;
#else        
        Application.Quit();
#endif
    }*/


    // center function
    public void CenterYesBtn()
    {
        if(thePlayerCtr.money >= 500) // 돈 충분
        {
            thePlayerCtr.playerMoneyChange(500, false); // 돈 500원 감소

            Debug.Log("닭 구매 완료");
            notice.WriteMessage("닭 구매 성공!");
            //howRich.GetComponent<Text>().text = "(보유 금액 : " + thePlayerCtr.money + "원)";
            SMScript.SpawnChicken(); // spawn chicken
            BuyChicken.SetActive(false);
            GMScript.isTimerStoped = false; // 구매창 꺼지고 시간 정지 해제
        }
        else // 돈 부족
        {
            thePlayerCtr.playerMoneyChange(500, false); // 잔액 부족 메세지
        }
        // 미해에게... playerMoneyChange 함수로만 쓰려고 했는데 닭 스폰 때문에 이게 최선인 듯...
    }
    public void CenterNoBtn()
    {
        Debug.Log("닭 구매 취소");
        BuyChicken.SetActive(false); // nothing happens
        GMScript.isTimerStoped = false; // 구매창 꺼지고 시간 정지 해제
    }


    // SaveNLoad 관련
    public void WhenRestart()
    {
        menuWindow.SetActive(false); // 메뉴 off
        GMScript.isTimerStoped = false; // 시간 흐르기 시작
    }
}
