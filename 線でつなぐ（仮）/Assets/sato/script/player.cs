using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //�C���X�y�N�^�[�ݒ�
    [SerializeField, Header("��l���̈ړ���"), Range(0, 10)]     float move_power;
    [SerializeField, Header("�}�E�X���x"), Range(100, 300)]      float mouse_power;
    [SerializeField, Header("�W�����v��"), Range(0, 10)]         float jump_power;
    [SerializeField, Header("�}�E�X�㉺�̌��E"), Range(0, 0.5f)] float mouse_max_y;

    [SerializeField, Header("��l���̃J����")] GameObject my_camera;

    //�v���C�x�[�g�ϐ�
    private Vector3 velocity;               //���W�b�g�{�f�B�̗�
    private Rigidbody rb;                   //���W�b�h�{�f�B���擾���邽�߂̕ϐ�
    private bool isGround = true;           //���n���Ă��邩�ǂ����̔���
    private float mem_camera_rotato_y = 0;  //�J������Y����]�L��
    private Transform camTransform;         //camera��transform
    private Vector3 startMousePos;          //�}�E�X����̎n�_
    private Vector3 presentCamRotation;     //�J������]�̎n�_���

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //���W�b�h�{�f�B���擾

        //�J�����֌W������
        camTransform = this.gameObject.transform;
        startMousePos = Input.mousePosition;
        presentCamRotation.x = camTransform.transform.eulerAngles.x;
        presentCamRotation.y = camTransform.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        //�J�����̉�] �}�E�X
        CameraRotationMouseControl();

        //���E�㉺�̈ړ�����
        if (Input.GetKey(KeyCode.W))
        {
            velocity = gameObject.transform.rotation * new Vector3(0, 0, move_power);
            Move(velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity = gameObject.transform.rotation * new Vector3(-move_power, 0, 0);
            Move(velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity = gameObject.transform.rotation * new Vector3(0, 0, -move_power);
            Move(velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity = gameObject.transform.rotation * new Vector3(move_power, 0, 0);
            Move(velocity * Time.deltaTime);
        }

        //�n�ʂ̒��n���Ă��邩�ǂ�������
        //���n���Ă���Ƃ�
        if (isGround == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGround = false;//  isGround��false�ɂ���
                rb.AddForce(new Vector3(0, jump_power*100, 0)); //��Ɍ������ė͂�������
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //Ground�^�O�̃I�u�W�F�N�g�ɐG�ꂽ�Ƃ�
        if (other.gameObject.tag == "Ground") 
        {
            //isGround��true�ɂ���
            isGround = true; 
        }
    }

    //�ړ������֐�
    private void Move(Vector3 vec)
    {
        this.gameObject.transform.position += vec;
    }

    //�J�����R���g���[���֐�
    private void CameraRotationMouseControl()
    {
        //(�ړ��J�n���W - �}�E�X�̌��ݍ��W) / �𑜓x �Ő��K��
        float x = (startMousePos.x + Input.mousePosition.x) / Screen.width;
        float y = mem_camera_rotato_y;

        //Y���̉�]�͈��l(mouse_max_y)�Ŏ~�܂�
        if (((startMousePos.y - Input.mousePosition.y) / Screen.height) <= mouse_max_y &&
            ((startMousePos.y - Input.mousePosition.y) / Screen.height) >= -mouse_max_y)
        {
            y = (startMousePos.y - Input.mousePosition.y) / Screen.height;
            mem_camera_rotato_y = y;
        }

        //��]�J�n�p�x �{ �}�E�X�̕ω��� * 90
        float eulerX = presentCamRotation.x + y * mouse_power;
        float eulerY = presentCamRotation.y + x * mouse_power;

        //��l���ƃJ�����ɂ��ꂼ��A��]�ʑ��
        camTransform.rotation = Quaternion.Euler(0, eulerY, 0);
        my_camera.transform.rotation= Quaternion.Euler(eulerX, eulerY, 0);
    }
}
