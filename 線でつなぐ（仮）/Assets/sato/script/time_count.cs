using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class time_count : MonoBehaviour
{
    [SerializeField, Header("�I������(�b)")] float total_time;
    [SerializeField, Header("�_���[�W�t�F�[�h")] GameObject fade;
    [SerializeField, Header("shut_out")] GameObject shut_out;
    [SerializeField, Header("���v���������������Ƃ�true")] bool clock_only;

    [SerializeField, Header("SE_�S��")] AudioSource heart_beat;
    [SerializeField, Header("SE_�f��")] AudioSource breath;

    [SerializeField, Header("�Q�[���I�[�o�[�p�l��")] GameObject gameover_panel;


    private int minute;             //��
    private int seconds;            //�b
    private float mem_total_time;   //�I�����ԋL���p
    private int count;              //1�b���݂̃J�E���g
    private bool time_move = true;  //���j���[�\�����͏o���Ȃ�
    private float fade_speed;       //�_���[�W�t�F�[�h�̃t�F�[�h���x
    private float sound_fade_speed; //SE�t�F�[�h�̃t�F�[�h���x

    //�A���ŉ�����Ȃ����߂̔���
    private bool key_check_E = true;

    // Start is called before the first frame update
    void Start()
    {
        fade_speed = 1 / total_time;
        sound_fade_speed = 0.4f / total_time;
        mem_total_time = total_time;
        count = (int)total_time - 1;

        minute = (int)total_time / 60;
        seconds = (int)total_time % 60;
        total_time = (int)total_time % 60;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�|�[�Y�����Ԃ��~�߂�
        if (Input.GetKey(KeyCode.E))
        {
            if (key_check_E)
            {
                if (time_move)
                    time_move = false;
                else
                    time_move = true;
            }
            key_check_E = false;
        }
        else { key_check_E = true; }

        //���Ԃ̃J�E���g����
        if (time_move)
        {
            if (mem_total_time > 0)
            {
                //���̏���
                if (total_time < 0)
                {
                    minute--;
                    total_time = 60;
                }
                total_time -= Time.deltaTime;
                mem_total_time -= Time.deltaTime;
                seconds = (int)total_time;
            }
            else
            {
                heart_beat.volume = 0;
                breath.volume = 0;
            }

            //���Ԃ̕\��
            gameObject.GetComponent<TextMesh>().text = minute.ToString("d2") + ":" + seconds.ToString("d2");

            //���v���������������Ƃ�
            if (!clock_only)
            {
                //�t�F�[�h�ڍs����
                if ((int)mem_total_time == count)
                {
                    fade.GetComponent<Image>().color += new Color(0, 0, 0, fade_speed);
                    heart_beat.volume += sound_fade_speed;
                    breath.volume += sound_fade_speed;
                    count--;
                }

                //���񂾂Ƃ��̃t�F�[�h
                if ((int)mem_total_time <= 0)
                    shut_out.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);

                //�t�F�[�h�ŃQ�[���I�[�o�[���m�炳���
                if (shut_out.GetComponent<Image>().color.a >= 1.0f)
                {
                    gameover_panel.GetComponent<Text>().color += new Color(0, 0, 0, 0.01f);

                    //���N���b�N�ŃX�e�[�W�Z���N�g
                    if (Input.GetMouseButton(0))
                    {
                        //GameObject.Find("stage_clear_check").GetComponent<stage_clear>().text_mem = "";
                        SceneManager.LoadScene("LOSEENDING");
                    }
                }

            }

        }
    }
}
