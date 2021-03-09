using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLoad : MonoBehaviour
{

    public float plantTimer;
    public Animator anim;
    public int i = 0;
    public GameObject watered;
    public int didItBloomed;
    public int toMuchWilted;
    public Transform thePlayer;
    public bool iswatered=false;
    public inventory Inven;
    public GameManager GMscript;
    public int wantedGrowthValue;
    public SpriteRenderer wsr;
    public float thisTime;

    //일시정지 창 등등이 켜져 있지 않을 때만 돌아가도록 설정
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        thePlayer = GameObject.Find("Player").GetComponent<Transform>();
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        plantTimer = GMscript.timer;
        thisTime = GMscript.timer;
        wsr = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        //일시정지 창 등등이 켜져 있지 않을 때만 돌아가도록 설정

        plantTimer += Time.deltaTime*GMscript.speedUp;

        if (plantTimer >=thisTime+wantedGrowthValue*60)
        {
            plantTimer = GMscript.timer;
            thisTime = GMscript.timer;
            
          
            wsr.enabled = false;
            if (i<didItBloomed)
            {
                if (iswatered == true)
                {
                    i++;
                    anim.SetInteger("One", i);
                    iswatered = false;
                }
            }


          else if(i>=didItBloomed && i<toMuchWilted)
            {
                Harvestit();
                iswatered = false;
                i++;
                anim.SetInteger("One", i);
            }

          else if( i>=toMuchWilted)
            {
                Destroy(this.gameObject);
            }
        }
    }

   
    
        
    
    void Harvestit()
    {
        
        Vector2 theplayerPosition = thePlayer.position;//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        Vector2 distance = theplayerPosition - mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            Debug.Log(hit.collider.gameObject);
            if (hit.collider != null)
            {
                
                if (hit.collider.CompareTag("Plant"))
                {
                   
                    if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//마우스 왼클릭을 하는 중에는
                    {

                        Debug.Log("수확함");
                        if (hit.collider.gameObject.name == "Flower(Clone)")
                        {
                            Inven.putInventory(41, 1);
                        }
                        else if (hit.collider.gameObject.name == "Pumpkin(Clone)")
                        {
                            Inven.putInventory(40, 1);
                        }
                        
                        Destroy(this.gameObject);

                    }
                }
            }

        }



    }

 



}
