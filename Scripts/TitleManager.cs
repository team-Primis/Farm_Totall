using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 버튼
using UnityEngine.SceneManagement; // 씬 이동

public class TitleManager : MonoBehaviour
{
    public Image NewBefore; // 기존에 존제하는 이미지
    public Sprite NewAfter; // 바뀌어질 이미지
    public Image ContiBefore;
    public Sprite ContiAfter;

    // Start is called before the first frame update
    void Start()
    {   }

    // Update is called once per frame
    void Update()
    {   }

    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
        NewBefore.sprite = NewAfter; // change button image
        SceneManager.LoadScene("OutSide");
    }

    public void OnClickContinue()
    {
        Debug.Log("이어하기");
        ContiBefore.sprite = ContiAfter; // change button image
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
}
