using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : Base_Enegization
{
    protected const int ELECTORIC_POWER = 1;   //1�ȏ�Œʓd

    [SerializeField]
    protected bool Conductor_hit = false;          //���̂ƐڐG���Ă邩
    [SerializeField]
    protected bool Insulator_hit = false;          //�≏�̂ƐڐG���Ă邩
    [SerializeField]
    protected bool Power_hit = false;          //�d���ƐڐG���Ă邩
    [SerializeField]
    protected bool hitting_insulator = false;      //����Ɏ������≏�̂ƐڐG���Ă��邱�Ƃ�`���邽�߂̕ϐ�
    [SerializeField]
    protected bool leaving_Conductor = false;      //�ڐG���Ă���conductor�Ɨ��ꂽ���Ƃ�`���邽�߂̕ϐ�
    [SerializeField]
    protected bool energi_check = false;         //���g���ʓd�������Ƃ��`�F�b�N���߂̕ϐ�
    [SerializeField]
    protected bool power_gave = false;             //�������ڐG���Ă铱�̂��ׂĂɓd�C��ʂ������m�F���邽�߂̕ϐ�
    [SerializeField]
    protected int power_save = 0;                  //�p���[��0�ɂȂ��Ă����̃I�u�W�F�N�g�̎����Ă��p���[�����Ƃ��Ƃǂꂮ�炢����������ۑ����Ă������߂̕ϐ�
    [SerializeField]
    protected int power_cnt = 0;                   //�d�����牽�ڂ̓��̂����J�E���g�B�������Ȃ�قǋ����Ȃ�
    [SerializeField]
    protected int contacing_conductor = 0;         //�ڐG���Ă��铱�̂̐�
    [SerializeField]
    protected int giving_conductor = 0;            //�d�C�𕪂��^�������̂̐�
    [SerializeField]
    private bool rotate_hit = false;

    [SerializeField, Header("�d���F")]
    private GameObject eneger_line;
    public void AlreadyGetEnegy()
    {
        giving_conductor++;
    }
    public void GivePowerReSet()
    {
        energization = false;
        power_gave = false;
        giving_conductor = 0;
    }

    //�d�͂̃Q�b�^�[
    public int GetPower() => power_cnt;
    
    //�d�̗͂D��x�A�����������d���ɋ߂��̂ŗD�悷��
    //set_p�����g��power_cnt
    public void SetPower(int set_p)
    {
        //set_p�ɂ͂��̃��\�b�h���N�������I�u�W�F�N�g��power_cnt������A
        //���ꂪ���̃I�u�W�F�N�g��power_cnt��菬������Α������
        if ((set_p > power_cnt || power_cnt == 0) && energization == false)
        {
            power_cnt = set_p;
        }
    }
    public void SetPower(int set_p,GameObject Obj)
    {
        //set_p�ɂ͂��̃��\�b�h���N�������I�u�W�F�N�g��power_cnt������A
        //���ꂪ���̃I�u�W�F�N�g��power_cnt��菬������Α������
        if ((set_p > power_cnt || power_cnt == 0) && energization == false)
        {
            power_cnt = set_p;
            Obj.gameObject.GetComponent<Conductor_Script>().AlreadyGetEnegy();
        }
    }

    //���g��power_cnt��0�ɂȂ����Ƃ��Ɏ����0�ɂ��邽�߂����̃��\�b�h
    public void SetPower(int set_p, int pow)
    {
        //��̃��\�b�h��set_p�̖�����ϐ�pow�ő�p����B
        if (power_save < pow)
        {
            //�d���ƐڐG���Ă��铱�̂������O�Ƃ���
            if(Power_hit!=true)
            {
                //�ʓd��Ԃł͂Ȃ��Ȃ�̂Œʓd���Ă���؂ƂȂ�ϐ�������������
                energi_check = true;
                GivePowerReSet();
                power_save = power_cnt;
                power_cnt = set_p;
            }
        }
    }

    //�^�OPower_Supply�I�u�W�F�N�g����A�N�Z�X���邽�߂̃��\�b�h
    public void SetPower(int set_p, bool turn_on)
    {
        power_cnt = set_p;
        Power_hit = turn_on;
    }

    /// <summary>
    /// �≏�̂̏����B�Z�b�g���̂�莩���̃p���[����������ΐ≏����Ȃ�
    /// </summary>
    /// <param name="set_insul">�Z�b�g�����≏�̂ƐڐG���Ă��邩�̐���</param>
    /// <param name="pow">�Z�b�g���̃p���[�̒l</param>
    public void SetInsulator(bool set_insul, int pow)
    {
        if (pow > power_cnt && pow != 0) 
        {
            hitting_insulator = set_insul;
        }
    }

    /// <summary>
    /// �≏�̂ƐڐG���Ă��邩�ǂ����B
    /// </summary>
    /// <returns></returns>
    public bool GetInsulator()
    {
        if(Insulator_hit||hitting_insulator)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //���̂Ɨ��ꂽ���̏���
    public void SetLeave(bool leave, int pow)
    {
        if (pow > power_cnt)
        {
            power_save = power_cnt;
            leaving_Conductor = leave;
            Conductor_hit = false;
            //���̃I�u�W�F�N�g��power_cnt��0�ɂȂ������Ƃɂ��
            //���̃I�u�W�F�N�g�Ɨאڂ��Ă���I�u�W�F�N�g���d�͂��������ǂ������m�F���邽�߁Atrue�ɂ���
            energi_check = true;
        }
    }

    public void EffExit()
    {
        PowerOn(power_save);
    }

    public void PowerOn(int a)
    {
        power_cnt = a;
        Debug.Log(a);
    }

    public void PowerOff()
    {
        GivePowerReSet();
        power_save = power_cnt;
        
        power_cnt = 0;
        energi_check = true;
    }

    // Update is called once per frame
    public void Update()
    {
        //�d�C���Ւf���鏈���B�≏�̂ƐڐG�A�����̃I�u�W�F�N�g���p���[�J�E���g���傫���I�u�W�F�N�g���≏�̂ƐڐG���Ă���Ɠd�C�Ւf
        if (((hitting_insulator == true || Insulator_hit == true || leaving_Conductor == true || contacing_conductor == 0) && Power_hit == false))
        {
            GivePowerReSet();
            if (leaving_Conductor == true)
            {
                power_cnt = 0;
                leaving_Conductor = false;
            }
        }
        else if (power_cnt >= ELECTORIC_POWER && (Conductor_hit == true || Power_hit == true))
        {
            energization = true;
        }


        if (energization == true)
        {
            //�I�u�W�F�N�g�̐F��\��
            eneger_line.SetActive(true);
            
                
        }
        else if (energization == false)
        {
            //�I�u�W�F�N�g�̐F���\��
            eneger_line.SetActive(false);

        }


    }

    public void OnCollisionEnter(Collision c)
    {
        //�≏�̂ƐڐG�����Ƃ�
        if (c.gameObject.tag == "Insulator")
        {
            //Insulator_hit��true�ɂ���
            Insulator_hit = true;
        }
        else if (c.gameObject.tag == "Conductor" || c.gameObject.tag == "Rotate")
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
    public void OnCollisionExit(Collision c)
    {
        //�d���Ɨ��ꂽ�Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            energization = false;
            Power_hit = false;
        }
        //�≏�̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Insulator")
        {
            Insulator_hit = false;
            GivePowerReSet();
        }
        //���̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Conductor" || c.gameObject.tag == "Rotate")
        {
            //���̂ƐڐG���Ă鑍����1�ւ炷
            contacing_conductor--;

            //�d�C���Ă邩�̊m�F�p�ϐ���false�ɂ���
            Conductor_hit = false;
        }
    }


    public void OnCollisionStay(Collision c)
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
                    //����̓��̂�power_cnt�ɂ͎��g��power_cnt���1���Ȃ����������č��ʉ���}��
                    c.gameObject.GetComponent<Conductor_Script>().SetPower(power_cnt - 1);
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
                //�ׂ̃I�u�W�F�N�g�̃p���[�̑傫�������̃I�u�W�F�N�g���傫����΁A���̃I�u�W�F�N�g�͐≏����Ȃ�
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
