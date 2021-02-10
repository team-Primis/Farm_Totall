using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 버튼, text 편집
using UnityEngine.SceneManagement; // 씬 이동

public class TitleManager : MonoBehaviour
{
    public Image NewBtImg; // 새 게임 버튼의 이미지
    public Sprite NewBefore; // original
    public Sprite NewAfter; // 마우스 닿을 때

    public Image ContiBtImg; // 이어하기 버튼의 이미지
    public Sprite ContiBefore; // original
    public Sprite ContiAfter; // 마우스 닿을 때

    public GameObject Choice;
    public GameObject WarningTxt;

    public SaveNLoad theSaveNLoad;

    // Start is called before the first frame update
    void Start()
    {   
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
    }

    // Update is called once per frame
    void Update()
    {   }

    public void NewEnter()
    {   NewBtImg.sprite = NewAfter;   } // 마우스 들어오면

    public void NewExit()
    {   NewBtImg.sprite = NewBefore;   } // 마우스 나가면

    public void ContiEnter()
    {   ContiBtImg.sprite = ContiAfter;   } // 마우스 들어오면

    public void ContiExit()
    {   ContiBtImg.sprite = ContiBefore;   } // 마우스 나가면

    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
        SceneManager.LoadScene("OutSide");
    }

    public void OnClickContinue()
    {
        Debug.Log("이어하기");
        Choice.SetActive(true); // 파일 선택 창 띄우기
    }

    public void OnClickQuit()
    {
        Debug.Log("게임 종료");
#if UNITY_EDITOR // 에디터에서 실행 중일 때는 플레이 상태 중단으로 대체
        UnityEditor.EditorApplication.isPlaying = false;
#else        
        Application.Quit(); // 실행된 게임 프로그램 종료 - 에디터에선 동작 X
#endif
    }

    public void OnClickX()
    {
        Debug.Log("이어하기 취소");
        Choice.SetActive(false); // 파일 선택 창 끄기
    }

    public void OnClickC1()
    {
        theSaveNLoad.CallLoadF1();
    }

    public void OnClickC2()
    {
        //theSaveNLoad.CallLoadF2();
    }
}
