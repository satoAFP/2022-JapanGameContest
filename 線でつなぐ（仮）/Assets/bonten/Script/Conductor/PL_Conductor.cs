using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�����삷�铱�̂̃X�N���v�g
public class PL_Conductor : Conductor_Script
{
    private void OnCollisionExit(Collision c)
    {
        //�d���ƐڐG�����ɓ��̂Ɨ��ꂽ�Ƃ�
        if (c.gameObject.tag == "Conductor")
        {
            contacing_conductor--;

            //�d�C���Ă邩�̊m�F�p�ϐ���false�ɂ���
            Conductor_hit = false;
            c.gameObject.GetComponent<Conductor_Script>().SetLeave(true, power_cnt);

            PowerOff();
        }
    }
}
