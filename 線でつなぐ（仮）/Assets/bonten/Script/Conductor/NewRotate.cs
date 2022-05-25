using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRotate : Base_Enegization
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



    private const int RIGHT = 0;         //���̃I�u�W�F�N�g
    private const int LEFT = 1;     //����ȊO�̃I�u�W�F�N�g

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    GameObject[] AssistObj = new GameObject[2];


    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    private bool[] hit_check = new bool[2];         //�A�V�X�gOBJ(���[�̋�)���������Ă邩�ǂ����`�F�b�N����悤

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


    //�≏�̂̏����B�Z�b�g����莩���̃p���[����������ΐ≏����Ȃ�
    public void SetInsulator(bool set_insul)
    {
        hitting_insulator = set_insul;
    }

    //���̂Ɨ��ꂽ���̏���
    public void SetLeave(bool leave, int pow)
    {
        if (pow > power_cnt)
        {
            if (this.gameObject.name == "Cube")
            {
                /*�o�O����
                 �d������Ȃ����Ă�R���_�N�^�[(�ȉ��V�����_�[1)����ɂ��̏�����ʂ��Cube�R���_�N�^�[�̃p���[��0�ɂȂ�
                 ����ɂ���āACube�R���_�N�^�[����Ȃ����Ă�R���_�N�^�[(�ȉ��V�����_�[3)��Exit�����ɓ���A����SetLeave
                 �ɓ����Ă�Cube�p���[��0(����pow�̕�)�ɂȂ��Ă�̂ł���if���ɓ���Ȃ�

                �t���[�`���[�g�I�ɕ`����
                Cube�������
                ��
                ��ɃV�����_�[1��Exit��Cube�ɑ΂��Ĕ������ACube��Power_cnt��0�ɂȂ�
                ��
                ���̌�ACube��Exit���V�����_�[3�ɑ΂��Ĕ������A�V�����_�[3�̓d�C���������Ƃ���(SetLeave�ɓ���)��
                ������Cube��Power_cnt��0�ɂȂ��Ă���̂�if���̒��ɓ���Ȃ�
                ��
                ���̌��ʁA�d���ƂȂ����Ă铱�̂�����Ă��d�C����������

                ��肽������
                Cube�������
                ��
                ���Cube��Exit���V�����_�[1�A����уV�����_�[3�ɔ���������
                ��

                 */
                Debug.Log(this.gameObject.name);
                Debug.Log(power_cnt);
                Debug.Log(pow);
            }

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


    public void SetCheckRight(bool success)
    {
        hit_check[RIGHT] = success;
    }
    public void SetCheckLeft(bool success)
    {
        hit_check[LEFT] = success;
    }

    // Update is called once per frame
    public void Update()
    {
        if (energization && efflight != null)
        {

            //�I�u�W�F�N�g�̐F��\��
            efflight.SetActive(true);
        }
        else 
        {
            power_cnt = 0;
            if (efflight != null)
            {
                //�I�u�W�F�N�g�̐F���\��
                efflight.SetActive(false);
            }
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        //�d���ƐڐG�����Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            ////Power_hit��true�ɂ���
            energization = true;
            ////�d������L�тĂ铱�͍̂ŏ��ɐݒ肵�A�ŗD��Ƃ���
            //power_cnt = 1;
        }
        //�≏�̂ƐڐG�����Ƃ�
        else if (c.gameObject.tag == "Insulator")
        {
            //Insulator_hit��true�ɂ���
            Insulator_hit = true;
        }
    }

    //���̓���̃I�u�W�F�N�g�����ꂽ���̏���
    public void OnCollisionExit(Collision c)
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
        }
        //���̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Conductor")
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
        if (c.gameObject.tag == "Conductor")
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
                        Debug.Log(this.gameObject.transform.parent.name);
                        energization = true;
                        power_cnt = c.gameObject.GetComponent<NewConductor>().GetPower() - 1;
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
