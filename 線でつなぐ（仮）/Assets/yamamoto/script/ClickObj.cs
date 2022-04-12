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
        //�}�e���A���ύX
        if (Input.GetMouseButtonDown(0))
        {
            mats[0] = mat[1];
        }
        else if(Input.GetMouseButtonDown(1))
        {
            mats[0] = mat[0];
        }

        GetComponent<Renderer>().materials = mats;
    }

   
}
