using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_music : Base_Color_Script
{

    private const int RIGHT = 0;         //���̃I�u�W�F�N�g
    private const int LEFT = 1;     //����ȊO�̃I�u�W�F�N�g

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    GameObject[] AssistObj = new GameObject[2];

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    private bool[] hit_check = new bool[2];         //�A�V�X�gOBJ(���[�̋�)���������Ă邩�ǂ����`�F�b�N����悤

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string OutColor_name;

    [SerializeField, Header("���ݔF��SE�ԍ�")]
    private int music_num = -1;

    void Start()
    {
        OutColor_name = "OutM&C";
    }

    public void SetCheckRight(bool success)
    {
        hit_check[RIGHT] = success;
    }
    public void SetCheckLeft(bool success)
    {
        hit_check[LEFT] = success;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "MusicOutput")
        {
            if (hit_check[RIGHT] == true && hit_check[LEFT] == true)
            {

                //�d�C���ʂ��Ă��邩�ǂ����m�F�B
                if ((collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == false && energization == true))
                {
                    //�������Ă���Obj�̗D��x(cnt�ϐ�)��0(0�Ȃ炷�łɒE�F����Ă�)�łȂ��A����Obj��菬�����Ȃ�Aenergization�͓r�؂�Ă�̂ŐF��j������B
                    energization = false;
                    music_num = -1;

                }
                else if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true && energization == false)
                {

                    //�ʓd���Ȃ̂ŕK�����������Ă�Ƃ���
                    if (collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num()!=-1)
                        music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                    
                    energization = true;
                   
                }
                
            }
            //�A�V�X�gobj���ق���RelayColor�ƐڐG���Ă��Ȃ���ΒE�F����B
            else if (energization == true)
            {
                energization = false;
                music_num = -1;
               
            }
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            //�d�C���ʂ��Ă��邩�ǂ����m�F�B
            if ((collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == false && energization == true))
            {
                //�������Ă���Obj�̗D��x(cnt�ϐ�)��0(0�Ȃ炷�łɒE�F����Ă�)�łȂ��A����Obj��菬�����Ȃ�Aenergization�͓r�؂�Ă�̂ŐF��j������B
                energization = false;
                music_num = -1;

            }
            else if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true && energization == false)
            {

                //�ڐG���Ă�RelayColor�̃J�E���g���1�傫���l���擾����i����ʍs�ɂ��邽�߁j

                energization = true;
                //�ʓd���Ȃ̂ŕK�����������Ă�Ƃ���
                if (collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num() != -1)
                    music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();

            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MusicOutput")
        {
            if (energization == true)
            {
                energization = false;
                music_num = -1;
              
            }
        }
        else if (collision.gameObject.name.Contains(OutColor_name) == true)
        {
            if (energization == true)
            {
                energization = false;
                music_num = -1;
            }
        }
    }
}
