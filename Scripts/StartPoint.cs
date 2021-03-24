using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour//플레이어가 닿았을 때 맵을 이동시켜줄 포인트 구현. 
{
    public string startPoint;//맵이 이동하면 플레이어가 있을 위치
    private PlayerMove thePlayer;//플레이어무브 스크립트.
    private MovingCamera theCamera;//카메라 움직여주는 스크립트.
    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<MovingCamera>();
        thePlayer = FindObjectOfType<PlayerMove>();

        if(startPoint == thePlayer.currentMapName)//스타트포인트의 이름이 플레이어의 현재 맵 이름과 같으면(플레이어가 집밖으로 나오면)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);//카메라의 위치를 스타트포인트의 위치로 바꿔줌(z 값은 원래 값 그대로).
            thePlayer.transform.position=this.transform.position;//플레이어의 위치로 스타트포인트의 위치로 바꿔줌.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
