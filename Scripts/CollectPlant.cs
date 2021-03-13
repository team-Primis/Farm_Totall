using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPlant : MonoBehaviour
{

    public Transform thePlayer;
    public inventory Inven;
    public Stemina stM;
    public SpawningPlant sP;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player").GetComponent<Transform>();
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
        sP = GameObject.Find("SpawningPlant").GetComponent<SpawningPlant>();
    }

    // Update is called once per frame
    void Update()
    {
        Harvestit();
    }

    public void Harvestit()
    {
      

        Vector2 theplayerPosition = thePlayer.position;//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


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
                        GameObject notwateredplant = hit.collider.gameObject;
                        PlantLoad pL = notwateredplant.GetComponent<PlantLoad>();
                        if(pL.i >= pL.didItBloomed && pL.i < pL.toMuchWilted)
                        {
                            if (hit.collider.gameObject.name == "BlueFlower(Clone)")
                            {
                                Inven.putInventory(41, 1);
                            }
                            else if (hit.collider.gameObject.name == "Pumpkin(Clone)")
                            {
                                Inven.putInventory(40, 1);
                            }

                            Destroy(notwateredplant);
                            sP.createdPlant.Remove(notwateredplant);
                            sP.PlantXp.Remove(notwateredplant.transform.position.x);
                            sP.PlantYp.Remove(notwateredplant.transform.position.y);
                            stM.UseHp(3f);
                        }

                               
                       

                    }
                }
            }

        }



    }
}
