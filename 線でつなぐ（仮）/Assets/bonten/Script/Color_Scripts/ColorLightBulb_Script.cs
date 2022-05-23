using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLightBulb_Script : Base_Color_Script
{
    //�q�I�u�W�F�N�g�擾�p
    [SerializeField]
    private GameObject PointLight;

    // Update is called once per frame
    void Update()
    {
        if (colorchange_signal)
        {
            //�F(emiison)��ݒ�
            this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)255));
          
            //�|�C���g���C�g�ɐF�����
            PointLight.GetComponent<Base_Color_Script>().SetColor(color);
            PointLight.gameObject.GetComponent<Light>().color = new Color((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE]);
            colorchange_signal = false;
        }
    }
}
