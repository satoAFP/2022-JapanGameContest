using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_music : MonoBehaviour
{
    //音源取得
    [SerializeField]
    private AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //PLが踏んだら一度音ならス
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        audioSource.PlayOneShot(sound1);
    }
}
