using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputMusic_Script : Base_Enegization
{
    [SerializeField]
    private int music_num;

    [SerializeField]
    private bool Change,Input_Hit;
    GameObject MixObj;
    void Start()
    {
       
    }

    void Update()
    {
        if(energization==true)
        GetComponent<Renderer>().material.color = new Color32(71,214, 255, 1);
        else
            GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 1);

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
            if (collision.gameObject.GetComponent<InputMusic_Script>().GetEnergization() == true)
            {
                //�d�C�͓d���Ȃ̂ŕK���ʂ�̂Ŏ擾�����g�ɑ��
                music_num = collision.gameObject.GetComponent<InputMusic_Script>().REset_num();
                energization = true;
                Input_Hit = true;
            }
            else
            {
                //OFF�ꍇ
                music_num = -1;
                energization = false;
                Input_Hit = false;
            }
        }
        else if (collision.gameObject.tag == "MusicOutput")
        {
            //�d����On�̑���̂ݍ쓮
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                //�d�C�͓d���Ȃ̂ŕK���ʂ�̂Ŏ擾�����g�ɑ��
                music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                Input_Hit = true;
                energization = true;
            }
            else if(collision.gameObject.GetComponent<OutputMusic_Script>())
            {
                if (Input_Hit == false)
                {
                    //OFF�ꍇ
                    music_num = -1;
                    Input_Hit = false;
                    energization = false;
                }
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //�������Ƃ��d���F�ԍ�������
        if (collision.gameObject.tag == "MusicInput")
        {
            music_num = -1;//�����Ȃ�
            energization = false;
            Input_Hit = false;
        }
        else if (collision.gameObject.tag == "MusicOutput")
        {
            music_num = -1;//�����Ȃ�
            energization = false;
            Input_Hit = false;
        }
    }

    public int Remusic_num()
    {
        return music_num;
    }
    //public void Set_inputhere(bool a)
    //{
    //    Input_Hit = a;
    //}

    //public bool Get_inputhere()
    //{
    //   return Input_Hit;
    //}

}
