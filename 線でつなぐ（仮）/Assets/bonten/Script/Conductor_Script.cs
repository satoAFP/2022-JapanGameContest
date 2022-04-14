using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    //�ʓd�ϐ���true�ɂȂ邩�ǂ����m�F����֐�
    public bool energization  = false;          //�ʓd���Ă邩
    public bool Conductor_hit = false;          //���̂ƐڐG���Ă邩
    public bool Insulator_hit = false;          //�≏�̂ƐڐG���Ă邩
    public bool Power_hit     = false;          //�d���ƐڐG���Ă邩
    public bool hitting_insulator = false;      //����Ɏ������≏�̂ƐڐG���Ă��邱�Ƃ�`���邽�߂̕ϐ�
    public bool hitting_Conductor = false;      //�ڐG���Ă���conductor�Ɨ��ꂽ���Ƃ�`���邽�߂̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitting_insulator == true && Power_hit == false) 
        {
            energization = false;
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
            //power_supply_hit��true�ɂ���
            energization = true;
        }
        //�≏�̂ƐڐG�����Ƃ�
        else if(c.gameObject.tag=="Insulator")
        {
            //Insulator_hit��true�ɂ���
            Insulator_hit = true;
        }
    }

    //���ꂽ�痣�ꂽ�I�u�W�F�N�g�̃��X�g���폜
    void OnCollisionExit(Collision c)
    {
        
        //�d���Ɨ��ꂽ�Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            //
            energization = false;
        }
        //�≏�̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Insulator")
        {
            if (Conductor_hit == true)
            {
                energization = true;
            }
            Insulator_hit = false;
        }
        //�d���ƐڐG�����ɓ��̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Conductor")
        {
            //�d�C���Ă邩�̊m�F�p�ϐ���false�ɂ���
            Conductor_hit = false;
            if(Power_hit==false)
            {
                energization = false;
            }
            c.gameObject.GetComponent<Conductor_Script>().hitting_Conductor = true;
        }
    }
    
    //�����ŐG��Ă�I�u�W�F�N�g�����X�g�ɂԂ�����
    void OnCollisionStay(Collision c)
    {
        
        //�≏�̂ƐڐG���Ă�Ƃ�
        if (c.gameObject.tag == "Insulator")
        {
            //insulator_hit��true�ɂ���
            energization = false;
        }
        else if (c.gameObject.tag == "Conductor")
        {

            if(Conductor_hit==false)
            {
                Conductor_hit = true;
            }
            if (energization == true)
            {
                //�������ʓd��Ԃɂ��鎞�A����̐ڐG���Ă��铱�̂��ʓd��Ԃɂ���
                c.gameObject.GetComponent<Conductor_Script>().energization = true;
            }


            
            if (Insulator_hit ==true)
            {
                //�ʓd��Ԃ�false�ɂ��A���g���≏�̂ƐڐG���Ă��邱�Ƃ�����̓��̂ɓ`����
                energization = false;
                c.gameObject.GetComponent<Conductor_Script>().hitting_insulator = true;
            }
            else
            {
                c.gameObject.GetComponent<Conductor_Script>().hitting_insulator = false;
            }
        }
    }
}
