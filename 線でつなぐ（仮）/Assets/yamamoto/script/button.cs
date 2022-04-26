using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class button : MonoBehaviour
{

    public bool botton_flag;//ボタンクリックフラグ

    public Animator anim;

    public Material[] mat = new Material[1];//変更したいマテリアルをセット
    Material[] mats;

    // Start is called before the first frame update
    void Start()
    {
        botton_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //めた
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
        Debug.Log("はい");
        //1秒停止
        yield return new WaitForSeconds(0.1f);
        botton_flag = false;
    }

    public void RayTargetButton()
    {
        //マテリアル変更(1=強調0=普通)

        mats[0] = mat[0];

        GetComponent<Renderer>().materials = mats;

        Debug.Log("k");

    }
}
