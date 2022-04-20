using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Eff_insulator : MonoBehaviour
{
	[SerializeField]
	private bool Stay=false;

    [SerializeField]
    private GameObject[] HitCon;

    [SerializeField]
    private string[] HitCon_name;

    [SerializeField]
    private int[] HitCon_tonumber;
    

    private int i,nowObj=1;

	void FixedUpdate()
    {

        for (int i = 0; i != 10; i++)
        {
            HitCon_tonumber[i]++;
            if (HitCon_tonumber[i] == 30)
            {
                HitCon[i].GetComponent<Conductor_Script>().EffExit();
                HitCon[i] = null;
                HitCon_name[i] = null;
                //nowObj--;
            }
        }

       
        
    }

	private void OnParticleCollision(GameObject other)
	{
        if (other.gameObject.tag == "Conductor")
        {
            string result = HitCon_name.SingleOrDefault(value => value == other.gameObject.name);

            

            if (result == null)
            {
                Debug.Log(result);
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
            other.gameObject.GetComponent<Conductor_Script>().PowerOff();
             
        }
	}

    
}
