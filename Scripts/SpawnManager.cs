using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Chicken;
    public GameObject NormalEgg;
    public GameObject GoodEgg;

    public int chickenCount = 0; // 닭 저장 및 로드를 위해 추가한 부분
    public List<GameObject> chickenList = new List<GameObject>();

    public int nEggCount = 0;
    public int gEggCount = 0;
    public List<GameObject> nEggList = new List<GameObject>();
    public List<GameObject> gEggList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {    }

    public void SpawnChicken()
    {
        chickenCount += 1; // 개수 및 고유 ID 반영
        GameObject chicken = Instantiate(Chicken);
        chickenList.Add(chicken); // spawn 하면서 리스트에 추가
        float chickenX = UnityEngine.Random.Range(-7.0f, 13.0f); // center 근처
        float chickenY = UnityEngine.Random.Range(-4.0f, 4.0f);
        chicken.transform.position = new Vector2(chickenX, chickenY); // 닭 생성할 위치
    }

    public void SpawnNEgg()
    {
        nEggCount += 1; // 개수 반영
        GameObject negg = Instantiate(NormalEgg);
        nEggList.Add(negg); // spawn 하면서 리스트에 추가
        float neggX = UnityEngine.Random.Range(17.0f, 23.0f); // 집 아래쪽
        float neggY = UnityEngine.Random.Range(0.0f, 3.0f);
        negg.transform.position = new Vector2(neggX, neggY);
    }

    public void SpawnGEgg()
    {
        gEggCount += 1; // 개수 반영
        GameObject gegg = Instantiate(GoodEgg);
        gEggList.Add(gegg); // spawn 하면서 리스트에 추가
        float geggX = UnityEngine.Random.Range(17.0f, 23.0f); // 집 아래쪽
        float geggY = UnityEngine.Random.Range(0.0f, 3.0f);
        gegg.transform.position = new Vector2(geggX, geggY);
    }
}
