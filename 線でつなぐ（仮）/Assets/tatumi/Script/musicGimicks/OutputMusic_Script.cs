using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputMusic_Script : Base_Enegization
{
    //���g�̐F�ԍ�
    [SerializeField, Header("���݂�SE�ԍ�")]
    private int music_num;

    //�ڍׂ͐X��N��
    [SerializeField]
    private bool Change,Input_Hit;
    GameObject MixObj;

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string InColor_name,OutColor_name;

    //�F��������
    private bool MCmode = false;
    void Start()
    {
        //�F��IN.OUT�ǂ������擾����\������
        InColor_name = "InM&C";
        OutColor_name = "OutM&C";
    }

    void Update()
    {
        //�F�����̏ꍇ�F�ύX�͎ז�
        if (MCmode == false)
        {
            if (energization == true)
                GetComponent<Renderer>().material.color = new Color32(71, 214, 255, 200);
            else
                GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 200);
        }

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
        if (collision.gameObject.tag == "Insulator")
        {
            music_num = -1;//�����Ȃ�
            energization = false;
            Input_Hit = false;
        }
        //SE����݂̂̏ꍇ(In)
        else if (collision.gameObject.tag == "MusicInput")
        {
            if (collision.gameObject.GetComponent<InputMusic_Script>().GetEnergization() == true)
            {
                //�擾�����g�ɑ��
                music_num = collision.gameObject.GetComponent<InputMusic_Script>().REset_num();
                energization = true;
                Input_Hit = true;
                MCmode = false;
            }
            else
            {
                //OFF�ꍇ
                music_num = -1;
                energization = false;
                Input_Hit = false;
                MCmode = false;
            }
        }
        //SE����݂̂̏ꍇ(Out)
        else if (collision.gameObject.tag == "MusicOutput")
        {
            //�d����On�̑���̂ݍ쓮
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                //�擾�����g�ɑ��
                music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                Input_Hit = true;
                energization = true;
                MCmode = false;
            }
            else if(collision.gameObject.GetComponent<OutputMusic_Script>())
            {
                if (Input_Hit == false)
                {
                    //OFF�ꍇ
                    music_num = -1;
                    Input_Hit = false;
                    energization = false;
                    MCmode = false;
                }
            }
        }
        //�F������̏ꍇ(In)
        else if (collision.gameObject.name.Contains(InColor_name) == true)
        {
            if (collision.gameObject.GetComponent<InputMusic_Script>().GetEnergization() == true)
            {
                //�擾�����g�ɑ��
                music_num = collision.gameObject.GetComponent<InputMusic_Script>().REset_num();
                energization = true;
                Input_Hit = true;
                MCmode = true;
            }
            else
            {
                //OFF�ꍇ
                music_num = -1;
                energization = false;
                Input_Hit = false;
                MCmode = true;
            }
        }
        //�F������̏ꍇ(Out)
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            //�d����On�̑���̂ݍ쓮
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                //�擾�����g�ɑ��
                music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                Input_Hit = true;
                energization = true;
                MCmode = true;
            }
            else if (collision.gameObject.GetComponent<OutputMusic_Script>())
            {
                if (Input_Hit == false)
                {
                    //OFF�ꍇ
                    music_num = -1;
                    Input_Hit = false;
                    energization = false;
                    MCmode = true;
                }
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //�������Ƃ��d�����ԍ�������(SE)
        if (collision.gameObject.tag == "MusicInput")
        {
            music_num = -1;//�����Ȃ�
            energization = false;
            Input_Hit = false;
            MCmode = false;
        }
        else if (collision.gameObject.tag == "MusicOutput")
        {
            music_num = -1;//�����Ȃ�
            energization = false;
            Input_Hit = false;
            MCmode = false;
        }
        //�������Ƃ��d�����ԍ�������(�F�t��)
        else if (collision.gameObject.name.Contains(InColor_name) == true)
        {
            music_num = -1;//�����Ȃ�
            energization = false;
            Input_Hit = false;
            MCmode = true;
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            music_num = -1;//�����Ȃ�
            energization = false;
            Input_Hit = false;
            MCmode = true;
        }
    }

    //��������
    public int Remusic_num()
    {
        return music_num;
    }
   

}
