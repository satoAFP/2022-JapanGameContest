using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Enegization : MonoBehaviour
{
    [SerializeField]
    protected bool energization=false;        //�d�C���ʂ��Ă邩�ǂ���


    //energization�̃Z�b�^�[�B
    public bool GetEnergization()
    {
        return energization;
    }
    public void SetEnergization(bool electoric)
    {
        energization = electoric;

    }
}
