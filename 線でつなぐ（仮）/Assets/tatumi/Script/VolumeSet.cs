using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�X���C�_�[������Ƃ������Ă������H
[RequireComponent(typeof(Slider))]
public class VolumeSet : MonoBehaviour
{
    Slider m_Slider;//���ʒ����p�X���C�_�[

    void Awake()
    {
        //�X���C�_�[�擾
        m_Slider = GetComponent<Slider>();
        m_Slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        //���ʃ}�X�^�[�I�Ȃ̂��擾�A���f
        m_Slider.value = AudioListener.volume;
        //�X���C�_�[�̒l���ύX���ꂽ�特�ʂ��ύX����
        m_Slider.onValueChanged.AddListener((sliderValue) => AudioListener.volume = sliderValue);
    }

    private void OnDisable()
    {
        //���[����
        m_Slider.onValueChanged.RemoveAllListeners();
    }
}
