using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    //�ʓd�ϐ���true�ɂȂ邩�ǂ����m�F����֐�
    protected void Confimation_Energi()
    {
        if(insulator_hit==false)
        {
            //�d���ƐڐG���Ă�Ƃ�
            if (power_supply_hit == true)
            {
                //�d�C���ʂ�
                energization = true;
            }
            //���̂ƐڐG���Ă�Ƃ�
            else if (energi_investigate == true)
            {
                //�d�C���ʂ�
                energization = true;
            }
            else
            {
                energization = false;
            }
        }
        else
        {
            energization = false;
        }
    }

    public bool counductor_hit = false;        //���̂Ɠ���������
    public bool insulator_hit = false;         //�≏�̂Ɠ���������
    public bool power_supply_hit = false;      //�d���Ɠ���������
    public bool energization = false;          //�ʓd���Ă邩

    private bool energi_investigate = false;

    public void Energi_On(bool energi)
    {
        energization = energi;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Confimation_Energi();


        if (energi_investigate == false)
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

    //�����ŐG��Ă�I�u�W�F�N�g�����X�g�ɂԂ�����
    void OnCollisionStay(Collision c)
    {
        //if(/*�d��������*/true)
        //{

        //}

        //�d���ƐڐG���Ă�Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            //power_supply_hit��true�ɂ���
            power_supply_hit = true;
            energi_investigate = true;
        }
        //�≏�̂ƐڐG���Ă�Ƃ�
        else if (c.gameObject.tag == "Insulator")
        {
            //insulator_hit��true�ɂ���
            insulator_hit = true;
            energi_investigate = false;
        }
        //���̂ƐڐG���Ă�Ƃ�!
        else if (c.gameObject.tag == "Conductor")
        {
            //�Ȃ����Ă铱�̂�energization(�ʓd�m�F�p�ϐ�)�ŏ�����
            energi_investigate = c.gameObject.GetComponent<Conductor_Script>().energization;
            //�d�C�����ĂȂ��Ƃ�
            //if (energization==false)
            //{
            
                
            //}
                
        }
    }

    //���ꂽ�痣�ꂽ�I�u�W�F�N�g�̃��X�g���폜
    void OnCollisionExit(Collision c)
    {
        //�d���Ɨ��ꂽ�Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            //power_supply_hit��true�ɂ���
            power_supply_hit = false;
        }
        //�d���Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Insulator")
        {
            //insulator_hit��false�ɂ���
            insulator_hit = false;
        }
        //�d���ƐڐG�����ɓ��̂Ɨ��ꂽ�Ƃ�
        else if (c.gameObject.tag == "Conductor")
        {
            //�d�C���Ă邩�̊m�F�p�ϐ���false�ɂ���
            energi_investigate = false;
        }
    }
}
