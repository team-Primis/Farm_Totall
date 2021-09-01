using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    private Camera cam;
    private float startingFOV;
   
    public float minFOV;
    public float maxFOV;
    public float zoomRate;

    public float zoomLerpSpeed;
    private float currentFOV;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        startingFOV = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        currentFOV = cam.orthographicSize;

        if (Input.GetKeyDown(KeyCode.P))
        {
            currentFOV=startingFOV;
        }


        if (Input.GetKeyDown(KeyCode.Equals))
        {
            currentFOV /= zoomRate;

        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            currentFOV *= zoomRate;
        }

        currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
        cam.orthographicSize = currentFOV;
        

    }

}
