using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ending_move : MonoBehaviour
{
    [SerializeField, Header("�X�^�b�t���[���X�s�[�h")] float role_speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�X�^�b�t���[���̈ړ�����
        if (this.gameObject.GetComponent<RectTransform>().localPosition.y <= 1999)
            this.gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, role_speed, 0);
    }
}
