using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour
{

    public AudioClip shellExplosionAudioClip;

    void OnTriggerEnter(Collider other)
    {
        //在某个时刻进行播放        
        AudioSource.PlayClipAtPoint(shellExplosionAudioClip, transform.position);
        //销毁物体，不会报错
        Destroy(this.gameObject);
    }

}

