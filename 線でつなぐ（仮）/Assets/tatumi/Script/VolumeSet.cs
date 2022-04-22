using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSet : MonoBehaviour
{
    Slider m_Slider;//���ʒ����p�X���C�_�[

    void Awake()
    {
        m_Slider = GetComponent<Slider>();
        m_Slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        m_Slider.value = AudioListener.volume;
        //�X���C�_�[�̒l���ύX���ꂽ�特�ʂ��ύX����
        m_Slider.onValueChanged.AddListener((sliderValue) => AudioListener.volume = sliderValue);
    }

    private void OnDisable()
    {
        m_Slider.onValueChanged.RemoveAllListeners();
    }
}
