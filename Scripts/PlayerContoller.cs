using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    private float speed = 0.1f;
    public bool freeze = false;
    public int numGE = 0; // 보유 중인 좋은 알의 개수
    public int numNE = 0; // 보유 중인 보통 알의 개수

    // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {
        if (freeze==false) // ↔ freeze==true : 플레이어 고정
        {
            // Moving up down left right
            if (Input.GetKey(KeyCode.W))
            {   this.transform.Translate(0, speed, 0);  }
            if (Input.GetKey(KeyCode.S))
            {   this.transform.Translate(0, -speed, 0);  }
            if (Input.GetKey(KeyCode.A))
            {   this.transform.Translate(-speed, 0, 0);  }
            if (Input.GetKey(KeyCode.D))
            {   this.transform.Translate(speed, 0, 0);  }
        }
    }

    public void FreezePlayer()
    {   freeze = true;   }

    public void MovePlayer()
    {   freeze = false;   }

    void OnTriggerEnter2D(Collider2D coll)
    { // 알 이름 -> tag로 변경
        if(coll.gameObject.name == "GoodEgg") // GoodEgg가 닿으면
        {
            numGE += 1; // 보유 개수 하나 증가
            Destroy(coll.gameObject); // 해당 알 화면에서 제거
        }
        else if(coll.gameObject.name == "NormalEgg") // NormalEgg가 닿으면
        {
            numNE += 1; // 보유 개수 하나 증가
            Destroy(coll.gameObject); // 해당 알 화면에서 제거
        }
    } // player가 trigger (collider+rigidbody(중력=0))

    // 할 일 : 창이 여러 개 중복으로 뜨면? -> 플레이어 멈추면 해결
    // 창 떠 있을 땐 플레이어 멈추게?
    // 몇 초 뒤에 창을 꺼야 하나
    // 충돌이 아니라 "스페이스 바"로 하려면?
}
