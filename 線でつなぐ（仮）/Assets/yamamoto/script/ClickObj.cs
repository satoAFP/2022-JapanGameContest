using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    public Material[] mat = new Material[2];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�֐����iUpdate�͂��������߁j
    public void ChangeMaterial(int a)
    {
        //�}�e���A���ύX(1=����0=����)
        
        mats[0] = mat[a];

        GetComponent<Renderer>().materials = mats;
    }

   
}
