using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferMap : MonoBehaviour//플레이어가 일정 위치에 왔을 때 플레이어를 다른 맵으로 이동시켜줌.
{
    public string transferMapName; //이동할 맵의 이름.
    private PlayerMove thePlayer;//플레이어의 스크립트를 가져옴.
    public SpawningDirt sD;//스포닝덜트 스크립트 가져옴.
    public SpawningPlant sP;//스포닝플랜트 스크립트 가져옴.
    // 성현
    public SpawnManager SMScript;//스포닝매니져 가져옴.
    public AudioClip goingIn;//문 여는 소리.
    public AudioClip goingOut;//문 닫는 소리.
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();
        
        // 성현
        SMScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        sD = GameObject.Find("SpawningDirt").GetComponent<SpawningDirt>();
        sP = GameObject.Find("SpawningPlant").GetComponent<SpawningPlant>();
    }

    // Update is called once per frame
    //집밖에서 집안으로 이동하는 콜라이더에 접촉 시 씬 로드하는 함수.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")//플레이어가 부딪치면
        {
            SoundManager.instance.SFXPlay("GoIn", goingIn);//문 여는 소리 재생.
            
            // 성현
            if (SMScript.DoClearNum == 1)
            {
                SMScript.DoClearNum += 1; // 씬 바뀌는 거 대기하면서 충돌을 여러 번 하길래 추가함
                SMScript.ClearChicken();
                SMScript.ClearEgg();
            }
            
            sD.GoIn = true;
            sP.GoIn = true;
            thePlayer.currentMapName = transferMapName;//이동할 씬을 현재 맵 이름으로 할당시켜줌.
            
            Invoke("NowGoIn", 1); // 씬 바뀌기 전에 닭이랑 알 저장할 시간 벌려고 지연시킴
            
            // 주석 묶은 부분은 지현이가 원래 쓴 부분
            /*thePlayer.currentMapName=transferMapName;
            SceneManager.LoadScene(transferMapName);*/

        }
    }

    // 성현
    public void NowGoIn()
    {

        
        SMScript.GoIn = true; // 성현 (02/25)
        SMScript.DoClearNum = 1; // 성현 (02/28)
        
        
      
        SceneManager.LoadScene(transferMapName);//집안 씬을 로드함.
        SoundManager.instance.SFXPlay("GoOut", goingOut);//문 닫는 소리를 재생시킴.


    }
}
