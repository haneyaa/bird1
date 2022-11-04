using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    public static AudioClips Instance;//单例

    public AudioClip[] audios;//声音特效数组
    public AudioSource asource;//音乐、特效播放组件

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        asource = GetComponent<AudioSource>();//查找特效播放组件
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
