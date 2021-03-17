
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Slider loadSlider;
    public bool isReset = false;

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
        if(isReset)
        {
            isReset = false;
            if(loadSlider.value != 0)
            {
                loadSlider.value = 0;
            }
        }

        // 0312 수정
        if(loadSlider.value >= 1.0f)
        {
            loadSlider.value = 1.0f;
            Invoke("ExitLoading", 0.5f);
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
