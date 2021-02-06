using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Input Output
using System.Runtime.Serialization.Formatters.Binary; // binary 파일로 변환
using UnityEngine.SceneManagement; // scene 전환

public class SaveNLoad : MonoBehaviour
{

    // 직렬화 - 세이브와 로딩에 있어서 필수적인 속성! (컴터가 읽고 쓰기 쉽게)
    [System.Serializable] // class 만들 때 직렬화 필요
    public class Data // 모든 세이브 기록들 담을 곳
    {
        // 저장할 것 ~
        // 플레이어 위치 (좌표, 씬) / 닭 개수 (위치는 랜덤? 저장?) / 시간 & Day
        // 인벤, 상자, 들고 있는 것 / 땅 상태 심어진 것 농작물 상태, 달걀
        // 달걀을 하 랜덤 위치에 스폰해야 하는데...
        // 닭의 경우 행복도, 플레이어의 경우 체력?
        // 농작물 상태를 1, 2, 3으로 해야 할 것 같악 애니메이션으로 넣으면 너무 애매 ㅜ

        // Vector3 등의 class는 직렬화가 안 됨 (자료형만 직렬화 가능)
        public float playerX;
        public float playerY;
        public float playerZ;

        public int playerLv;
        public int playerHP;
        public int playerMP;

        public int playerCurrentHP;
        public int playerCurrnetMP;
        public int playerCurrentEXP;

        public int playerATK;
        public int playerDEF;
        public int playerHPR;
        public int playerMPR;

        public int added_atk;
        public int added_def;
        public int added_hpr;
        public int added_mpr;

        public List<int> playerItemInventory; // 아이템의 ID 값 넣으면 됨
        public List<int> playerItemInventoryCount; // 몇 개 소지했는지
        public List<int> playerEquipItem; // 아이템 ID

        public string mapName;
        public string sceneName;

        // Database에 선언한 리스트들
        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNameList;
        public List<float> varNumberList;
    }

    private PlayerMove thePlayer;
    //private PlayerStat thePlayerStat;
    //private DatabaseManager theDatabase;
    //private Inventory theInven;
    //private Equipment theEquip;

    public Data data; // Data 이용할 것임

    private Vector3 vector; // player 위치 불러올 곳

    public void CallSave()
    {
        thePlayer = FindObjectOfType<PlayerMove>();
        //thePlayerStat = FindObjectOfType<PlayerStat>();
        //theDatabase = FindObjectOfType<DatabaseManager>();
        //theInven = FindObjectOfType<Inventory>();
        //theEquip = FindObjectOfType<Equipment>();

        data.playerX = thePlayer.transform.position.x;
        data.playerY = thePlayer.transform.position.y;
        data.playerZ = thePlayer.transform.position.z;

        //data.playerLv = thePlayerStat.character_Lv;
        //data.playerHP = thePlayerStat.hp;
        //data.playerMP = thePlayerStat.mp;
        //data.playerCurrentHP = thePlayerStat.currentHP;
        //data.playerCurrentMP = thePlayerStat.currentMP;
        //data.playerCurrentEXP = thePlayerStat.currentEXP;
        //data.playerATK = thePlayerStat.atk;
        //data.playerDEF = thePlayerStat.def;
        //data.playerHPR = thePlayerStat.recover_hp;
        //data.playerMPR = thePlayerStat.recover_mp;
        //data.added_atk = theEquip.added_atk;
        //data.added_def = theEquip.added_def;
        //data.added_hpr = theEquip.added_hpr;
        //data.added_mpr = theEquip.added_mpr;

        //data.mapName = thePlayer.currentMapName;
        //data.sceneName = thePlayer.currentSceneName;

        Debug.Log("기초 저장 성공");

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
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");
        // FileStream : 파일 입출력기 - 이 프로젝트가 설치된 폴더에 (= Asset 폴더)
        // 경로 + 파일 이름 (확장자는 아무렇게나 써도 됨
        bf.Serialize(file, data); // data class에 담긴 정보를 file 파일에 기록하고 직렬화
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }

    public void CallLoad()
    {
        // Save와 순서 반대로

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);
        if(file != null && file.Length > 0) // 파일이 있고 내용도 있을 때
        {
            data = (Data)bf.Deserialize(file); // 직렬화된 것을 Data 형식으로 바꿈

            thePlayer = FindObjectOfType<PlayerMove>();
            //thePlayerStat = FindObjectOfType<PlayerStat>();
            //theDatabase = FindObjectOfType<DatabaseManager>();
            //theInven = FindObjectOfType<Inventory>();
            //theEquip = FindObjectOfType<Equipment>();

            //thePlayer.currentMapName = data.mapName;
            //thePlayer.currentSceneName = data.sceneName;

            vector.Set(data.playerX, data.playerY, data.playerZ);
            thePlayer.transform.position = vector;

            //thePlayerStat.character_Lv = data.playerLv;
            //thePlayerStat.hp = data.playerHP;
            //thePlayerStat.mp = data.playerMP;
            //thePlayerStat.currentHP = data.playerCurrentHP;
            //thePlayerStat.currentMP = data.playerCurrentMP;
            //thePlayerStat.currentEXP = data.playerCurrentEXP;
            //thePlayerStat.atk = data.playerATK;
            //thePlayerStat.def = data.playerDEF;
            //thePlayerStat.recover_hp = data.playerHPR;
            //thePlayerStat.recover_mp = data.playerMPR;
            //theEquip.added_atk = data.added_atk;
            //theEquip.added_def = data.added_def;
            //theEquip.added_hpr = data.added_hpr;
            //theEquip.added_mpr = data.added_mpr;

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

            // scene 이동
            SceneManager.LoadScene(data.sceneName);
            // 다른 씬의 것은 참조 불가, 씬 이동 후의 명령어는 실행 안 됨
            // → 다른 스크립트 이용해서 카메라 설정 ()
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다");
        }

        file.Close();
    }
}
