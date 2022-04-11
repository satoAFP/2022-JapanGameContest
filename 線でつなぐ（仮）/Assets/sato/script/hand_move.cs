using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_move : MonoBehaviour
{
    [SerializeField, Header("��̓������x"), Range(1, 10)] int ani_speed;
    [SerializeField, Header("��̏㉺�̑��x"), Range( 0.001f, 0.01f)] float updown_speed;
    [SerializeField, Header("true = �E��@false = ����")] bool hand_check;

    private Vector3[] ani_pos_right = new Vector3[10];      //�E��̃A�j���[�V����
    private Vector3[] ani_pos_left = new Vector3[10];       //����̃A�j���[�V����
    private int[] ani_count = new int[2];                   //���݂̃A�j���[�V�����ԍ�
    private bool[] ani_check = new bool[2];                 //�Đ������t�Đ�����
    private int frame = 0;                                  //�J�n������̃t���[��
    private Vector3 now_pos;                                //���݂̍��W
    private float move_amount_y = 0.2f;                     //�s�����N�����Ǝ肪�o�Ă���ړ���

    // Start is called before the first frame update
    void Start()
    {
        now_pos = this.gameObject.transform.localPosition;

        //��̃A�j���[�V�������W����
        for (int i=0;i<10;i++)
        {
            ani_pos_right[i].x = 0.5f + (0.01f * i);
            ani_pos_right[i].y = 0.7f - (0.005f * i);
            ani_pos_right[i].z = 0.5f - (0.005f * i);
        }
        for (int i = 0; i < 10; i++)
        {
            ani_pos_left[i].x = -0.5f - (0.01f * i);
            ani_pos_left[i].y = 0.7f - (0.005f * i);
            ani_pos_left[i].z = 0.5f - (0.005f * i);
        }

        //�z��̏�����
        ani_count[0] = 4;
        ani_count[1] = 5;
        ani_check[0] = true;
        ani_check[1] = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�ړ����̂ݎ肪����(��)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            //�E��ƍ��肻�ꂼ��̓���
            if (hand_check)
                hand_move_right(now_pos.y);
            else
                hand_move_left(now_pos.y);

            now_pos = this.gameObject.transform.localPosition;

            move_amount_y += updown_speed;
            if (move_amount_y <= 0.2f)
                now_pos.y += updown_speed;
            else
                move_amount_y = 0.2f;
        }
        else
        {
            move_amount_y -= updown_speed;
            if (move_amount_y >= 0)
                now_pos.y -= updown_speed;
            else
                move_amount_y = 0;
        }

        this.gameObject.transform.localPosition = now_pos;

        //�t���[���̉��Z
        frame++;
    }


    private void hand_move_right(float y)
    {
        //���W�̍X�V
        ani_pos_right[ani_count[0]].y = y;
        this.gameObject.transform.localPosition = ani_pos_right[ani_count[0]];

        //0�`9�Ԃ̃A�j���[�V�������s���������邽�߂̏���
        if (ani_count[0] == 9)
            ani_check[0] = false;
        else if (ani_count[0] == 0)
            ani_check[0] = true;

        //�w�肵���t���[�����ɃA�j���[�V��������i�߂�
        if (frame % ani_speed == 0)
        {
            if (ani_check[0])
                ani_count[0]++;
            else
                ani_count[0]--;
        }
    }

    private void hand_move_left(float y)
    {
        //���W�̍X�V
        ani_pos_left[ani_count[1]].y = y;
        this.gameObject.transform.localPosition = ani_pos_left[ani_count[1]];

        //0�`9�Ԃ̃A�j���[�V�������s���������邽�߂̏���
        if (ani_count[1] == 9)
            ani_check[1] = false;
        else if (ani_count[1] == 0)
            ani_check[1] = true;

        //�w�肵���t���[�����ɃA�j���[�V��������i�߂�
        if (frame % ani_speed == 0)
        {
            if (ani_check[1])
                ani_count[1]++;
            else
                ani_count[1]--;
        }
    }


}
