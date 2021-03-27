using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerItems : MonoBehaviour
{
    
    //아이템을 담아놓을 보관상자
    public List<Item> container = new List<Item>();

    //아이템 추가 관련
    private containerDatabase db;
    private ContainerUI conUI;
    Item emptyItem;

    void Start()
    {
        db = GameObject.Find("CDatabase").GetComponent<containerDatabase>();
        conUI = GameObject.Find("Canvas2").transform.Find("containerPanel").GetComponent<ContainerUI>();
        emptyItem = new Item(1000, "없음", "empty", " ", Item.Category.empty);

    }


    //해당 id를 가진 아이템을 인벤에서 보관상자에 넣음 (인벤 스크립트에서 호출)
    public void PutInContainer(int id, int num = 1)
    {
        Item item = db.GetItem(id);
        //해당 아아템이 존재하지 않음
        if (item == null)
        {
            Debug.Log(id + " id 를 가진 아이템은 데이터베이스에 없습니다.");
            return;
        }
        //아이템을 처음 넣을 경우
        if (CheckForItem(id) == null)
        {
            item.count = num;
            //containerItem이라는 보관상자 아이템 보관스크립트에 추가함
            container.Add(item);
            //conUI.AddNewItem(item);
            conUI.isContainerChanged = true;
            //Debug.Log(id + "라는 id를 가진 아이템을 보관상자에 추가합니다. ");
        }
        //아이템이 이미 보관상자에 존재할경우
        else
        {
            item.count += num;
            conUI.UpdateUI(item);
            //Debug.Log(id + "라는 id를 가진 아이템을 " + num + "개 더 추가합니다.");
        }
    }
    
    //바로 그 아이템을 보관상자에 넣음(0327 추가)
    public void PutInContainer(Item item,int num=1)
    {
        if (item != emptyItem)
        {
            //아이템을 처음 넣을 경우
            if (CheckForItem(item.id) == null)
            {
                //아이템을 container List에 추가해주고 UI반영
                item.count = num;
                container.Add(item);
                conUI.isContainerChanged = true;

            }
            else
            {
                //컨테이너에 이미 있는 아이템 갯수를 증가시켜주고 UI반영
                CheckForItem(item.id).count += item.count;
                conUI.isContainerChanged = true;

            }

        }
        else
        {
            Debug.Log("빈 아이템을 넣을 순 없습니다.");
        }
    }


    public Item CheckForItem(int id)
    {
        return container.Find(item => item.id == id);
    }

    public Item CheckForItem(string name)
    {
        return container.Find(item => item.Ename == name);
    }

    public Item GetItem(int index) {
        if(index <= container.Count - 1)
        {
            return container[index];
        }
        else
        {
            return emptyItem;
        }
    }
}
