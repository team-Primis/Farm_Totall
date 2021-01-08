using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();
    //public Sprite[] sp = Resources.LoadAll<Sprite>("ItemIcons/" + "34x34icons180709");
    // public Sprite[] sprites;

    void Start()
    {
        //sprites = Resources.LoadAll<Sprite>("ItemIcons/34x34icons180709");

        // items.Add(new Item(string img, string name, int id, string def, ItemType type))
        //items. Add(new Item(이미지이름, 이름, 아이템번호, 설명, 타입)
        items.Add(new Item("0", "potato", 1001, "This is delicious", Item.ItemType.Use));
        items.Add(new Item("0", "sweetPotato", 1002, "This is delicious", Item.ItemType.Use));
        items.Add(new Item("0", "pumpkin", 1003, "This is delicious", Item.ItemType.Use));
        items.Add(new Item("0", "sprinkler", 2001, "This is for watering plants", Item.ItemType.Tool));




        // 원하는 만큼 만들어주면 됨(물론 나중에 디비가 생긴다면 이거 안해도 되는건데, 우리는 디비 없이 할거니까 해야됨
    }

}
