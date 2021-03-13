using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferMapFromInsideToOutside : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름.
    private PlayerMove thePlayer;
    public SpawningDirt sD;
    // 성현
    public SpawnManager SMScript;
    public SpawningPlant sP;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // 성현
            if(SMScript.DoLoadNum == 1)
            {
                SMScript.DoLoadNum += 1;
                SMScript.GoOut = true; // 씬 바뀐 후 닭이랑 알 로드하려고
            }
            sD.GoOut = true;
            sP.GoOut = true;
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
            thePlayer.transform.position = new Vector2(24, 8);
        }

    }
}

