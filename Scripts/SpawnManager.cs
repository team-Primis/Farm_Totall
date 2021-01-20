using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Chicken;
    public GameObject NormalEgg;
    public GameObject GoodEgg;

    // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {    }

    public void SpawnChicken()
    {
        GameObject chicken = Instantiate(Chicken);
        chicken.transform.position = new Vector2(3,-3); // 닭 생성할 위치
    }

    public void SpawnNEgg()
    {
        GameObject negg = Instantiate(NormalEgg);
        negg.transform.position = new Vector2(7,-3);
    }

    public void SpawnGEgg()
    {
        GameObject gegg = Instantiate(GoodEgg);
        gegg.transform.position = new Vector2(9,-3);
    }
    
    // 닭 및 계란이 생성되는 위치는?
}
