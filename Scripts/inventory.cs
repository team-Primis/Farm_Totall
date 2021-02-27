using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    //실제 유저가 가지고 있는 내부 인벤토리에 대한 코드
    private itemDatabase db;
    private ContainerDb container;
    private SellingUI sellingUI;


    public List<Item> characterItems = new List<Item>();
    public UIInventory inventoryUI;

    public Button putInBtn;

    public Item equipedItem;

    public bool isUIItemClickChanged = false;
    public Transform emptrans;

    public NoticeText notice;



    public static inventory instance = null;

    Item emptyItem;


    // Start is called before the first frame update
    void Start()
    {
        db = GameObject.Find("Database").GetComponent<itemDatabase>();
        container = GameObject.Find("Canvas2").transform.Find("containerPanel").GetComponent<ContainerDb>();
        sellingUI = GameObject.Find("Canvas2").transform.Find("sellingPanel").GetComponent<SellingUI>();

        putInBtn.onClick.AddListener(PutInContainer);
        
        //테스트용으로 미리 인벤토리에 넣어놓은것들
        putInventory(1,3); 
        putInventory(2);
        putInventory(100);
        putInventory(101);

        putInventory(99,3); // 닭 밥 테스트용 건초

        //안내사항 띄우고 싶을땐 이렇게 쓰기
        notice = GameObject.Find("Notice").GetComponent<NoticeText>();

        emptyItem = new Item(1000, "없음", "empty", " ", Item.Category.empty);

        //아이템 획득한 걸 반영하고 싶다면 인벤토리 스크립트 참조하고 
        //인벤토리스크립트이름.putInventory(아이템코드) 쓰면돼!
    }

    void Awake()
    {
        //한개만 존재하기 위함 ( 싱글턴 )
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

  


     void PutInContainer()
    {
        if (equipedItem != emptyItem)
        {
            container.PutInContainer(equipedItem.id, equipedItem.count);
            equipedItem.count = 0;
            RemoveAll(equipedItem.id);
            equipedItem = null;
            ClearSlot();
        }
        else
        {
            Debug.Log("선택된 아이템이 없어 넣을 수 없습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //I키를 눌렀을때 열리고 닫히게 조절 가능
        if (Input.GetKeyDown(KeyCode.I))
        {
            //한번 누르면 열리고 한번 더 누르면 닫힘
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }

       
        //E키를 눌렀을떄 장착된 아이템이 아이템 카테고리 라면 ( 사용 가능한 아이템 ) 아이템을 사용한다.
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (equipedItem != emptyItem) // 장착된 상태 
            {
                if (equipedItem.category == Item.Category.item)
                {
                    UseItem(equipedItem);
                 
                }
            }
        }


    }
    public void renewSlot()
    {
        inventoryUI.MakeSlotNull();
    }

    public void MoveSlot(Transform trans)
    {
        inventoryUI.MoveEmphasizedSlot(trans);
    }

    public void ClearSlot()
    {
        inventoryUI.MakeSlotClear();
    }

    public Item GetEqippedITem()
    {
        return equipedItem;
    }

    //이 아이디를 가진 아이템을 인벤에 넣을떄 쓰는 함수.
    //해당 id가 아이템 데이터베이스에 존재하는지 확인하고 존재 시 인벤(characterItems)에 넣는다
    public void putInventory(int id, int plusNum = 1)
    {
        //db 확인
        Item itemToAdd = db.GetItem(id);

        //없을경우
        if (itemToAdd == null)
        {
            Debug.Log("해당 item id가 데이터베이스에 없습니다.");
            return;
        }

        //있을 경우
        //캐릭터 인벤에 아이템 추가, 외부 인벤에 그림 바꿈(빈 객체가 있는 칸을 해당 객체의 그림으로 바꿈)

        //해당 아이템을 처음 추가하는 경우 : 인벤토리를 확인했을 때 해당 객체가 없음
        if (CheckForItem(id) == null)
        {

            
            itemToAdd.count += plusNum;
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);


           // Debug.Log("아이템 " + itemToAdd.Kname+"을 "+itemToAdd.count+"개 추가합니다.");
        }
        else
        {
            itemToAdd.count += plusNum;
            inventoryUI.UpdateItemNumUI(itemToAdd);
         //   Debug.Log("아이템" + itemToAdd.Kname + "의 갯수를 "+plusNum+"개 추가해서 현재 갯수는 " + itemToAdd.count+"개 입니다.");
        }
        sellingUI.isItemChanged = true;

    }

    public void putInventory(string name, int plusNum = 1)
    {
        //db 확인
        Item itemToAdd = db.GetItem(name);

        //없을경우
        if (itemToAdd == null)
        {
            Debug.Log("해당 item id가 데이터베이스에 없습니다.");
            return;
        }
        //있을 경우
        //캐릭터 인벤에 아이템 추가, 외부 인벤에 그림 바꿈(빈 객체가 있는 칸을 해당 객체의 그림으로 바꿈)

        //해당 아이템을 처음 추가하는 경우 : 인벤토리를 확인했을 때 해당 객체가 없음
        if (CheckForItem(name) == null)
        {
            itemToAdd.count += plusNum;
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);

            // Debug.Log("아이템 " + itemToAdd.Kname+"을 "+itemToAdd.count+"개 추가합니다.");
        }
        else
        {
            itemToAdd.count += plusNum;
            inventoryUI.UpdateItemNumUI(itemToAdd);
            //   Debug.Log("아이템" + itemToAdd.Kname + "의 갯수를 "+plusNum+"개 추가해서 현재 갯수는 " + itemToAdd.count+"개 입니다.");
        }
        sellingUI.isItemChanged = true;

    }

    
    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public Item CheckForItem(string name)
    {
        return characterItems.Find(item => item.Ename == name);
    }

    public void RemoveAll(int id)
    {
        Item Item = CheckForItem(id);
        if (Item != null)
        {
            Item.count = 0;
            characterItems.Remove(Item);
            //Debug.Log("아이템 " + Item.Kname + "을 인벤토리에서 제거합니다.");
            inventoryUI.RemoveItem(Item);
            sellingUI.isItemChanged = true;
            //만약 삭제한 아이템이 장착하고 있던 아이템이었으면, 장착된 곳에서 삭제하기
            if(equipedItem != emptyItem && equipedItem.id == id)
            {
                equipedItem = emptyItem;
                ClearSlot();
            }
        }
        else
        {
            Debug.Log("아이디가 " + id + "인 아이템은 인벤토리에 존재하지 않습니다.");
        }
    }
    //해당 id를 가진 아이템 1개 제거 : 아이템의 갯수를 1개씩 줄이기. 아이템이 1개-> 0개가 되었을때는 제거.
    public void RemoveItem(int id)
    {
        Item ItemToRemove = CheckForItem(id);
        if (ItemToRemove != null)
        {
            //아이템이 하나밖에 없었는데 제거함 -> 아예 객체 제거
            if (ItemToRemove.count == 1)
            {
                RemoveAll(id);

            }
            else
            {
                ItemToRemove.count--;
                inventoryUI.UpdateItemNumUI(ItemToRemove);
                //Debug.Log("Item 하나를 제거합니다. 현재 남은 갯수 : " + ItemToRemove.count+"개");
            }

        }
        else
        {
            Debug.Log("아이디가 " + id + "인 아이템은 인벤토리에 존재하지 않습니다.");
        }
    }

    public Item GetItem(int index)
    {
        if (index <= characterItems.Count - 1)
        {
            return characterItems[index];
        }
        else
        {
            return null;
        }
    }

    //아이템 사용 : 아이템일 경우 하나 사라짐
    public void UseItem(Item item)
    {
        if (item != null&&item.Ename!= "" && item.count > 0)
        {
            RemoveItem(item.id);
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
            sellingUI.isItemChanged = true;
            notice.WriteMessage(item.Kname + "을 사용하셨습니다.");
        }
        
    }

    
   
    // 0개 이상이면 할거
    public void UseItem(string name)
    {
        Item item = db.GetItem(name);
        if (item != null && item.Ename != ""&&item.count>0)
        {
            RemoveItem(item.id);
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
            sellingUI.isItemChanged = true;
            notice.WriteMessage(item.Kname + "을 사용하셨습니다.");

        }
    }

    public void UseItem(int id)
    {
        Item item = db.GetItem(id);
        if (item != null && item.Ename != "" && item.count > 0)
        {
            RemoveItem(item.id);
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
            sellingUI.isItemChanged = true;
            notice.WriteMessage(item.Kname + "을 사용하셨습니다.");

        }
    }
    
}
