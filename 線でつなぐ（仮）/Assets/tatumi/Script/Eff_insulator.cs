using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Eff_insulator : MonoBehaviour
{
    //���݂������Ă�Obj�ۑ�
    [SerializeField]
    private GameObject[] HitCon;

    //��L�̖��O�p
    [SerializeField]
    private string[] HitCon_name;

    //��L��Obj��60FPS�Ǘ��łǂꂾ���ێ����邩��ۑ����邽�߂̕ϐ�
    [SerializeField]
    private int[] HitCon_tonumber;
    

    private int i,nowObj=1;

	void FixedUpdate()
    {
        //�ۑ����邢���ꂩ��Obj�����莞�Ԍo���Ă邩�m�F
        for (int i = 0; i != 10; i++)
        {
            HitCon_tonumber[i]++;
            //�����Ă�Ȃ�Exit���聕�l��������
            if (HitCon_tonumber[i] == 30)
            {
                //�d��ON,OFF�̓R���_�N�^�[���ŊǗ�
                HitCon[i].GetComponent<Conductor_Script>().EffExit();
                HitCon[i] = null;
                HitCon_name[i] = null;
                //nowObj--;
            }
        }

       
        
    }

	private void OnParticleCollision(GameObject other)
	{
        //���̂Ƃ���NOMAL�d���̂ݔ���
        if (other.gameObject.tag == "Conductor")
        {
            //���O���擾���݂̕ۑ����Ă��閼�O�Ɉ�v���Ȃ���
            string result = HitCon_name.SingleOrDefault(value => value == other.gameObject.name);

            //�����ɓ���
            if (result == null)
            {
               // Debug.Log(result);
                //���A�C�e����T���ĂԂ�����
                for (int i = 0; i != 10; i++)
                {
                    if (HitCon[i] == null)
                    {
                        //�V�K�Ȃ����
                        HitCon[i] = other.gameObject;
                        HitCon_name[i] = HitCon[i].name;
                        HitCon_tonumber[i] = 0;
                        break;
                    }
                }
            }
            else
            {
                //Stay��number��0�ɂ���
                for(int i=0;i!=10;i++)
                {
                    if (HitCon_name[i] == HitCon[i].name)
                    {
                        HitCon_tonumber[i] = 0;
                        break;
                    }
                }
            }

            // ������������������d��OFF
            //�d��ON,OFF�̓R���_�N�^�[���ŊǗ�
            other.gameObject.GetComponent<Conductor_Script>().PowerOff();
             
        }
	}

    
}
