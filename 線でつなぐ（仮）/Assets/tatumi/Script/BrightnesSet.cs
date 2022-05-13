using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnesSet : MonoBehaviour
{
    //�摜�i���邳�ݒ�p�̔��F������j
    [SerializeField]
    Image image;

    Slider m_Slider;//���ʒ����p�X���C�_�[

    void Awake()
    {
        //�擾
        m_Slider = GetComponent<Slider>();
        
    }

    void Update()
    {
        //�X���C�_�[���猻�݂̒l�𓧖��x�ɔ��f
        float alpha = m_Slider.value*255.0f;

        image.color = new Color32(0, 0, 0, (byte)alpha);
    }
}
