using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coloer : MonoBehaviour
{
    //���݂̐F�l���m�F���オ�艺����؂�ւ��p�t���O
    public int a=0;
    private bool A;

    //�摜�̐F������ύX
    [SerializeField]
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�F�̏㉺�̕ύX
        if (a < 1)
        {
            A = true;
        }
        else if (a > 255)
        {
            A = false;
        }
        
        //���ۂɔ��f
        if (A == true)
            a++;
        else
            a--;

      //���
        image.color = new Color32(255,255,255,(byte) a);
        
    }
}
