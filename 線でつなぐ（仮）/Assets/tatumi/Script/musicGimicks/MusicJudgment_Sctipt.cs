using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJudgment_Sctipt : Base_Enegization
{
    //�Q�[���N���A�ƂȂ鉹�ԍ�,���݂̔ԍ�
    [SerializeField]
    private int clearMusic,muisc_num;

    //������Ώہi�҂��҂������p�j
    [SerializeField]
    private GameObject[] objs;

    //�F���������邩
    [SerializeField]
    private bool MCmode;

    // Update is called once per frame
    void Update()
    {
        //�������Ȃ���Β��ڂ�����
        if (MCmode == false)
        {
            objs[0].SetActive(true);
            objs[1].SetActive(false);

            if (clearMusic == muisc_num)
            {
                //�����ɃN���A�̏ؓI�ȃR�[�h
                objs[0].SetActive(false);
                objs[1].SetActive(true);
            }
        }
        else
        {
            //�������Ă��enege���炢����
            if (clearMusic == muisc_num)
            {
                //�����ɃN���A�̏ؓI�ȃR�[�h
                energization = true;
            }
            else
                energization = false;
        }
    }

    public void now_music(int a)
    {
        //���y�ԍ��󂯎��
        muisc_num = a;
    }
}
