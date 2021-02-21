using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 닭과 달걀 스폰 함수 및 정보 담고 있음

    public GameObject Chicken;
    public GameObject NormalEgg;
    public GameObject GoodEgg;

    public int chickenCount = 0; // 닭 저장 및 로드를 위해 추가한 부분
    public List<GameObject> chickenList = new List<GameObject>();

    public int nEggCount = 0;
    public int gEggCount = 0;
    public List<GameObject> nEggList = new List<GameObject>();
    public List<GameObject> gEggList = new List<GameObject>();

    // 집 출입 시 닭과 달걀 로드 관련
    public List<float> chickenXp = new List<float>();
    public List<float> chickenYp = new List<float>();
    public List<int> chickenHp = new List<int>();
    public List<bool> chickenCe = new List<bool>();
    public List<float> neggXp = new List<float>();
    public List<float> neggYp = new List<float>();
    public List<float> geggXp = new List<float>();
    public List<float> geggYp = new List<float>();
    public bool GoOut = false;

    // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {    
        if(GoOut)
        {
            GoOut = false;
            Invoke("LoadSpawn", 1); // 씬 바꿀 시간 부여
        }

    }

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


    // 집에서 나온 뒤 쓸 함수들
    public void LoadSpawn()
    {
        LoadChicken();
        LoadEgg();
    }

    // 집 들어갈 땐 닭과 달걀의 위치도 저장하기
    // 집 나올 땐 닭과 달걀 다 삭제하고 새로 스폰, 위치 불러오기
    public void ClearChicken() // 들어갈 떄
    {
        // 위치 저장 후 리스트에서 삭제 (프리팹은 씬 이동하면 사라져서 파괴할 필요 없음)
        for (int i = 0; i < chickenCount; i++)
        {
            chickenXp.Add(chickenList[i].transform.position.x);
            chickenYp.Add(chickenList[i].transform.position.y);
            chickenHp.Add(chickenList[i].GetComponent<Chicken>().happy);
            chickenCe.Add(chickenList[i].GetComponent<Chicken>().checkEgg);
        }
        /*for (int i = 0; i < chickenCount; i++)
        {
            Destroy(chickenList[i].gameObject);
        }*/
        chickenList.Clear();
    }
    public void LoadChicken() // 나올 때
    {
        // 스폰 후 위치 데이터 비우기
        for (int i = 0; i < chickenCount; i++)
        {
            GameObject chicken = Instantiate(Chicken);
            chickenList.Add(chicken); // spawn 하면서 리스트에 추가
            chicken.transform.position = new Vector2(chickenXp[i], chickenYp[i]); // 닭 생성할 위치
            chickenList[i].GetComponent<Chicken>().happy = chickenHp[i];
            chickenList[i].GetComponent<Chicken>().checkEgg = chickenCe[i];
        }
        chickenXp.Clear();
        chickenYp.Clear();
        chickenHp.Clear();
        chickenCe.Clear();
    }
    public void ClearEgg() // 들어갈 떄
    {
        // 위치 저장 후 리스트에서 삭제 (프리팹은 씬 이동하면 사라져서 파괴할 필요 없음)
        for (int i = 0; i < nEggCount; i++)
        {
            neggXp.Add(nEggList[i].transform.position.x);
            neggYp.Add(nEggList[i].transform.position.y);
        }
        for (int i = 0; i < gEggCount; i++)
        {
            geggXp.Add(gEggList[i].transform.position.x);
            geggYp.Add(gEggList[i].transform.position.y);
        }
        /*for (int i = 0; i < nEggCount; i++)
        {
            Destroy(nEggList[i].gameObject);
        }
        for (int i = 0; i < gEggCount; i++)
        {
            Destroy(gEggList[i].gameObject);
        }*/
        nEggList.Clear();
        gEggList.Clear();
    }
    public void LoadEgg() // 나올 때
    {
        // 스폰 후 위치 데이터 비우기
        for (int i = 0; i < nEggCount; i++)
        {
            GameObject negg = Instantiate(NormalEgg);
            nEggList.Add(negg);
            negg.transform.position = new Vector2(neggXp[i], neggYp[i]);
        }
        for (int i = 0; i < gEggCount; i++)
        {
            GameObject gegg = Instantiate(GoodEgg);
            gEggList.Add(gegg);
            gegg.transform.position = new Vector2(geggXp[i], geggYp[i]);
        }
        neggXp.Clear();
        neggYp.Clear();
        geggXp.Clear();
        geggYp.Clear();
    }
}
