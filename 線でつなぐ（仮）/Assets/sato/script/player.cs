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

    //�v���C�x�[�g�ϐ�
    private Vector3 velocity;               //���W�b�g�{�f�B�̗�
    private Rigidbody rb;                   //���W�b�h�{�f�B���擾���邽�߂̕ϐ�
    private bool isGround = true;           //���n���Ă��邩�ǂ����̔���
    private float mem_camera_rotato_y = 0;  //�J������Y����]�L��
    private Transform _camTransform;        //camera��transform
    private Vector3 _startMousePos;         //�}�E�X����̎n�_
    private Vector3 _presentCamRotation;    //�J������]�̎n�_���

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //���W�b�h�{�f�B���擾

        //�J�����֌W������
        _camTransform = this.gameObject.transform;
        _startMousePos = Input.mousePosition;
        _presentCamRotation.x = _camTransform.transform.eulerAngles.x;
        _presentCamRotation.y = _camTransform.transform.eulerAngles.y;
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
                Debug.Log("a");
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
        float x = (_startMousePos.x + Input.mousePosition.x) / Screen.width;
        float y = mem_camera_rotato_y;

        //Y���̉�]�͈��l�Ŏ~�܂�
        if (((_startMousePos.y - Input.mousePosition.y) / Screen.height) <= mouse_max_y &&
            ((_startMousePos.y - Input.mousePosition.y) / Screen.height) >= -mouse_max_y)
        {
            y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;
            mem_camera_rotato_y = y;
        }

        //��]�J�n�p�x �{ �}�E�X�̕ω��� * 90
        float eulerX = _presentCamRotation.x + y * mouse_power;
        float eulerY = _presentCamRotation.y + x * mouse_power;

        _camTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
    }
}
