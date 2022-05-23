using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Script : Conductor_Script
{

    private const int RIGHT = 0;         //���̃I�u�W�F�N�g
    private const int LEFT = 1;     //����ȊO�̃I�u�W�F�N�g

    public Material[] mat = new Material[2];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    GameObject[] AssistObj = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    public new void Update()
    {
        if (leaving_Conductor == true)
        {
            energization = false;
            power_cnt = 0;
            leaving_Conductor = false;
        }
        if (energization == true)
        {
            //�I�u�W�F�N�g�̐F���V�A���ɂ���
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (energization == false)
        {
            //�I�u�W�F�N�g�̐F���O���[�ɂ���
            GetComponent<Renderer>().material.color = Color.gray;

        }
    }

    public new void OnCollisionExit(Collision c)
    {

        if (c.gameObject.tag == "Conductor")
        {
            //���̂ƐڐG���Ă鑍����1�ւ炷
            contacing_conductor--;
            leaving_Conductor = true;
            //�d�C���Ă邩�̊m�F�p�ϐ���false�ɂ���
            Conductor_hit = false;
        }
    }

    public new void OnCollisionEnter(Collision c)
    {
        //�≏�̂ƐڐG�����Ƃ�
        if (c.gameObject.tag == "Insulator")
        {
            //Insulator_hit��true�ɂ���
            Insulator_hit = true;
        }
        else if (c.gameObject.tag == "Conductor")
        {
            //���̂ɐG�ꂽ��A�����_�łǂꂾ���̓��̂ƐڐG���Ă��邩�J�E���g����
            contacing_conductor++;

                //�V�������̂ɐG�ꂽ��Agiving_conductor,power_gave,energization�������񃊃Z�b�g����
                if (energization == true)
                {
                    GivePowerReSet();
                }
        }
    }

    public new void OnCollisionStay(Collision collision)
    {
        //�d�C���Ւf���鏈���B�≏�̂ƐڐG�A�����̃I�u�W�F�N�g���p���[�J�E���g���傫���I�u�W�F�N�g���≏�̂ƐڐG���Ă���Ɠd�C�Ւf
        if (collision.gameObject.tag == "Conductor")
        {
            if (Insulator_hit || hitting_insulator) 
            {
                energization = false;
            }
            else if (power_cnt >= ELECTORIC_POWER )
            {
                if(AssistObj[RIGHT].GetComponent<RotateAssist>().GetHitConductor()&& AssistObj[LEFT].GetComponent<RotateAssist>().GetHitConductor())
                {
                    energization = true;
                    power_save = power_cnt;
                    collision.gameObject.GetComponent<Conductor_Script>().SetPower(power_cnt - 1);
                    collision.gameObject.GetComponent<Conductor_Script>().SetEnergization(true);
                }
                else
                {
                    energization = false;
                    if(power_cnt>collision.gameObject.GetComponent<Conductor_Script>().GetPower())
                    {
                        collision.gameObject.GetComponent<Conductor_Script>().SetPower(0, power_cnt);
                    }
                }
            }
        }
    }
}
