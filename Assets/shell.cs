using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour
{

    public AudioClip shellExplosionAudioClip;

    void OnTriggerEnter(Collider other)
    {
        //��ĳ��ʱ�̽��в���        
        AudioSource.PlayClipAtPoint(shellExplosionAudioClip, transform.position);
        //�������壬���ᱨ��
        Destroy(this.gameObject);
    }

}

