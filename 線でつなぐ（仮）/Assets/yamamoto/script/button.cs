using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class button : MonoBehaviour
{

    public bool botton_flag;//�{�^���N���b�N�t���O

    public Animator anim;

    public Material[] mat = new Material[1];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;

    // Start is called before the first frame update
    void Start()
    {
        botton_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�߂�
    }

    public void RayPushButton()
    {
        if(botton_flag == true)
        {
            anim.SetBool("Push", true);
            StartCoroutine("ButtonPushed");
        }
        else
        {
            anim.SetBool("Push", false);
        }
       
    }

    IEnumerator ButtonPushed()
    {
        Debug.Log("�͂�");
        //1�b��~
        yield return new WaitForSeconds(0.1f);
        botton_flag = false;
    }

    public void RayTargetButton()
    {
        //�}�e���A���ύX(1=����0=����)

        mats[0] = mat[0];

        GetComponent<Renderer>().materials = mats;

        Debug.Log("k");

    }
}
