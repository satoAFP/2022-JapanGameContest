using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIniMap : MonoBehaviour
{
    public GameObject player;
    Vector3 trans, Angle;

    public float Set_x, END_z_front,END_z_back;
    
    // Start is called before the first frame update
    void Start()
    {
        //�J�����̏����ʒu�Œ�
        trans.z = END_z_front;
        trans.x = Set_x;
    }

    // Update is called once per frame
    void Update()
    {
       // Angle = player.transform.localEulerAngles;


        //��O�ɉ�����Ȃ�
        if (END_z_front > player.transform.position.z)
            ;
        //���ɍs���Ȃ�
        else if (END_z_back < player.transform.position.z)
            ;
        //��L�ȊO
        else
            trans.z = player.transform.position.z;


        //Angle.x = 90.0f;

        //���፷�Ή��p
        trans.y = player.transform.position.y+12.0f;
        

        //this.transform.rotation = Quaternion.Euler(Angle);
        this.transform.position = trans;
    }
}
