using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour//플레이어가 닿았을 때 맵을 이동시켜줄 포인트 구현. 
{
    public string startPoint;//맵이 이동하면 플레이어가 있을 위치
    private PlayerMove thePlayer;
    private MovingCamera theCamera;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<MovingCamera>();
        thePlayer = FindObjectOfType<PlayerMove>();

        if(startPoint == thePlayer.currentMapName)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position=this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
