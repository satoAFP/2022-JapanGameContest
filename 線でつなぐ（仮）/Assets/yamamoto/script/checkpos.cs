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
        if(other.gameObject.tag=="Conductor"|| other.gameObject.tag == "ColorOutput"|| other.gameObject.tag == "Rotate")
        {
            Debug.Log("�������ꂪ�o���Ă��I");
            BoxCastRayTest.GetComponent<BoxCastRayTest>().Existence_Check = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //�d���I�u�W�F�N�g�ɔ���u���b�N���������Ă���Ƃ����̏�Ƀu���b�N��u���Ȃ�����
        //tag[Noset]�@���̏I�_�ɒu���Ă郉�C�g�I�u�W�F�N�g
        //if (other.gameObject.tag == "Power_Supply" || other.gameObject.tag == "Noset")
        //{
        //    Debug.Log("�u����");
        //    BoxCastRayTest.GetComponent<BoxCastRayTest>().NosetLight = true;
        //}
        //else
        //{
        //    Debug.Log("�Ȃ��u����");
        //    BoxCastRayTest.GetComponent<BoxCastRayTest>().NosetLight = false;
        //}
    }
}
