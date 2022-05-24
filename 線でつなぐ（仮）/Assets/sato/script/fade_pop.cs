using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade_pop : MonoBehaviour
{
    [SerializeField, Header("�����o��������")] bool text_fade;

    [SerializeField, Header("�t�F�[�h���x"), Range(0f, 0.02f)] float fade_speed;

    [SerializeField, Header("�t�F�[�h���������e�L�X�g")] GameObject text_fade_obj;


    private bool first = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (text_fade)
        {
            if (text_fade_obj.GetComponent<Text>().color.a > 1.0f)
            {
                first = false;
            }


            if (first)
                text_fade_obj.GetComponent<Text>().color += new Color(0, 0, 0, fade_speed);
            else
            {

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
