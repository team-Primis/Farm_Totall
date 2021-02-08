using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 버튼, text 편집
using UnityEngine.SceneManagement; // 씬 이동

public class TitleManager : MonoBehaviour
{
    public Image NewBefore; // 기존에 존제하는 이미지
    public Sprite NewAfter; // 바뀌어질 이미지
    public Image ContiBefore;
    public Sprite ContiAfter;

    public GameObject Choice;
    public GameObject WarningTxt;

    // Start is called before the first frame update
    void Start()
    {   }

    // Update is called once per frame
    void Update()
    {   }

    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
        NewBefore.sprite = NewAfter; // change button image - 마우스 닿는 동안만으로 수정할 것
        SceneManager.LoadScene("OutSide");
    }

    public void OnClickContinue()
    {
        Debug.Log("이어하기");
        ContiBefore.sprite = ContiAfter; // change button image - 마찬가지
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
        //저장한 파일이 존재한다면
        Debug.Log("File 1 이어하기");
        //SceneManager.LoadScene("OutSide");
        
        //저장한 파일이 존재하지 않는다면
        //WarningTxt.SetActive(true); // 몇 초 동안만
    }

    public void OnClickC2()
    {
        //저장한 파일이 존재한다면
        Debug.Log("File 2 이어하기");
        //SceneManager.LoadScene("OutSide");

        //저장한 파일이 존재하지 않는다면
        //WarningTxt.SetActive(true); // 몇 초 동안만
    }
}
