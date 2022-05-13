using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLightBulb_Script : Base_Color_Script
{
    //�q�I�u�W�F�N�g�擾�p
    private GameObject PointLight;

    // Start is called before the first frame update
    void Start()
    {
        //�|�C���g���C�gobj���擾
        PointLight = transform.Find("Point Light").gameObject;
        Debug.Log(colorchange_signal);
    }

    // Update is called once per frame
    void Update()
    {
        if (colorchange_signal)
        {
            //�F��ݒ�
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            //�|�C���g���C�g�ɐF�����
            PointLight.GetComponent<Base_Color_Script>().SetColor(color);
            PointLight.gameObject.GetComponent<Light>().color = new Color((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE]);
            colorchange_signal = false;
        }
    }
}
