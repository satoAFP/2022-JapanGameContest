using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject MapcipSlect;//�}�b�v�`�b�v�X�N���v�g

    //�}�b�v�`�b�v�̏�ɑ��̃I�u�W�F�N�g������Ă���Ƃ�
    private void OnTriggerEnter(Collider other)
    {
        //���C���[�ԍ�10�FTarget
        if(other.gameObject.layer == 10)
        {
            MapcipSlect.GetComponent<MapcipSlect>().Onblock = true;
        }
    }

    //�}�b�v�`�b�v�̏�ɑ��̃I�u�W�F�N�g������Ă���Ƃ�
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            MapcipSlect.GetComponent<MapcipSlect>().Onblock = false;
        }
    }
}
