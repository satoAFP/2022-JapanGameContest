using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MC_Judgment : MonoBehaviour
{
    //���̃X�N���v�g�擾
    MusicJudgment_Sctipt Music_script;
    ColorJudgment_Sctipt Color_script;

    //���enege�̏�Ԃ��擾
    [SerializeField]
    private bool[] clearflag = new bool[2];

    //Obj���̂̔��s��\���E��\���ōČ�
    [SerializeField]
    private GameObject[] objs;

    //2��ނ�enege�̒ʓd��Ԋm�F�p
    [SerializeField, Header("�ʓd�m�Fbool")]
    public bool OK;

    // Start is called before the first frame update
    void Start()
    {
        //���script�擾
        Music_script = this.gameObject.GetComponent<MusicJudgment_Sctipt>();
        Color_script = this.gameObject.GetComponent<ColorJudgment_Sctipt>();
    }

    // Update is called once per frame
    void Update()
    {
        //�t���O�X�V
        clearflag[0]=Music_script.GetEnergization();
        clearflag[1]=Color_script.GetEnergization();

        //�S�v�f��v�����f
        IEnumerable<bool> results = (IEnumerable<bool>)clearflag.Distinct();

        //true,false�ǂ��炩���ނ��ǂ���
        int num = results.Count();

        //bool�^�Ŕ���ł���悤�ɕϊ�
        foreach (bool Check in results)
        {
            //���ނ̃^�C�v�𔻒�
            OK = Check;
          
            //�Е��ʓd�Ȃ�OFF
            if(num==2)
            {
                objs[0].SetActive(true);
                objs[1].SetActive(false);
            }
            //������v
            else if (num == 1)
            {
               //�������ʓd���ĂȂ�����OFF
                if (OK == false)
                {
                    objs[0].SetActive(true);
                    objs[1].SetActive(false);
                   
                }
                //ON!!!
                else
                {
                    //�����ɃN���A�̏ؓI�ȃR�[�h
                    objs[0].SetActive(false);
                    objs[1].SetActive(true);
                }
            }
        }
    }
}
