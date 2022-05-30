using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewConductor : Base_Enegization
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
    [SerializeField]
    private bool Pl_control;

    public void GivePowerReSet()
    {
        energization = false;
        power_gave = false;
        giving_conductor = 0;
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

    //�d�͂��i�[
    public void PowerOn(int a)
    {
        power_cnt = a;
    }

    public void PowerOff()
    {
        GivePowerReSet();
        power_save = power_cnt;

        power_cnt = 0;
        energi_check = true;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        //�d��?�G�l���M�[���C���I�Ȃ���A�N�e�B�u�A��A�N�e�B�u�ɐݒ肷��
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
                power_save = 0;
            }
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        //�d���ƐڐG�����Ƃ�
        if (c.gameObject.CompareTag("Power_Supply"))
        {
            //�d�C��ʂ�
            energization = true;
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
            //Power_hit��false�ɂ��Aenergization��false�ɂ���
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
        else if(c.gameObject.CompareTag("Rotate"))
        {
            Debug.Log(this.gameObject.transform.parent.name + "�F" + c.gameObject.GetComponent<NewRotate>().GetPower());
            if (power_cnt < c.gameObject.GetComponent<NewRotate>().GetPower())
            {
                energization = false;
                power_save = power_cnt;
            }
        }
    }


    public void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Conductor")
        {
            //�≏�̂ƐڐG���Ă��鎞�A
            if ((Insulator_hit || hitting_insulator) && (power_cnt != 0 && power_cnt > c.gameObject.GetComponent<NewConductor>().GetPower()) && energization) 
            {
                c.gameObject.GetComponent<NewConductor>().SetInsulator(true);
                energization = false;
                power_save = power_cnt;
            }
            else
            {

                //���g���ʓd���ĂȂ��āA�ڐG���Ă�ConductorObj���ʓd���Ă鎞�A���g���ʓd����
                //�ڐG���Ă�ConductorObj�̓d�͂�1�ቺ���������l���擾
                if ((!energization && c.gameObject.GetComponent<NewConductor>().GetEnergization()) && power_cnt < c.gameObject.GetComponent<NewConductor>().GetPower()) 
                {
                    energization = true;
                    power_cnt = c.gameObject.GetComponent<NewConductor>().GetPower() - 1;
                    c.gameObject.GetComponent<NewConductor>().SetInsulator(false);
                }
                //���g���ʓd���Ă���A�ڐG���Ă�ConductorObj���ʓd���ĂȂ������Ƃ��A���g��������
                //�d�͂�0�ɂ���
                else if (energization && !c.gameObject.GetComponent<NewConductor>().GetEnergization() && power_cnt < c.gameObject.GetComponent<NewConductor>().GetSavePower())
                {
                    //Debug.LogError(this.gameObject.transform.parent.name);
                    energization = false;
                    power_save = power_cnt;
                }
            }
        }
        else if (c.gameObject.tag == "Rotate")
        {
            //�≏�̂ƐڐG���Ă��鎞�A
            if ((Insulator_hit || hitting_insulator) && (power_cnt != 0 && power_cnt > c.gameObject.GetComponent<NewRotate>().GetPower()) && energization)
            {
                c.gameObject.GetComponent<NewRotate>().SetInsulator(true);
                energization = false;
                power_save = power_cnt;
            }
            else
            {
                //���g���ʓd���ĂȂ��āA�ڐG���Ă�ConductorObj���ʓd���Ă鎞�A���g���ʓd����
                //�ڐG���Ă�ConductorObj�̓d�͂�1�ቺ���������l���擾
                if ((!energization && c.gameObject.GetComponent<NewRotate>().GetEnergization()) && power_cnt < c.gameObject.GetComponent<NewRotate>().GetPower())
                {
                    energization = true;
                    power_cnt = c.gameObject.GetComponent<NewRotate>().GetPower() - 1;
                    c.gameObject.GetComponent<NewRotate>().SetInsulator(false);
                }
                //���g���ʓd���Ă���A�ڐG���Ă�ConductorObj���ʓd���ĂȂ������Ƃ��A���g��������
                //�d�͂�0�ɂ���
                else if (energization && !c.gameObject.GetComponent<NewRotate>().GetEnergization() && power_cnt < c.gameObject.GetComponent<NewRotate>().GetSavePower())
                {
                    energization = false;
                    power_save = power_cnt;
                }
            }
        }
    }
}
