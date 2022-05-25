using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stage_clear : MonoBehaviour
{
    //�X�e�[�W�N���A��
    [System.NonSerialized] public bool[] Stage_clear = new bool[6];

    //�N���A��������
    [System.NonSerialized] public bool clear;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        Stage_clear[0] = true;
        for (int i = 1; i < 6; i++)
            Stage_clear[i] = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�N���A������
        if (clear)
        {
            //���ꂼ��̃X�e�[�W���N���A���Ă��邩���f
            for (int i = 1; i < 6; i++)
            {
                if (SceneManager.GetActiveScene().name == "Stage" + i)
                {
                    Stage_clear[i] = true;
                }
            }
        }

        //�X�e�[�W�Z���N�g�ɖ߂�ƃN���A�t���O�߂�
        if (SceneManager.GetActiveScene().name == "STAGE_SELECT")
        {
            clear = false;
        }
    }
}
