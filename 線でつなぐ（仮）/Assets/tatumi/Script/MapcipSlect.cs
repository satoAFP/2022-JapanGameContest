using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapcipSlect : MonoBehaviour
{
    public Material[] mat = new Material[2];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;

    //�Z���N�gON,OFF�Ń}�e���A������
    private bool now_select;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
        now_select = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�N���b�N�̖��m�ȋN�_���Ȃ�����Update�ŏ����i�}�E�X���O�ꂽ�Ƃ��Ȃǁj
        //����Ȃ�Ray�̊O�ꂽ���ɌĂяo�����֐����g������
        if (now_select == true)
        {
            now_select = false;
            mats[0] = mat[1];

            GetComponent<Renderer>().materials = mats;
        }
        else
        {
            if (mats[0] == mat[1])
            {
                mats[0] = mat[0];

                GetComponent<Renderer>().materials = mats;
            }
        }
    }

    public void ChangeMaterial()
    {
        now_select = true;
    }
}
