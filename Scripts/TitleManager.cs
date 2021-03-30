using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 버튼, text 편집
using UnityEngine.SceneManagement; // 씬 이동
using System.IO; // Input Output
using System.Runtime.Serialization.Formatters.Binary; // binary 파일로 변환

public class TitleManager : MonoBehaviour
{
    public Image NewBtImg; // 새 게임 버튼의 이미지
    public Sprite NewBefore; // original
    public Sprite NewAfter; // 마우스 닿을 때

    public Image ContiBtImg; // 이어하기 버튼의 이미지
    public Sprite ContiBefore; // original
    public Sprite ContiAfter; // 마우스 닿을 때

    public GameObject Choice;
    public Text F1Content;
    public Text F2Content;

    public SaveNLoad theSaveNLoad;
    public MenuControl theMenuControl;

    // 소리 추가 (0330)
    AudioSource audioSource;
    public AudioClip BtnSound;

    // Start is called before the first frame update
    void Start()
    {   
        theSaveNLoad = GameObject.Find("GameManager").GetComponent<SaveNLoad>();
        theMenuControl = GameObject.Find("GameManager").GetComponent<MenuControl>();

        GameObject canvas = GameObject.Find("Canvas");
        Choice = canvas.transform.Find("Choice").gameObject;
        F1Content = canvas.transform.Find("Choice").transform.Find("Choice1BT").
                transform.Find("F1Content").GetComponent<Text>();
        F2Content = canvas.transform.Find("Choice").transform.Find("Choice2BT").
                transform.Find("F2Content").GetComponent<Text>();

        theMenuControl.BGAudio.Stop(); // (0327)

        audioSource = GameObject.Find("SoundEffect").GetComponent<AudioSource>(); // (0330)
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
        // Debug.Log("새 게임");
        audioSource.clip = BtnSound;
        audioSource.Play();

        theSaveNLoad.CallNewGame();
    }

    public void OnClickContinue()
    {
        // Debug.Log("이어하기");
        audioSource.clip = BtnSound;
        audioSource.Play();

        Choice.SetActive(true); // 파일 선택 창 띄우기

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
                int moneyNow = 10000 - data1.usedMoney; // 기본값 10000
                F1Content.GetComponent<Text>().text = "DAY " + data1.day +
                                                        " / " + moneyNow + "원" +
                                                        " / " + "닭 " + data1.chickenCount + "마리";
            }
            else
            {
                F1Content.GetComponent<Text>().text = "EMPTY";
            }
            file1.Close(); // 파일 닫기
        }
        else
        {
            F1Content.GetComponent<Text>().text = "NO FILE";
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
                int moneyNow = 10000 - data2.usedMoney; // 기본값 10000
                F2Content.GetComponent<Text>().text = "DAY " + data2.day +
                                                        " / " + moneyNow + "원" +
                                                        " / " + "닭 " + data2.chickenCount + "마리";
            }
            else
            {
                F2Content.GetComponent<Text>().text = "EMPTY";
            }
            file2.Close(); // 파일 닫기
        }
        else
        {
            F2Content.GetComponent<Text>().text = "NO FILE";
        }
    }

    public void OnClickQuit()
    {
        // Debug.Log("게임 종료");
        audioSource.clip = BtnSound;
        audioSource.Play();

        Invoke("QuitThis", 0.2f); // 소리 재생 때문에 delay 둠 (0330)
    }

    public void QuitThis()
    {
#if UNITY_EDITOR // 에디터에서 실행 중일 때는 플레이 상태 중단으로 대체
        UnityEditor.EditorApplication.isPlaying = false;
#else        
        Application.Quit(); // 실행된 게임 프로그램 종료 - 에디터에선 동작 X
#endif
    }

    public void OnClickX()
    {
        // Debug.Log("이어하기 취소");
        audioSource.clip = BtnSound;
        audioSource.Play();

        Choice.SetActive(false); // 파일 선택 창 끄기
    }

    public void OnClickC1()
    {
        // Debug.Log("File 1 이어하기");
        audioSource.clip = BtnSound;
        audioSource.Play();

        theSaveNLoad.CallLoadF1();
    }

    public void OnClickC2()
    {
        // Debug.Log("File 2 이어하기");
        audioSource.clip = BtnSound;
        audioSource.Play();

        theSaveNLoad.CallLoadF2();
    }

    public void OnClickR1() // (0322)
    {
        audioSource.clip = BtnSound;
        audioSource.Play();

        FileInfo fileInfo1 = new FileInfo(Application.dataPath + "/SaveFile1.txt");
        if (fileInfo1.Exists) // 파일이 존재하면
        {
            File.Delete(Application.dataPath + "/SaveFile1.txt");
            File.Delete(Application.dataPath + "/SaveFile1.txt.meta");
            F1Content.GetComponent<Text>().text = "NO FILE";
        }
    }

    public void OnClickR2() // (0322)
    {
        audioSource.clip = BtnSound;
        audioSource.Play();

        FileInfo fileInfo2 = new FileInfo(Application.dataPath + "/SaveFile2.txt");
        if (fileInfo2.Exists) // 파일이 존재하면
        {
            File.Delete(Application.dataPath + "/SaveFile2.txt");
            File.Delete(Application.dataPath + "/SaveFile2.txt.meta");
            F2Content.GetComponent<Text>().text = "NO FILE";
        }
    }
}
