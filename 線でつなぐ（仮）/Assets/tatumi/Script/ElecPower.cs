using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecPower : MonoBehaviour
{
    public int mynumber;
    //���L�ł���Q�[���I�u�W�F�N�g�^
    public GameObject maneger;
    public bool flag,change;

    //�X�N���v�g�^�i�C���[�W�́j��錾���O�ŕς�����I
    ElecPower_maneger SC_EPmane;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        change = false;

        //���g��˂����ށi�������j
        SC_EPmane = maneger.GetComponent<ElecPower_maneger>();
    }

    // Update is called once per frame
    void FiexdUpdate()
    {
        //�֐��ɂ��Ă�������
        if(flag != change)
        {
            change = flag;
            //�}�e���A���ύX
            if(change==true)
            {
                SC_EPmane.flags[mynumber] = flag;
                SC_EPmane.Change(flag, mynumber);
            }
            else
            {

            }
        }
        
    }
}
