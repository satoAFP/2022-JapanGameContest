using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpos : MonoBehaviour
{
    private GameObject BoxCastRayTest;

    // Start is called before the first frame update
    void Start()
    {
        BoxCastRayTest = GameObject.Find("fps_camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //���i�V�����_�[�j�ɔ���u���b�N���������Ă���Ƃ����̏�Ƀu���b�N��u���Ȃ�����
        if(other.gameObject.tag=="Conductor"|| other.gameObject.tag == "ColorOutput")
        {
            Debug.Log("�������ꂪ�o���Ă��I");
            BoxCastRayTest.GetComponent<BoxCastRayTest>().Existence_Check = true;
        }

        //�d���I�u�W�F�N�g�ɔ���u���b�N���������Ă���Ƃ����̏�Ƀu���b�N��u���Ȃ�����
        if (other.gameObject.tag == "Power_Supply")
        {
            Debug.Log("�e�B�[�_�́Z�Z�Z�C�����悷������I");
            BoxCastRayTest.GetComponent<BoxCastRayTest>().NosetLight = true;
        }
        else
        {
            Debug.Log("�q���[�Y�́Z�Z�Z�C�����悷������I");
            BoxCastRayTest.GetComponent<BoxCastRayTest>().NosetLight = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�d���I�u�W�F�N�g�ɔ���u���b�N���������Ă���Ƃ����̏�Ƀu���b�N��u���Ȃ�����
        if (other.gameObject.tag == "Power_Supply")
        {
           // Debug.Log("�q���[�Y�́Z�Z�Z�C�����悷������I");
            //BoxCastRayTest.GetComponent<BoxCastRayTest>().NosetLight = true;
        }
    }
}
