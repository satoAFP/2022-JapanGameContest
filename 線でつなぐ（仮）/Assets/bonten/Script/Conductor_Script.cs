using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    protected static int electoric_power = 1;   //1�ȏ�Œʓd
    protected enum Contact
    {
        CONTACT,
        GIVING_POWER,

        MAX_SIZE
    }
    protected void Give_Power_ReSet()
    {
        energization = false;
        power_gave = false;
        giving_conductor = 0;
    }

    //�ʓd�ϐ���true�ɂȂ邩�ǂ����m�F����֐�
    public bool energization  = false;          //�ʓd���Ă邩
    public bool Conductor_hit = false;          //���̂ƐڐG���Ă邩
    public bool Insulator_hit = false;          //�≏�̂ƐڐG���Ă邩
    public bool Power_hit     = false;          //�d���ƐڐG���Ă邩
    public bool hitting_insulator = false;      //����Ɏ������≏�̂ƐڐG���Ă��邱�Ƃ�`���邽�߂̕ϐ�
    public bool leaving_Conductor = false;      //�ڐG���Ă���conductor�Ɨ��ꂽ���Ƃ�`���邽�߂̕ϐ�
   // public bool turn_of_energi = false;         //���g���ʓd�������Ƃ�`���邽�߂̕ϐ�
    public bool power_gave = false;
    public int power_cnt = 0;                   //�d�����牽�ڂ̓��̂����J�E���g�B�������Ȃ�قǋ����Ȃ�
    public int leaving_power = 0;               //���̂����ꂽ���ɗ��ꂽ���̂̒l���󂯎��ϐ�
    public int contacing_conductor = 0;         //�ڐG���Ă��铱�̂̐�
    public int giving_conductor = 0;            //�d�C�𕪂��^�������̂̐�

    //�d�̗͂D��x�A�����������d���ɋ߂��̂ŗD�悷��
    public void Set_Power(int set_p)
    {
        if ((set_p < power_cnt || power_cnt == 0) && energization == false) 
        {
            Debug.Log(set_p);
            power_cnt = set_p;
        }
    }

    //�≏�̂̏����B�Z�b�g����莩���̃p���[����������ΐ≏����Ȃ�
    public void Set_insulator(bool set_insul,int pow)
    {
        if (pow < power_cnt)
        {
            Debug.Log(set_insul);
            hitting_insulator = set_insul;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((hitting_insulator == true && Power_hit == false) || (Insulator_hit == true && Power_hit == false))
        {
            Give_Power_ReSet();
        }
        else if (power_cnt >= electoric_power && (Conductor_hit == true || Power_hit == true)) 
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
            //Power_hit��true�ɂ���
            Power_hit = true;
            //�d������L�тĂ铱�͍̂ŏ��ɐݒ肵�A�ŗD��Ƃ���
            power_cnt = 1;
        }
        //�≏�̂ƐڐG�����Ƃ�
        else if(c.gameObject.tag=="Insulator")
        {
            //Insulator_hit��true�ɂ���
            Insulator_hit = true;
        }
        else if (c.gameObject.tag == "Conductor")
        {
            contacing_conductor++;
            if(energization==true)
            {
                Give_Power_ReSet();
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
            Give_Power_ReSet();
        }
        //�d���ƐڐG�����ɓ��̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Conductor")
        {
            //�d�C���Ă邩�̊m�F�p�ϐ���false�ɂ���
            Conductor_hit = false;
            if(Power_hit==false)
            {
                c.gameObject.GetComponent<Conductor_Script>().leaving_power = power_cnt;
                power_cnt = 0;
            }
            c.gameObject.GetComponent<Conductor_Script>().leaving_Conductor = true;
        }
    }
    
    //�����ŐG��Ă�I�u�W�F�N�g�����X�g�ɂԂ�����
    void OnCollisionStay(Collision c)
    {
        
        if (c.gameObject.tag == "Conductor")
        {

            if(Conductor_hit==false)
            {
                Conductor_hit = true;
            }
            if (Insulator_hit == true || hitting_insulator == true)
            {
                c.gameObject.GetComponent<Conductor_Script>().Set_insulator(true, power_cnt);
            }
            else if (energization == true && power_gave == false) 
            {
                //�������ʓd��Ԃɂ��鎞�A����̐ڐG���Ă��铱�̂��ʓd��Ԃɂ���
                if(giving_conductor < contacing_conductor)
                {
                    c.gameObject.GetComponent<Conductor_Script>().Set_Power(power_cnt + 1);
                    giving_conductor++;
                    
                }
                else
                {
                    power_gave = true;
                }
                c.gameObject.GetComponent<Conductor_Script>().Set_insulator(false, power_cnt);
            }
            
        }
    }
}
