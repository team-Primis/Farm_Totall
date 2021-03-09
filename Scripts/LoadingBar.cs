using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Slider loadSlider;
    public bool isReset = false;

    public float plusNum = 0.0f;
    public bool plusBar = false;
    float nextValue = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        loadSlider = GameObject.Find("Canvas2").transform.Find("Loading").transform.
                                                Find("LoadingSlider").GetComponent<Slider>();
        loadSlider.value = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12)) // 임시
        {
            /*loadSlider.value += 0.2f;
            if(loadSlider.value >= 1.0f)
            {
                Invoke("ExitLoading", 0.5f);
            }*/
            IngLoading(0.2f);
        }

        if(Input.GetKeyDown(KeyCode.F11)) // 임시
        {
            ExitLoading();
        }

        if(Input.GetKeyDown(KeyCode.F10)) // 임시
        {
            ResetLoading();
        }

        if(isReset)
        {
            isReset = false;
            if(loadSlider.value != 0)
            {
                loadSlider.value = 0;
            }
        }

        if(plusBar)
        {
            plusBar = false;
            nextValue = loadSlider.value + plusNum;
            if(nextValue <= 1.0f)
            {   loadSlider.value = nextValue;   }
            else // nextValue > 1.0f
            {   loadSlider.value = 1.0f;    }
            plusNum = 0.0f;

            if(loadSlider.value == 1.0f)
            {   Invoke("ExitLoading", 1);   }
        }
    }

    public void IngLoading(float i) // IngLoading(증가시킬값)
    {
        if(plusBar == false)
        {
            plusNum = i;
            plusBar = true;
        }
        else
        {
            Debug.Log("plusBar is already true");
        }
    }

    public void ResetLoading() // 사용할 때마다 Reset 필수
    {
        this.gameObject.SetActive(true);
        isReset = true;
    }

    public void ExitLoading() // 로딩창 종료하고 싶을 때
    {
        loadSlider.value = 0.0f; // 나갈 때 full에서 시작하는 거 해결
        this.gameObject.SetActive(false);
    }
}
