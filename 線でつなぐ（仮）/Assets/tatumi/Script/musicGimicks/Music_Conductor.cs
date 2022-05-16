using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Conductor : Base_Enegization
{
    //���ݑ����t��SE�̎�ނ𔻕ʁ�����̕������ʂ��Ȃ�����
    [SerializeField, Header("���݂�SE�ԍ��Ɠ����SE�ԍ��ݒ�(�ݒ肵���������ʂ��Ȃ�)")]
    private int music_num,Set_music_num;

    //�����炠���ڍׂ͐X��N����
    [SerializeField]
    private bool Change, Input_Hit;
    GameObject MixObj;

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string  OutColor_name;

    void Start()
    {
        //�F�����y��output�Ǘ�
        //InColor_name = "InM&C";
        OutColor_name = "OutM&C";
    }

    void Update()
    {
        //�ʓd�󋵂ɂ��F�ύX
        if (energization == true)
            GetComponent<Renderer>().material.color = new Color32(71, 214, 255, 200);
        else
            GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 200);

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
        //SE�݂̂̐ݒ�
       if (collision.gameObject.tag == "MusicOutput")
        {
            //�d����On�̑���̂ݍ쓮
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                if (Set_music_num == collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num())
                {
                    //�d�C�͓d���Ȃ̂ŕK���ʂ�̂Ŏ擾�����g�ɑ��
                    music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                    Input_Hit = true;
                    energization = true;
                }
            }
            else
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
       //�����F�̐ݒ薼�O�Ŕ���(out��)
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            //�d����On�̑���̂ݍ쓮
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                if (Set_music_num == collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num())
                {
                    //�d�C�͓d���Ȃ̂ŕK���ʂ�̂Ŏ擾�����g�ɑ��
                    music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                    Input_Hit = true;
                    energization = true;
                }
            }
            else
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
        if (collision.gameObject.tag == "MusicOutput")
        {
            music_num = -1;//�����Ȃ�
            energization = false;
            Input_Hit = false;
        }
        //�����F�̐ݒ薼�O�Ŕ���(out��)
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            music_num = -1;//�����Ȃ�
            energization = false;
            Input_Hit = false;
        }
    }

    //���g�̐F�ԍ���Ԃ�
    public int Remusic_num()
    {
        return music_num;
    }
  
}
