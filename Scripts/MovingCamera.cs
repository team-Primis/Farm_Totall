using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed;
    static public MovingCamera instance;
    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
            height = Camera.main.orthographicSize;
            width = height * Screen.width / Screen.height;
            instance = this;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    // Update is called once per frame
    void Update()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*cameraSpeed);
        float Ix = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, - Ix + center.x, Ix + center.x);
        float Iy = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -Iy + center.y, Iy + height);

        transform.position = new Vector3(clampX, clampY, -10f); 
    }
}
