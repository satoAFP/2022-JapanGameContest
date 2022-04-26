using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputMusic_Script : Base_Color_Script
{
    private int music_num;
    GameObject MixObj;
    void Start()
    {
       
    }

    public void OnCollisionEnter(Collision collision)
    {
        //����ΏۍX�V
        if (collision.gameObject.tag == "ColorMix")
        {
            MixObj = collision.gameObject;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "MusicInput")
        {
            //�d�C�͓d���Ȃ̂ŕK���ʂ�̂Ŏ擾�����g�ɑ��
            music_num = collision.gameObject.GetComponent<InputMusic_Script>().REset_num();
            energization = true;
        }
        else if (collision.gameObject.tag == "MusicOutput")
        {
            //�d����On�̑���̂ݍ쓮
            if (collision.gameObject.GetComponent<OutputMusic_Script>().energization == true)
            {
                //�d�C�͓d���Ȃ̂ŕK���ʂ�̂Ŏ擾�����g�ɑ��
                music_num = collision.gameObject.GetComponent<InputMusic_Script>().REset_num();
                energization = true;
            }
            else
            {
                //OFF�ꍇ
                music_num =-1;
                energization = false;
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //�������Ƃ��d���F�ԍ�������
        if (collision.gameObject.tag == "ColorInput")
        {
            music_num = -1;//�����Ȃ�
            energization = false;
        }
    }
}
