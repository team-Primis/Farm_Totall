using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    //실제 유저가 가지고 있는 내부 인벤토리에 대한 코드
    public List<Item> characterItems = new List<Item>();
    private itemDatabase db;
    public UIInventory inventoryUI;

    public Item equipedItem;

    public bool isUIItemClickChanged = false;
    public Transform emptrans;

    // Start is called before the first frame update
    void Start()
    {
        db = GameObject.Find("Database").GetComponent<itemDatabase>();
        //잠시 에러 없애기 위함..
        //equipedItem = null;
        putInventory(1); //1번 id 1개
        putInventory(1);
        putInventory(1);
        putInventory(4);

        RemoveItem(4); // item 종류에 상관없이 없애버리기
        UseItem(4); // 소모품 없애기

        
       

        putInventory(2);
        // putInventory(3);
        putInventory(100);
        putInventory(101);



        //아이템 획득한 걸 반영하고 싶다면 인벤토리 스크립트 참조하고 
        //인벤토리스크립트이름.putInventory(아이템코드) 쓰면돼!
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //한번 누르면 열리고 한번 더 누르면 닫힘
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }

        //인벤토리 아이템 클릭 시 뭐가 클릭됐는지 알려줌
        if (isUIItemClickChanged)
        {
            inventoryUI.MoveEmphasizedSlot(emptrans);
            isUIItemClickChanged = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (equipedItem != null && equipedItem.Ename != "") // 장착된 상태 
            {
                if (equipedItem.category == Item.Category.item)
                {
                    UseItem(equipedItem);
                 
                }
            }
        }


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


            itemToAdd.count = plusNum;
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);


            Debug.Log("Added item : " + itemToAdd.Kname);
        }
        else
        {
            itemToAdd.count += plusNum;
            inventoryUI.UpdateItemNumUI(itemToAdd);
            Debug.Log("Item " + itemToAdd.Ename + "count became " + itemToAdd.count);
        }

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
            itemToAdd.count = plusNum;
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);

            Debug.Log("Added item : " + itemToAdd.Kname);
        }
        else
        {
            itemToAdd.count += plusNum;
            inventoryUI.UpdateItemNumUI(itemToAdd);
            Debug.Log("Item " + itemToAdd.Ename + "count became " + itemToAdd.count);
        }

    }




    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public Item CheckForItem(string name)
    {
        return characterItems.Find(item => item.Ename == name);
    }

    public void RemoveItem(int id)
    {
        Item ItemToRemove = CheckForItem(id);
        if (ItemToRemove != null)
        {
            //아이템이 하나밖에 없었는데 제거함 -> 아예 객체 제거
            if (ItemToRemove.count == 1)
            {
                characterItems.Remove(ItemToRemove);
                Debug.Log("Item removed : " + ItemToRemove.Kname);
                inventoryUI.RemoveItem(ItemToRemove);

            }
            else
            {
                ItemToRemove.count--;
                inventoryUI.UpdateItemNumUI(ItemToRemove);
                Debug.Log("Item 하나 제거. 현재 갯수 : " + ItemToRemove.count);
            }

        }
        else
        {
            Debug.Log("아이디가 " + id + "인 아이템은 인벤토리에 존재하지 않습니다.");
        }
    }

    //아이템 사용 : 아이템일 경우 하나 사라짐
    public void UseItem(Item item)
    {
        if (item.Ename != null)
        {
            RemoveItem(item.id);
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
        }
    }

    public void UseItem(string name)
    {
        Item item = db.GetItem(name);
        if (item.Ename != null)
        {
            RemoveItem(item.id);
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
        }
    }

    public void UseItem(int id)
    {
        Item item = db.GetItem(id);
        if (item.Ename != null)
        {
            RemoveItem(item.id);
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
        }
    }
}
