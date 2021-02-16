using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Input Output
using System.Runtime.Serialization.Formatters.Binary; // binary 파일로 변환
using UnityEngine.SceneManagement; // scene 전환

public class SaveNLoad : MonoBehaviour
{
    // 현재 GameManager에 붙어있음
    // 근데 타이틀에도 있어야 함 - gamemanager를 파괴X

    public bool doLoadF1 = false;
    public bool doLoadF2 = false;

    void Update()
    {
        if(doLoadF1 == true)
        {
            doLoadF1 = false;
            Debug.Log("File 1 로드 시작 - 1");
            Invoke("RealLoadF1", 4); // 씬 바꿀 시간 기다림
        }
        else if(doLoadF2 == true)
        {
            //doLoadF2 = false;
            //Debug.Log("File 2 로드 중");
            //Invoke("RealLoadF2", 4); // 씬 바꿀 시간 기다림
        }
    }

    private PlayerMove thePlayerMove; // 플레이어 좌표, 씬 이름
    private GameManager theGameManager; // 날짜, 시간, stamina
    private PlayerControll thePlayerControll; // 보유금액, laborCount
    private SpawnManager theSpawnManager; // 닭 개수, 행복도, checkEgg, 각 달걀 개수
    private inventory theInventory; // 인벤템 ID, 인벤템 개수

    public SNLData data; // SNLData 이용할 것임

    private Vector2 vector; // player 위치 불러올 곳

    // 나중에 CallSaveF2, CallLoadF2 만들 거임
    public void CallSaveF1()
    {
        thePlayerMove = FindObjectOfType<PlayerMove>();
        theGameManager = FindObjectOfType<GameManager>();
        thePlayerControll = FindObjectOfType<PlayerControll>();
        theSpawnManager = FindObjectOfType<SpawnManager>();
        theInventory = FindObjectOfType<inventory>();

        data.playerX = thePlayerMove.transform.position.x;
        data.playerY = thePlayerMove.transform.position.y;
        data.sceneName = thePlayerMove.currentMapName;

        data.day = theGameManager.day;
        data.timer = theGameManager.timer;
        data.stamina = theGameManager.stamina;

        data.money = thePlayerControll.money;
        data.laborCount = thePlayerControll.laborCount;

        data.chickenCount = theSpawnManager.chickenCount;
        for(int i = 0; i < theSpawnManager.chickenCount; i++)
        {
            data.happy.Add(theSpawnManager.chickenList[i].GetComponent<Chicken>().happy);
            data.checkEgg.Add(theSpawnManager.chickenList[i].GetComponent<Chicken>().checkEgg);
        }

        data.nEggCount = theSpawnManager.nEggCount;
        data.gEggCount = theSpawnManager.gEggCount;

        for(int i = 0; i < theInventory.characterItems.Count; i++)
        {
            data.characterItemsID.Add(theInventory.characterItems[i].id);
            data.characterItemsCnt.Add(theInventory.characterItems[i].count);
        }

        //Debug.Log("기초 저장 성공");

        // 비우지 않으면 로드 할 때마다 똑같은 게 더 생김
        //data.playerItemInventory.Clear();
        //data.playerItemInventoryCount.Clear();
        //data.playerEquipItem.Clear();

        //for(int i = 0; i < theDatabase.var_name.Length; i++)
        //{
        //    data.varNameList.Add(theDatabase.var_name[i]);
        //    data.varNumberList.Add(theDatabase.var[i]);
        //}

        //for(int i = 0; i < theDatabase.switch_name.Length; i++)
        //{
        //    data.swNameList.Add(theDatabase.switch_name[i]);
        //    data.swList.Add(theDatabase.switches[i]);
        //}

        //List<Item> itemList = theInven.SaveItem();
        //for(int i = 0; i < itemList.Count; i++) // 인벤토리
        //{
        //    Debug.Log("인벤토리 아이템 저장 성공 : " + itemList[i].itemID);
        //    data.playerItemInventory.Add(itemList[i].itemID);
        //    data.playerItemInventoryCount.Add(itemList[i].itemCount);
        //}

        //for(int i = 0; i < theEquip.equipItemList.Length; i++) // 장비창 (배열이라 length)
        //{
        //    Debug.Log("장착된 아이템 저장 성공 : " + theEquip.equipItemList[i].itemID);
        //    data.playerEquipItem.Add(theEquip.equipItemList[i].itemID);
        //}

        // 물리적인 파일로 저장
        BinaryFormatter bf = new BinaryFormatter(); // 2진 파일로 변환
        FileStream file = File.Create(Application.dataPath + "/SaveFile1.txt");
        // FileStream : 파일 입출력기 - 이 프로젝트가 설치된 폴더에 (= Asset 폴더)
        // 경로 + 파일 이름 (확장자는 아무렇게나 써도 됨)
        bf.Serialize(file, data); // data class에 담긴 정보를 file 파일에 기록하고 직렬화
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }

