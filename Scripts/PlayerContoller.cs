using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public GameObject BuyChicken; // for moving control
    private float speed = 0.1f;
    public int numGE = 0; // 보유 중인 좋은 알의 개수
    public int numNE = 0; // 보유 중인 보통 알의 개수

    // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {
        // BuyChicken 창이 켜져있으면 player 못 움직임
        if (BuyChicken.activeSelf == false)
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "GoodEgg") // GoodEgg가 닿으면
        {
            numGE += 1; // 보유 개수 하나 증가
            Destroy(coll.gameObject); // 해당 알 화면에서 제거
        }
        else if(coll.gameObject.tag == "NormalEgg") // NormalEgg가 닿으면
        {
            numNE += 1; // 보유 개수 하나 증가
            Destroy(coll.gameObject); // 해당 알 화면에서 제거
        }
    } // 계란이 trigger (collider+rigidbody(중력=0))

    // 창이 여러 개 중복으로 뜨면?
    // 몇 초 뒤에 창을 꺼야 하나 -> 여유 있을 때 할 일
    // 충돌이 아니라 "스페이스 바"로 하려면? -> 수정해야 할 부분! (닭)
}
