using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferMap : MonoBehaviour//플레이어가 일정 위치에 왔을 때 플레이어를 다른 맵으로 이동시켜줌.
{
    public string transferMapName; //이동할 맵의 이름.
    private PlayerMove thePlayer;

    // 성현
    public SpawnManager SMScript;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();
        
        // 성현
        SMScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // 성현
            if(SMScript.DoClearNum == 1)
            {
                SMScript.DoClearNum += 1;
                SMScript.ClearChicken();
                SMScript.ClearEgg();
            }
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

        thePlayer.currentMapName=transferMapName;
        SceneManager.LoadScene(transferMapName);
        // 지현이에게... starting point 좌표 조금만 옮겨주면 좋을 것 같아 너무 박혀서 시작하는 듯...☆
    }
}
