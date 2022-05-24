using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�����삷�铱�̂̃X�N���v�g
public class PL_Conductor : Conductor_Script
{
    public new void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "Insulator")
        {
            //Insulator_hit��true�ɂ���
            Insulator_hit = true;
        }
        else if (c.gameObject.tag == "Conductor")
        {
            //���̂ɐG�ꂽ��A�����_�łǂꂾ���̓��̂ƐڐG���Ă��邩�J�E���g����
            contacing_conductor++;

            //���łɓd�C���ʂ��Ă铱�̂ɐG��������ɓd�C��n�����񐔂𐔂���ϐ���+1����
            if (c.gameObject.GetComponent<Conductor_Script>().GetEnergization() == true)
            {
                //giving_conductor++;
            }

            //�V�������̂ɐG�ꂽ��Agiving_conductor,power_gave,energization�������񃊃Z�b�g����
            if (energization == true)
            {
                GivePowerReSet();
            }
        }
        
    }
    public new void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Insulator")
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
            power_cnt = 0;
            contacing_conductor--;
        }
    }
}
