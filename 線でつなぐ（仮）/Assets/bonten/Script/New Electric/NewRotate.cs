using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRotate : Base_RotateAssist
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
    private GameObject efflight;




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
    public void SetPower(int set_p, GameObject Obj)
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
            if (Power_hit != true)
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

    //power_cnt�̃Q�b�^�[
    public int GetPower() => power_cnt;


    //save_cnt�̃Q�b�^�[
    public int GetSavePower() => power_save;

    //�≏�̂̏����B�Z�b�g����莩���̃p���[����������ΐ≏����Ȃ�
    public void SetInsulator(bool set_insul)
    {
        hitting_insulator = set_insul;
    }

    // Update is called once per frame
    public void Update()
    {
        if (energization)
        {
            if(efflight != null)
            {
                //�I�u�W�F�N�g�̐F��\��
                efflight.SetActive(true);
            }
        }
        else 
        {
            if (efflight != null)
            {
                //�I�u�W�F�N�g�̐F���\��
                efflight.SetActive(false);
            }
            else
            {
                power_cnt = 0;
            }
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        //�d���ƐڐG�����Ƃ�
        if (c.gameObject.CompareTag("Power_Supply"))
        {
            ////Power_hit��true�ɂ���
            energization = true;
            ////�d������L�тĂ铱�͍̂ŏ��ɐݒ肵�A�ŗD��Ƃ���
            //power_cnt = 1;
        }
        //�≏�̂ƐڐG�����Ƃ�
        else if (c.gameObject.CompareTag("Insulator"))
        {
            //Insulator_hit��true�ɂ���
            Insulator_hit = true;
        }
    }

    //���̓���̃I�u�W�F�N�g�����ꂽ���̏���
    public void OnCollisionExit(Collision c)
    {

        //�d���Ɨ��ꂽ�Ƃ�
        if (c.gameObject.CompareTag("Power_Supply"))
        {
            //
            energization = false;
            Power_hit = false;
        }
        //�≏�̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.CompareTag("Insulator"))
        {
            Insulator_hit = false;
        }
        //���̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.CompareTag("Conductor"))
        {
            if (power_cnt < c.gameObject.GetComponent<NewConductor>().GetPower())
            {
                energization = false;
                power_save = power_cnt;
            }
        }
    }


    public void OnCollisionStay(Collision c)
    {
        if (c.gameObject.CompareTag("Conductor"))
        {

            if (hit_check[RIGHT] == true && hit_check[LEFT] == true)
            {
                //�≏�̂ƐڐG���Ă��鎞�A
                if ((Insulator_hit || hitting_insulator) && (power_cnt != 0 && power_cnt > c.gameObject.GetComponent<NewConductor>().GetPower()) && energization)
                {
                    c.gameObject.GetComponent<NewConductor>().SetInsulator(true);
                    energization = false;
                    power_save = power_cnt;
                    power_cnt = 0;
                }
                else
                {

                    //���g���ʓd���ĂȂ��āA�ڐG���Ă�ConductorObj���ʓd���Ă鎞�A���g���ʓd����
                    //�ڐG���Ă�ConductorObj�̓d�͂�1�ቺ���������l���擾
                    if ((!energization && c.gameObject.GetComponent<NewConductor>().GetEnergization()) && power_cnt < c.gameObject.GetComponent<NewConductor>().GetPower())
                    {
                        energization = true;
                        power_cnt = c.gameObject.GetComponent<NewConductor>().GetPower() - 1;
                        Debug.Log(power_cnt);
                        c.gameObject.GetComponent<NewConductor>().SetInsulator(false);
                    }
                    //���g���ʓd���Ă���A�ڐG���Ă�ConductorObj���ʓd���ĂȂ������Ƃ��A���g��������
                    //�d�͂�0�ɂ���
                    else if (energization && !c.gameObject.GetComponent<NewConductor>().GetEnergization() && power_cnt < c.gameObject.GetComponent<NewConductor>().GetPower())
                    {
                        energization = false;
                        power_save = power_cnt;

                    }
                }
            }
            else
            {
                energization = false;
            }
                
        }
    }
}
