using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Item> inventory = new List<Item>();
    private itemDatabase db;

    public int slotX, slotY;    // 인벤토리 가로 세로
    public List<Item> slots = new List<Item>(); // 인벤토리 속성 변수

    private bool showInventory = false;
    // I버튼을 누르면 활성화/비활성화 되는 부울 변수
    public GUISkin skin;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < slotX * slotY; i++)
        {
            //처음에 슬롯 갯수만큼 슬롯 칸에 빈 오브젝트 추가

            //일단 빈 공간 만들기
            slots.Add(new Item());
            // 아이템 슬롯칸에 빈 오브젝트 추가하기
            inventory.Add(new Item());
            // 인벤토리에 추가
        }
        db = GameObject.FindGameObjectWithTag("Database").GetComponent<itemDatabase>();

        //db랑 비교해서, 가지고 있는 것들을 인벤에 표시
        for (int i = 0; i < slotX * slotY; i++)
        {
            if (db.items[i] != null)
            {
                inventory[i] = db.items[i];
                // 디비의 아이템칸에 비어있지 않다면, 저장
            }
            else
            {
                // 디비의 아이템칸이 비어있다면 ?

            }
        }

    }

       

    void Update()
    {
            //inventory.Add(db.items[0]) : DB의 0번째 아이템을 인벤토리에 추가


            if (Input.GetKeyDown(KeyCode.O))
        {
            showInventory = !showInventory;
            // 키를 누를때마다 인벤창이 꺼졌다 켜졌다 함
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;
        // Skin 을 skin 답게 ' ㅅ'
        if(showInventory)
        {
            DrawInventory();
        }
    }

    void DrawInventory()
    {
        int k = 0;
        if (k < slotX * slotY)
        {
            for (int j = 0; j < slotY; j++)
            {

                for (int i = 0; i < slotX; i++)
                {
                    Rect slotRect = new Rect(i * 102 + 100, j * 102 + 30, 100, 100);
                    // 박스 분할하기
                    GUI.Box(slotRect, "", skin.GetStyle("slot background"));
                    // 각 박스의 생성 위치를 설정해주는 곳입니다. skin.GetStyle은 이전에 만들었던 skin을 불러오는 것임

                    // 기능 추가하기
                    slots[k] = inventory[k];
                    if (slots[k].itemName != null)
                    {
                        GUI.DrawTexture(slotRect, slots[k].itemIcon);
                       // Debug.Log(slots[k].itemName);
                    }

                    k++;
                    // 갯수 증가
                }
            }
        }
    }
}
