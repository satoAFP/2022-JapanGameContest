using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allmap_moveCamera : MonoBehaviour
{

    //�܂����̌���ꂽ�Ƃ���ɂ��邾���̑��ݏڍׂ�Allmap_move���m�F

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���ݒn�Ԃ�
    public Vector3 Getpos()
    {
        //��(���������ŏ���)
        //trans.x += a.x + this.transform.position.x;

        //�c
       // trans.z = a.y + this.transform.position.z;

        return this.transform.position;
    }

    //���iX�j�̏ꏊ�ݒ�
    public void Setpos_x(float a)
    {

        Vector3 trans = this.transform.position;
        trans.x = a;
        this.transform.position = trans;
    }

    //��\�c�iZ�j�̏ꏊ�ݒ�
    public void Setpos_z(float a)
    {
        Vector3 trans = this.transform.position;
        trans.z = a;
        this.transform.position = trans;
    }
}
