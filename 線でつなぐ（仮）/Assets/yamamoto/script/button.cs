using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class button : MonoBehaviour, IPointerClickHandler
{

    public bool botton_flag;//�{�^���N���b�N�t���O

    // Start is called before the first frame update
    void Start()
    {
        botton_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �N���b�N���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    public void OnPointerClick(PointerEventData eventData)
    {
        print($"�I�u�W�F�N�g {name} ���N���b�N���ꂽ��I");
        // �ԐF�ɕύX����
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        //botton_flag = true;//�N���b�N�t���O��true
    }
}
