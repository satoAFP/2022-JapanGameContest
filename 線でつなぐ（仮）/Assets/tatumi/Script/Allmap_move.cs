using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Image�R���|�[�l���g��K�v�Ƃ���
[RequireComponent(typeof(Image))]

public class Allmap_move : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // �h���b�O�O�̈ʒu(���S�ʒu)
    private Vector3 prevPos;

    //�ړ��Ώ�
    public GameObject All_map;
    //�ϐ��擾�p
    Vector3 trans_C;

    //MAP���E�_���W
    [SerializeField, Header("���W���E�_(z=�c,x=��)")]
    public float END_z_front, END_z_back,END_x_right,END_x_left;

    // Use this for initialization
    void Start()
    {
        //rootPos = new Vector3(512.0f, 287.0f, 0f); //��ʂ̔���
    }

    // Update is called once per frame
    void Update()
    {
        //��ɍ��J�������������擾
        trans_C = All_map.GetComponent<Allmap_moveCamera>().Getpos();

        //�}�E�X�z�C�[���̉�]�ʎ擾
        var scroll = Input.mouseScrollDelta.y;

        //��xsize�ɗ��Ƃ�����
        var size = All_map.GetComponent<Camera>().orthographicSize;

        //���E�_�̏ꍇ�X���[
        if (size - scroll > 20)
            ;
        else if (size - scroll < 0)
            ;
        //���E�_�Ɉ���������Ȃ��ꍇMAP�̊g�嗦�ɉe��������iCamera��SIZE�ύX�j
        else
        All_map.GetComponent<Camera>().orthographicSize =size - scroll;
    }


    //�h���b�O���h���b�v�֌W

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O�O�̈ʒu���L�����Ă���
        prevPos = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //�c�̂ݔ��f(2Dy��3Dz�ɕϊ��i�c�j)
        trans_C.z = trans_C.z - ((prevPos.y - eventData.position.y) *0.01f);
        //���p
        trans_C.x = trans_C.x - ((prevPos.x - eventData.position.x) * 0.01f);

        //�ړ����(�c)
        if (trans_C.z < END_z_front)
            ;
        else if (trans_C.z > END_z_back)
            ;
        //���f
        else
        All_map.GetComponent<Allmap_moveCamera>().Setpos_z(trans_C.z);

        //�ړ����(��)
        if (trans_C.x > END_x_right)
            ;
        else if (trans_C.x < END_x_left)
            ;
        //���f
        else
            All_map.GetComponent<Allmap_moveCamera>().Setpos_x(trans_C.x);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //����ȂɈ�a���Ȃ��̂ō��̂Ƃ��떳��
       
    }

}