    public void CallLoadF1() // 씬 로드
    {
        // Save와 순서 반대로
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/SaveFile1.txt");
        if (fileInfo.Exists) // 파일이 존재하면
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/SaveFile1.txt", FileMode.Open);
            // 원래) if(file != null && file.Length > 0)
            if(file.Length > 0) // 내용이 있을 때
            {
                data = (SNLData)bf.Deserialize(file); // 직렬화된 것을 Data 형식으로 바꿈

                doLoadF1 = true;

                // 씬 이동
                Debug.Log("File 1 씬 로드");

                thePlayerMove = FindObjectOfType<PlayerMove>();
                thePlayerMove.currentMapName = data.sceneName;
                SceneManager.LoadScene(data.sceneName);
                // 다른 씬의 것은 참조 불가, 씬 이동 후의 명령어는 실행 안 됨
            }
            else
            {
                Debug.Log("File 1 비었음");
            }
            file.Close(); // 파일 닫기
        }
        else
        {
            Debug.Log("File 1 없음");
        }
    }

    public void RealLoadF1() // 파일 로드
    {
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/SaveFile1.txt");
        if (fileInfo.Exists) // 파일이 존재하면
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/SaveFile1.txt", FileMode.Open);
            // 원래) if(file != null && file.Length > 0)
            if(file.Length > 0) // 내용이 있을 때
            {
                data = (SNLData)bf.Deserialize(file); // 직렬화된 것을 Data 형식으로 바꿈

                thePlayerMove = FindObjectOfType<PlayerMove>();
                theGameManager = FindObjectOfType<GameManager>();
                thePlayerControll = FindObjectOfType<PlayerControll>();
                theSpawnManager = FindObjectOfType<SpawnManager>();
                theInventory = FindObjectOfType<inventory>();

                Debug.Log("File 1 로드 시작 - 2");

                vector.Set(data.playerX, data.playerY);
                thePlayerMove.transform.position = vector;

                theGameManager.day = data.day;
                theGameManager.timer = data.timer;
                theGameManager.stamina = data.stamina;

                thePlayerControll.money = data.money;
                thePlayerControll.laborCount = data.laborCount;

                theSpawnManager.chickenCount = 0;
                for(int i = 0; i < data.chickenCount; i++)
                {
                    theSpawnManager.SpawnChicken();
                } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                for(int i = 0; i < data.chickenCount; i++)
                {
                    theSpawnManager.chickenList[i].GetComponent<Chicken>().happy = data.happy[i];
                    theSpawnManager.chickenList[i].GetComponent<Chicken>().checkEgg = data.checkEgg[i];
                }

                theSpawnManager.nEggCount = 0;
                theSpawnManager.gEggCount = 0;
                for(int i = 0; i < data.nEggCount; i++)
                {
                    theSpawnManager.SpawnNEgg();
                } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                for(int i = 0; i < data.gEggCount; i++)
                {
                    theSpawnManager.SpawnGEgg();
                } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨

                for(int i = 0; i< data.characterItemsID.Count; i++)
                {
                    theInventory.putInventory(data.characterItemsID[i], data.characterItemsCnt[i]);
                } // 맨 초기에는 아이템 없음 -> 하나씩 넣기
                // 아이템이 도구일 때 제외하는 코드 추가할 것!

                //theDatabase.var = data.varNumberList.ToArray(); // List → Array
                //theDatabase.var_name = data.varNameList.ToArray();
                //theDatabase.switches = data.swList.ToArray();
                //theDatabase.switch_name = data.swNameList.ToArray();

                //for(int i = 0; i < theEquip.equipItemList.Length; i++)
                //{
                //    for(int x = 0; x < theDatabase.itemList.Count; x++)
                //    {
                //        if(data.playerEquipItem[i] == theDatabase.itemList[x].itemID)
                //        {
                //             theEquip.equipItemList[i] = theDatabase.itemList[x];
                //             Debug.Log("장착 아이템 로드됨 : " + theEquip.equipItemList[i].itemID);
                //             break;
                //        }
                //    }
                //}

                //List<Item> itemsList = new List<Item>();
                //for(int i = 0; i < data.playerItemInventory.Count; i++)
                //{
                //    for(int x = 0; x < theDatabase.itemList.Count; x++)
                //    {
                //        if(data.playerItemInventory[i] == theDatabase.itemList[x].itemID)
                //        {
                //             itemList.Add(theDatabase.itemList[x]);
                               // 저장할 땐 ID 값만, 불러올 땐 아이템 통째로
                //             Debug.Log("인벤토리 로드됨 : " + theDatabase.itemList[x].itemID);
                //             break;
                //        }
                //    }
                //}

                // 위까지만 하면 아이템이 1개만 들어가므로 개수 고려해 줘야 함
                //for(int i = 0; i < data.playerItemInventoryCount.Count; i++)
                //{
                //    itemList[i].itemCount = data.playerItemInventoryCount[i];
                //}

                // 반영 (이거 안 하면 그냥 0 나옴(?))
                //theInven.LoadItem(itemList);
                //theEquip.ShowTxt();

                //GameManager theGM = FindObjectOfType<GameManager>();
                //theGM.LoadStart();

                Debug.Log("File 1 로드 완료");
            }
            file.Close(); // 파일 닫기
        }
    }
}
