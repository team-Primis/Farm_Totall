using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;//싱글톤 기반용.
    public AudioClip[] walkingSoundClip;//걷는 소리.
    // Start is called before the first frame update
    public void Awake()
    {
        if(instance==null)//스태틱 인스턴스가 눌일 때는
        {
            instance = this;//씬 이동 시에도 파괴하지 않음.
            DontDestroyOnLoad(instance);//그 이후 씬 이동을 통해 생성되는 모든 복사본의 인스턴스는 눌임.
        }
        else//씬 이동을 통해 생성되는 모든  복사본의 인스턴스는 this가 아니므로
        {
            Destroy(gameObject);//씬 이동 시 없애줌.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //원래는 미해가 만들어둔 오디오 소스로 할라고 했는데 연속으로 마우스를 클릭하니까 소리가 씹히는 경우가 잦아서 마우스 클릭 각각에 반응하도록 이 방법으로 함.
    public void SFXPlay(string sfxName, AudioClip clip)//음악 재생 함수..
    {
        GameObject go = new GameObject(sfxName + "Sound");//새로운 오브젝트를 생성해서
        AudioSource audioSource = go.AddComponent<AudioSource>();//그 오브젝트에 오디오 소스 컴포넌트를 넣어줌.
        audioSource.clip = clip;//그 오디오 소스의 클립을 지정해주고
        audioSource.Play();//소리를 플레이하고
        Destroy(go, clip.length);//소리가 끝나면 이 오브젝트를 없애줌.
    }

    
 
}
