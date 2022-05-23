using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAssist : MonoBehaviour
{
    //�A�V�X�g�����]�I�u�W�F�N�g
    [SerializeField]
    private GameObject RotateObj;

    private GameObject ConductorObj;

    //���̂ƐڐG���Ă邩�̐���
    [SerializeField]
    private bool hitting_conductor;

    [SerializeField]
    private int power = 0;

    public bool GetHitConductor() => hitting_conductor;

    public void Update()
    {
        if(ConductorObj!=null)
        {
            //�ڐG���Ă���ConductorObj���≏�̂Ɨ��ꂽ�Ƃ��ARotateObj�ɓd�C��ʂ��AConductorObj��j��
            if(!ConductorObj.GetComponent<Conductor_Script>().GetInsulator())
            {
                hitting_conductor = true;
                //�d�͂����邩�m�F
                if (ConductorObj.GetComponent<Conductor_Script>().GetPower() > 1)
                {
                    power = ConductorObj.gameObject.GetComponent<Conductor_Script>().GetPower();
                    RotateObj.GetComponent<Rotate_Script>().SetPower(power);
                    ConductorObj = null;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag=="Conductor")
        {
            hitting_conductor = true;
            //�≏�̂ƐڐG���Ă�����I�u�W�F�N�g���擾
            if (collider.gameObject.GetComponent<Conductor_Script>().GetInsulator())
            {
                hitting_conductor = false;
                ConductorObj = collider.gameObject;
            }
            //RotateObj�ɓd�C��ʂ�
            else if(collider.gameObject.GetComponent<Conductor_Script>().GetEnergization())
            {
                power = collider.gameObject.GetComponent<Conductor_Script>().GetPower();
                RotateObj.GetComponent<Rotate_Script>().SetPower(power);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Conductor")
        {
            hitting_conductor = false;
            if(ConductorObj!=null)
            {
                ConductorObj = null;
            }
        }
    }
}
