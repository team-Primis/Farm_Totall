using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLoad : MonoBehaviour
{

    public float timer;
    public Animator anim;
    private int i = 0;
    public GameObject watered;
    public int didItBloomed;
    public int toMuchWilted;
    public Transform thePlayer;
    public bool iswatered=false;
    public inventory Inven;
    //일시정지 창 등등이 켜져 있지 않을 때만 돌아가도록 설정
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        thePlayer = GameObject.Find("Player").GetComponent<Transform>();
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();

    }

    // Update is called once per frame
    void Update()
    {

        //일시정지 창 등등이 켜져 있지 않을 때만 돌아가도록 설정

        timer += Time.deltaTime;
        
        if (timer >= 6)
        {
            timer = 0;



            i++;
            anim.SetInteger("One", i);
            iswatered = false;
        }
        if (i >= didItBloomed)
        {
            Harvestit();
            

        }

        if (i >= toMuchWilted)//시든 지 너무 오래 지나면 파괴됨.
        {
            Destroy(this.gameObject);
        }







    }

    void Harvestit()
    {

        Vector2 theplayerPosition = thePlayer.position;//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 themousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기마다 이동하는 것처럼 보이기 위해 올림하여 마우스 위치 재설정.
        Vector2 distance = theplayerPosition - mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Plant"))
                {

                    if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//마우스 왼클릭을 하는 중에는
                    {

                        Debug.Log("수확함");
                        Destroy(this.gameObject);
                        Inven.putInventory(41, 1);

                    }
                }
            }

        }



    }

   



}
