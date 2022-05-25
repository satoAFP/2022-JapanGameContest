using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade_pop : MonoBehaviour
{
    [SerializeField, Header("�����o��������")] bool text_fade;

    [SerializeField, Header("�t�F�[�h���x"), Range(0f, 0.02f)] float fade_speed;

    [SerializeField, Header("�t�F�[�h���������e�L�X�g")] GameObject text_fade_obj;

    //�ŏ������\��
    private bool first = true;


    private void Start()
    {
        //�\���p�e�L�X�g����
        text_fade_obj.GetComponent<Text>().text = GameObject.Find("stage_clear_check").GetComponent<stage_clear>().text_mem;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�e�L�X�g�\�����������Ƃ�
        if (text_fade)
        {
            //�e�L�X�g���\������؂����Ƃ�
            if (text_fade_obj.GetComponent<Text>().color.a > 1.0f)
            {
                first = false;
            }

            //�e�L�X�g�\��
            if (first)
                text_fade_obj.GetComponent<Text>().color += new Color(0, 0, 0, fade_speed);
            else
            {
                //�e�L�X�g��\��
                text_fade_obj.GetComponent<Text>().color -= new Color(0, 0, 0, fade_speed);

                //���ߓx���グ��
                gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, fade_speed);

                //���S�ɓ��߂��������
                if (gameObject.GetComponent<Image>().color.a < 0)
                    gameObject.SetActive(false);
            }
        }
        else
        {
            //���ߓx���グ��
            gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, fade_speed);

            //���S�ɓ��߂��������
            if (gameObject.GetComponent<Image>().color.a < 0)
                gameObject.SetActive(false);
        }
    }
}
