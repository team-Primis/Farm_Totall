using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 버튼
using UnityEngine.SceneManagement; // 씬 이동

public class TitleManager : MonoBehaviour
{
    public Image NewBefore; // 기존에 존제하는 이미지
    public Sprite NewAfter; // 바뀌어질 이미지
    public Image ContiBefore;
    public Sprite ContiAfter;

    // Start is called before the first frame update
    void Start()
    {   }

    // Update is called once per frame
    void Update()
    {   }

    public void ChangeNewImage()
    {
        NewBefore.sprite = NewAfter;
    }

    public void ChangeContiImage()
    {
        ContiBefore.sprite = ContiAfter;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("OutSide");
    }
}
