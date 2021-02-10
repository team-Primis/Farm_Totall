using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 이동
//using UnityEngine.UI; // for UI text

public class MenuControl : MonoBehaviour
{
    // 서브메뉴(=일시정지) 화면 + Load + Save (2/3 수정 - SaveNLoad 스크립트로)
    // GameManager에 붙임!

    public GameObject menuWindow; // 직접 넣어줘야 함
    public GameObject whereSave; // 직접 넣어줘야 함

    public GameManager GMScript; // for isMenuOpen
    public SaveNLoad theSaveNLoad;

    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        theSaveNLoad = GameObject.Find("GameManager").GetComponent<SaveNLoad>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel")) // esc 버튼
        {
            if(menuWindow.activeSelf) // 켜져있을 때
            {
                menuWindow.SetActive(false); // 메뉴 off
                GMScript.isTimerStoped = false; // 시간 흐르기 시작
            }
            else // 꺼져있을 때
            {
                menuWindow.SetActive(true); // 메뉴 on
                GMScript.isTimerStoped = true; // 시간 정지 시작
            }
        }

        if(Input.GetKeyDown(KeyCode.F9)) // 임시 - 타이틀 버튼으로 옮길 예정
        {
	        // 불러오기
	        theSaveNLoad.CallLoadF1();
        }

        if(menuWindow.activeSelf)
        {
            GMScript.isMenuOpen = true;
        }
        else
        {
            GMScript.isMenuOpen = false;
        }
        // 메뉴 on → 시간(menucontrol), 닭(Chicken), 플레이어(playermove) 정지
    }

    public void GoConti() // 게속하기
    {
        menuWindow.SetActive(false); // 메뉴 off
        GMScript.isTimerStoped = false; // 시간 흐르기 시작
    }

    public void GoSave() // 저장하기
    {
        whereSave.SetActive(true); // 저장할 파일 선택할 UI
        // 저장 완료 후 whereSave를 다시 false로 바꾼 후 menuWindow false로 해야 함
        // 아래에 구현 중
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
        whereSave.SetActive(false);
        menuWindow.SetActive(false); // 메뉴 off
        GMScript.isTimerStoped = false; // 시간 흐르기 시작
    }
    public void GoFile2()
    {
        /*theSaveNLoad.CallSaveF2(); // 저장*/
        Debug.Log("File 2 저장 완료");
        whereSave.SetActive(false);
        menuWindow.SetActive(false); // 메뉴 off
        GMScript.isTimerStoped = false; // 시간 흐르기 시작
    }

    /*public void GameExit() // 종료하기 버튼 - 없앰
    {
#if UNITY_EDITOR // 타이틀매니저 스크립트 참고
        UnityEditor.EditorApplication.isPlaying = false;
#else        
        Application.Quit();
#endif
    }*/
}
