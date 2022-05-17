using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade_pop : MonoBehaviour
{
    [SerializeField, Header("�t�F�[�h���x"), Range(0f, 0.02f)] float fade_speed;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���ߓx���グ��
        gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, fade_speed);

        //���S�ɓ��߂��������
        if (gameObject.GetComponent<Image>().color.a < 0)
            gameObject.SetActive(false);
    }
}
