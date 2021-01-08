using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start()
    {

        // items.Add(new Item(string img, string name, int id, string def, ItemType type))
        //items. Add(new Item(이미지이름, 이름, 아이템번호, 설명, 타입)
        items.Add(new Item("0", "potato", 1001, "This is delicious", Item.ItemType.Use));
        items.Add(new Item("1", "sweetPotato", 1002, "This is delicious", Item.ItemType.Use));
        items.Add(new Item("2", "pumpkin", 1003, "This is delicious", Item.ItemType.Use));
        items.Add(new Item("3", "sprinkler", 2001, "This is for watering plants", Item.ItemType.Tool));




        //DB가 있다면 이렇게 수동으로 안해도 됨! 나중에 DB를 고려하자
    }

}
