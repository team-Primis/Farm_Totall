
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;//싱글톤 기반용.
    public AudioClip[] walkingSoundClip;//걷는 소리.
    public AudioSource audioSource;//미해가 만든 bgm 오디오 소스 가져옴.
    public GameManager GMscript;//게임매니져.
    public AudioClip DaySound;//낮소리.
    public AudioClip NightSound;//밤소리.
    public AudioClip InsideSound;//집안에서 나는 소리.

    //0331 미해 merge 추가 : MenuControl 소통용
    public bool audioStopClicked;


    // Start is called before the first frame update
    public void Awake()
    {
        audioSource =GameObject.Find("DonDestroyGameObject").transform.Find("bgm").GetComponent<AudioSource>();
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (instance==null)//스태틱 인스턴스가 눌일 때는
        {
            instance = this;//씬 이동 시에도 파괴하지 않음.
            DontDestroyOnLoad(instance);//그 이후 씬 이동을 통해 생성되는 모든 복사본의 인스턴스는 눌임.
        }
        else//씬 이동을 통해 생성되는 모든  복사본의 인스턴스는 this가 아니므로
        {
            Destroy(gameObject);//씬 이동 시 없애줌.
        }
        //0331 미해 merge 추가 : MenuControl 소통용
        audioStopClicked = false;

    }

    // Update is called once per frame
    void Update()
    {
       
        SelectBgmClipOutside();
        SelectBgmClipInside();
        
            
     



    }
    //원래는 미해가 만들어둔 오디오 소스로 할라고 했는데 연속으로 마우스를 클릭하니까 소리가 씹히는 경우가 잦아서 마우스 클릭 각각에 반응하도록 이 방법으로 함.
    public void SFXPlay(string sfxName, AudioClip clip)//음악 재생 함수..
    {
        GameObject go = new GameObject(sfxName + "Sound");//새로운 오브젝트를 생성해서
        AudioSource audioSource = go.AddComponent<AudioSource>();//그 오브젝트에 오디오 소스 컴포넌트를 넣어줌.
        audioSource.clip = clip;//그 오디오 소스의 클립을 지정해주고
        audioSource.Play();//소리를 플레이하고
        Destroy(go, clip.length);//소리가 끝나면 이 오브젝트를 없애줌.
    }

    public void SelectBgmClipOutside()//집밖에서 들리는 소리 정하기.
    {

        if (SceneManager.GetActiveScene().name == "OutSide")
        {
            if (5 < GMscript.minute && GMscript.minute < 19)//낮시간에는
            {
                audioSource.clip = DaySound;//낮 소리 플레이.
                
            }
            else
            {
                audioSource.clip = NightSound;//밤에는 밤 소리 플레이.
            }
           
        }
        /*0331 미해 수정
        지현이의 원래 코드 :
        if (!audioSource.isPlaying)
            audioSource.Play();
          */

        //바뀐 코드
        //(원래 코드는 배경음이 플레이 안되고 있으면, 바로 플레이를 시켜서 성현이의 일시정지 기능이 실행되지 않았습니당! 그래서 bool 추가했습니당)
        /*if (!audioSource.isPlaying &&!audioStopClicked)
            audioSource.Play();*/
        // (0402 성현 주석처리)
        // 아래는 바뀐 코드! (성현) - 맨 아래 참고
        if(SceneManager.GetActiveScene().name == "OutSide")
        {
            if (!audioStopClicked && !audioSource.isPlaying && !GMscript.isLoadingOpen)
            {
                audioSource.Play();
            }

            if (GMscript.isLoadingOpen)
            {
                audioSource.Stop();
            }
        }
            
    }

    public void SelectBgmClipInside()//집안에서 들리는 소리 정하기.
    {
        if (SceneManager.GetActiveScene().name == "Inside")
        {
            audioSource.clip = InsideSound;//집안 소리 플레이.
           
        }

        /*0331 미해 수정
         * 지현이의 원래 코드 :
        if (!audioSource.isPlaying)
            audioSource.Play();
          */

        //바뀐 코드
        //(원래 코드는 배경음이 플레이 안되고 있으면, 바로 플레이를 시켜서 성현이의 일시정지 기능이 실행되지 않았습니당! 그래서 bool 추가했습니당)
        /*if (!audioStopClicked && !audioSource.isPlaying)
        {
            audioSource.Play();
        }*/
        // (0402 성현 주석처리)
        // 아래는 바뀐 코드! (성현) - 맨 아래 참고
        if(SceneManager.GetActiveScene().name == "Inside")
        {
            if (!audioStopClicked && !audioSource.isPlaying
                && !GMscript.isLoadingOpen
                && !GameObject.Find("Canvassleep").transform.Find("SleepLoading").gameObject.activeSelf)
            {
                audioSource.Play();
            }

            if (GMscript.isLoadingOpen
                || GameObject.Find("Canvassleep").transform.Find("SleepLoading").gameObject.activeSelf)
            {
                audioSource.Stop();
            }
        }
       
    }
 
}

// 0402 성현 수정~
// (1)
// if (!audioStopClicked && !audioSource.isPlaying) { audioSource.Play(); }
// 이 부분 둘 다 조건문에 넣었습니당
// 미해가 바꾼 건 타이틀 갈 때 stop 했더니 씬 상관없이 계속 다시 재생 시키더라고
// 그래서 타이틀에 노래 계속 나오는 것 같아서 바꿨고 해결됐슴니당
// (2)
// 침대에서 자는 화면(SleepLoading.activeSelf) 및 로딩 화면(isLoadingOpen)에선 BGM 정지!
