using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferMap : MonoBehaviour//플레이어가 일정 위치에 왔을 때 플레이어를 다른 맵으로 이동시켜줌.
{
    public string transferMapName; //이동할 맵의 이름.
    private PlayerMove thePlayer;
    // Start is called before the first frame update


    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName=transferMapName;
            SceneManager.LoadScene(transferMapName);
            thePlayer.transform.position = new Vector2(7.5f, -7.0f); // 저장 때문에,,,
        }
    }
}
