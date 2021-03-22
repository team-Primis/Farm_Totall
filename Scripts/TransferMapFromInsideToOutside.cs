using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferMapFromInsideToOutside : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름.
    private PlayerMove thePlayer; //플레이어를 움직이게 해주는 스크립트를 가져옴.
    public SpawningDirt sD; //스포닝덜트 스크립트를 가져옴.
    // 성현
    public SpawnManager SMScript; //스포닝매니져를 가져옴.
    public SpawningPlant sP; //스포닝플랜트를 가져옴.
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
    //집안에서 집밖으로 나갈 수 있는 콜라이더에 부딪치면 집밖으로 나가는 함수.
    private void OnTriggerEnter2D(Collider2D collision)//만약 충돌이 일어나면
    {
        if (collision.gameObject.name == "Player")//그 충돌 주체가 플레이어면
        {
            SoundManager.instance.SFXPlay("GoIn", goingIn);//사운드매니져에 만들어둔 음악 재생 함수로 문 여는 소리를 재생시킴.
            // 성현
            if (SMScript.DoLoadNum == 1)
            {
                SMScript.DoLoadNum += 1;
                SMScript.GoOut = true; // 씬 바뀐 후 닭이랑 알 로드하려고
            }
            sD.GoOut = true;
            sP.GoOut = true;
            thePlayer.currentMapName = transferMapName;//플레이어의 현재 맵 이름을 이동하는 맵 이름으로 지정시켜줌.

            Invoke("DelayLoad", 1f);//소리 재생이 제대로 안 되길래 씬 로드를 조금 지연시킴.
            
            

        }

    }

    //씬 로드를 지연시키기 위한 함수.
    public void DelayLoad()
    {


        SceneManager.LoadScene(transferMapName);//집밖 씬을 로드해줌.
        SoundManager.instance.SFXPlay("GoIn", goingOut);//사운드매니져의 음악 재생 함수를 이용하여 문 닫는 소리를 재생시켜줌.
        thePlayer.transform.position = new Vector2(24, 8);//플레이어의 위치를 집 옆으로 바꿔줌.

    }
}

