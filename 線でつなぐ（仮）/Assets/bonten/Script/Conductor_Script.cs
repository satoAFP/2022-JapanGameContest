using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    private const int ELECTORIC_POWER = 1;   //1�ȏ�Œʓd

    [SerializeField]
    //�ʓd�ϐ���true�ɂȂ邩�ǂ����m�F����֐�
    private bool energization = false;          //�ʓd���Ă邩
    [SerializeField]
    private bool Conductor_hit = false;          //���̂ƐڐG���Ă邩
    [SerializeField]
    private bool Insulator_hit = false;          //�≏�̂ƐڐG���Ă邩
    [SerializeField]
    private bool Power_hit = false;          //�d���ƐڐG���Ă邩
    [SerializeField]
    private bool hitting_insulator = false;      //����Ɏ������≏�̂ƐڐG���Ă��邱�Ƃ�`���邽�߂̕ϐ�
    [SerializeField]
    private bool leaving_Conductor = false;      //�ڐG���Ă���conductor�Ɨ��ꂽ���Ƃ�`���邽�߂̕ϐ�
    [SerializeField]
    private bool energi_check = false;         //���g���ʓd�������Ƃ��`�F�b�N���߂̕ϐ�
    [SerializeField]
    private bool power_gave = false;             //�������ڐG���Ă铱�̂��ׂĂɓd�C��ʂ������m�F���邽�߂̕ϐ�
    [SerializeField]
    private int power_save = 0;                  //�p���[��0�ɂȂ��Ă����̃I�u�W�F�N�g�̎����Ă��p���[�����Ƃ��Ƃǂꂮ�炢����������ۑ����Ă������߂̕ϐ�
    [SerializeField]
    private int power_cnt = 0;                   //�d�����牽�ڂ̓��̂����J�E���g�B�������Ȃ�قǋ����Ȃ�
    [SerializeField]
    private int contacing_conductor = 0;         //�ڐG���Ă��铱�̂̐�
    [SerializeField]
    private int giving_conductor = 0;            //�d�C�𕪂��^�������̂̐�

    //energization�̃Z�b�^�[�B
    public bool GetEnergization()
    {
        return energization;
    }
    public void SetEnergization(bool electoric)
    {
        energization = electoric;

    }
    public void GivePowerReSet()
    {
        energization = false;
        power_gave = false;
        giving_conductor = 0;
    }
    //�d�̗͂D��x�A�����������d���ɋ߂��̂ŗD�悷��
    //set_p�����g��power_cnt
    public void SetPower(int set_p)
    {
        //set_p�ɂ͂��̃��\�b�h���N�������I�u�W�F�N�g��power_cnt������A
        //���ꂪ���̃I�u�W�F�N�g��power_cnt��菬������Α������
        if ((set_p < power_cnt || power_cnt == 0) && energization == false)
        {
            power_cnt = set_p;
        }
    }

    //���g��power_cnt��0�ɂȂ����Ƃ��Ɏ����0�ɂ��邽�߂����̃��\�b�h
    public void SetPower(int set_p, int pow)
    {
        //��̃��\�b�h��set_p�̖�����ϐ�pow�ő�p����B
        if (power_cnt > pow)
        {
            //�ʓd��Ԃł͂Ȃ��Ȃ�̂Œʓd���Ă���؂ƂȂ�ϐ�������������
            GivePowerReSet();
            power_save = power_cnt;
            power_cnt = set_p;
        }
    }

    //�^�OPower_Supply�I�u�W�F�N�g����A�N�Z�X���邽�߂̃��\�b�h
    public void SetPower(int set_p, bool turn_on)
    {
        power_cnt = set_p;
        Power_hit = turn_on;
    }

    //�≏�̂̏����B�Z�b�g����莩���̃p���[����������ΐ≏����Ȃ�
    public void SetInsulator(bool set_insul, int pow)
    {
        if (pow < power_cnt && pow != 0) 
        {
            hitting_insulator = set_insul;
        }
    }

    //���̂Ɨ��ꂽ���̏���
    public void SetLeave(bool leave, int pow)
    {
        if (pow < power_cnt)
        {
            power_save = power_cnt;
            leaving_Conductor = leave;
            Conductor_hit = false;
            //���̃I�u�W�F�N�g��power_cnt��0�ɂȂ������Ƃɂ��
            //���̃I�u�W�F�N�g�Ɨאڂ��Ă���I�u�W�F�N�g���d�͂��������ǂ������m�F���邽�߁Atrue�ɂ���
            energi_check = true;
        }
    }

    IEnumerator EffExit()
    {
        for(int i=0;i < 2;i++)
        {
            if(i==0)
            {
                yield return 0.1f;
            }
            else if(i==1)
            {
                PowerOn(power_save);
            }
        }
        
    }

    public void PowerOn(int pow)
    {
        power_cnt = pow;
    }

    public void PowerOff()
    {
        GivePowerReSet();
        power_save = power_cnt;
        power_cnt = 0;
        StartCoroutine(EffExit());
        energization = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((hitting_insulator == true && Power_hit == false) || (Insulator_hit == true && Power_hit == false) || (leaving_Conductor == true && Power_hit == false))
        {
            GivePowerReSet();
            if (leaving_Conductor == true)
            {
                power_cnt = 0;
            }


            leaving_Conductor = false;
        }
        else if (power_cnt >= ELECTORIC_POWER && (Conductor_hit == true || Power_hit == true))
        {
            energization = true;
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

    private void OnCollisionEnter(Collision c)
    {
        //�d���ƐڐG�����Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            ////Power_hit��true�ɂ���
            //Power_hit = true;
            ////�d������L�тĂ铱�͍̂ŏ��ɐݒ肵�A�ŗD��Ƃ���
            //power_cnt = 1;
        }
        //�≏�̂ƐڐG�����Ƃ�
        else if (c.gameObject.tag == "Insulator")
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

    //���̓���̃I�u�W�F�N�g�����ꂽ���̏���
    void OnCollisionExit(Collision c)
    {

        //�d���Ɨ��ꂽ�Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            //
            energization = false;
            Power_hit = false;
        }
        //�≏�̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Insulator")
        {
            Insulator_hit = false;
            GivePowerReSet();
        }
        //�d���ƐڐG�����ɓ��̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Conductor")
        {
            //�d�C���Ă邩�̊m�F�p�ϐ���false�ɂ���
            Conductor_hit = false;
            c.gameObject.GetComponent<Conductor_Script>().SetLeave(true, power_cnt);
        }
    }


    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Conductor")
        {
            if (Conductor_hit == false)
            {
                Conductor_hit = true;
            }
            if (Insulator_hit == true || hitting_insulator == true)
            {
                c.gameObject.GetComponent<Conductor_Script>().SetInsulator(true, power_cnt);
            }
            else if (energization == true && power_gave == false)
            {
                
                //�������ʓd��Ԃɂ��鎞�A����̐ڐG���Ă��铱�̂��ʓd��Ԃɂ���
                if (giving_conductor < contacing_conductor)
                {
                    //����̓��̂�power_cnt�ɂ͎��g��power_cnt���1�������������č��ʉ���}��
                    c.gameObject.GetComponent<Conductor_Script>().SetPower(power_cnt + 1);
                    giving_conductor++;
                }
                else
                {
                    power_gave = true;
                }
                c.gameObject.GetComponent<Conductor_Script>().SetInsulator(false, power_cnt);
            }
            //���̃I�u�W�F�N�g�̃p���[��0�ɂȂ������Ƃɂ��A�ׂ̃I�u�W�F�N�g���p���[��0�ɂȂ邩�ǂ����m�F����
            if (power_cnt == 0 && energi_check == true)
            {

                //�ׂ̃I�u�W�F�N�g�̃p���[�̑傫�������̃I�u�W�F�N�g��菬������΁A���̃I�u�W�F�N�g�͐≏����Ȃ�
                c.gameObject.GetComponent<Conductor_Script>().SetPower(0, power_save);
                giving_conductor++;
                if (giving_conductor == contacing_conductor)
                {
                    energi_check = false;
                    giving_conductor = 0;
                }
            }

        }
    }
}
